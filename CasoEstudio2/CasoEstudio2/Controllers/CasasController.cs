using CasoEstudio2.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace CasoEstudio2.Controllers
{
    public class CasasController : Controller
    {
        private readonly IConfiguration _configuration;

        public CasasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MostrarCasas()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var casas = context.Query<CasasModel>(
                    "MostrarCasas", commandType: CommandType.StoredProcedure).ToList();

                return View(casas);
            }
        }

        [HttpGet]
        public IActionResult CasasDisponibles()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var casas = context.Query<CasasModel>(
                    "MostrarCasasOrdenadasPorEstado", commandType: CommandType.StoredProcedure).ToList();

                return View(casas);
            }
        }

        [HttpGet]
        public IActionResult CasasPrecios()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var casas = context.Query<CasasModel>(
                    "MostrarCasasPorPrecio", commandType: CommandType.StoredProcedure).ToList();

                return View(casas);
            }
        }

        [HttpGet]
        public IActionResult AlquilarCasa()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var casas = context.Query<CasasModel>(
                    "ObtenerCasasDisponibles", commandType: CommandType.StoredProcedure).ToList();

                var selectList = new List<SelectListItem>();
                foreach (var casa in casas)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = casa.IdCasa.ToString(),
                        Text = $"{casa.DescripcionCasa} - ₡{casa.PrecioCasa:N2}"
                    });
                }

                ViewBag.Casas = selectList;
                return View();
            }
        }

        [HttpPost]
        public IActionResult AlquilarCasa(CasasModel casa)
        {
            System.Diagnostics.Debug.WriteLine($"Iniciando AlquilarCasa con IdCasa: {casa.IdCasa}, UsuarioAlquiler: {casa.UsuarioAlquiler}");
            TempData["Debug"] = "Iniciando proceso de alquiler...";

            
            ModelState.Remove("DescripcionCasa");
            ModelState.Remove("Fecha");
            ModelState.Remove("Estado");
            ModelState.Remove("PrecioCasa");

            
            if (string.IsNullOrEmpty(casa.UsuarioAlquiler))
            {
                System.Diagnostics.Debug.WriteLine("Error: UsuarioAlquiler vacío");
                ModelState.AddModelError("UsuarioAlquiler", "El usuario que alquila es requerido");
            }

            if (casa.IdCasa == 0)
            {
                System.Diagnostics.Debug.WriteLine("Error: Casa no seleccionada");
                ModelState.AddModelError("IdCasa", "Debe seleccionar una casa");
            }

         
            foreach (var modelStateEntry in ModelState.Values)
            {
                foreach (var error in modelStateEntry.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Error de validación: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("ModelState no válido");
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                TempData["Debug"] = $"Errores de validación: {string.Join(", ", errorMessages)}";
                CargarListaCasas();
                return View(casa);
            }

            try
            {
                System.Diagnostics.Debug.WriteLine("Intentando actualizar en la base de datos...");
                TempData["Debug"] = "Intentando actualizar en la base de datos...";

                using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@IdCasa", casa.IdCasa);
                    parameters.Add("@UsuarioAlquiler", casa.UsuarioAlquiler.Trim());

                    System.Diagnostics.Debug.WriteLine($"Ejecutando SP con IdCasa: {casa.IdCasa}, UsuarioAlquiler: {casa.UsuarioAlquiler}");
                    int rowsAffected = context.Execute("ActualizarAlquilerCasa", parameters, commandType: CommandType.StoredProcedure);
                    
                    System.Diagnostics.Debug.WriteLine($"Filas afectadas: {rowsAffected}");
                    TempData["Debug"] = $"Resultado de la actualización: {rowsAffected} filas afectadas";

                    if (rowsAffected > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Actualización exitosa, redirigiendo...");
                        TempData["Mensaje"] = "La casa ha sido reservada exitosamente";
                        return RedirectToAction("MostrarCasas");
                    }
                    
                    System.Diagnostics.Debug.WriteLine("No se actualizó ninguna fila");
                    ModelState.AddModelError("", "No se pudo reservar la casa. La casa puede no estar disponible o ya está reservada.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en la actualización: {ex.Message}");
                ModelState.AddModelError("", $"Ocurrió un error al procesar la reserva: {ex.Message}");
                TempData["Debug"] = $"Error: {ex.Message}";
            }

            System.Diagnostics.Debug.WriteLine("Volviendo a la vista con errores");
            CargarListaCasas();
            return View(casa);
        }

        private void CargarListaCasas()
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var casas = context.Query<CasasModel>(
                    "ObtenerCasasDisponibles", commandType: CommandType.StoredProcedure).ToList();

                var selectList = new List<SelectListItem>();
                foreach (var c in casas)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = c.IdCasa.ToString(),
                        Text = $"{c.DescripcionCasa} - ₡{c.PrecioCasa:N2}"
                    });
                }

                ViewBag.Casas = selectList;
            }
        }
    }
}
