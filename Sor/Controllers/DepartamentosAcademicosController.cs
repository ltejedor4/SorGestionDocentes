using Sor.Models.EDM;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Sor.Controllers
{
    public class DepartamentosAcademicosController : ApiController
    {
        public IHttpActionResult Get()
        {
            List<DepartamentosAcademicos> departamentos = new List<DepartamentosAcademicos>();
            using (var db = new GestionDocenteEntities())
            {
                departamentos = db.DepartamentosAcademicos.ToList();               
            }

            if (departamentos.Count < 1)
                return NotFound();

            return Ok(departamentos);
        }



    }
}
