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

        [HttpGet]
        public IHttpActionResult CalificacionDelPsicologo(int moduloId, int scoreId, string calificacion)
        {
            int result = 0;
            using (var db = new GestionDocenteEntities())
            {
                var terna = db.TernasxModulo.Where(x => x.ModuloId == moduloId && x.DocenteScoreId == scoreId).FirstOrDefault();
                if (terna != null && terna.TernaModuloId > 0)
                {                    
                    terna.CalificacionDelPsicologo = calificacion;
                    result = db.SaveChanges();
                }
                else
                    return NotFound();
            }

            return Ok();
        }

    }
}
