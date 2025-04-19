using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        //[HttpGet]
        //public IActionResult RegistrarMatricula()
        //{
        //    using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
        //    {
        //        var tipoCursoLista = context.Query<(int Id, string Nombre)>("SELECT TipoCurso, DescripcionTipoCurso FROM TiposCursos");
        //        ViewBag.TipoCursoLista = tipoCursoLista.Select(tc => new SelectListItem
        //        {
        //            Value = tc.Id.ToString(),
        //            Text = tc.Nombre
        //        }).ToList();
        //    }
        //    return View();
        //}


    }
}
