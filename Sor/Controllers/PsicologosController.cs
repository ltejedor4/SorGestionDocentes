using Sor.Models.EDM;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Sor.Controllers
{
    public class PsicologosController : ApiController
    {
        public IHttpActionResult Get()
        {
            List<Psicologos> psicologos = new List<Psicologos>();
            using (var db = new GestionDocenteEntities())
            {
                psicologos = db.Psicologos.ToList();           
            }

            if (psicologos.Count < 1)
                return NotFound();

            return Ok(psicologos);
        }
    }
}
