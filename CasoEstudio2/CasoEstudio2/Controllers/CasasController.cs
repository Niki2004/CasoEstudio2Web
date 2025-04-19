using CasoEstudio2.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;


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
    }


}
