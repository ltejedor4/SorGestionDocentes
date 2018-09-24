using Sor.Models.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sor.Controllers
{
    public class TernaModuloController : ApiController
    {       
        [HttpGet]
        public IHttpActionResult PutRechazoTernaxModulo(int moduloId, int scoreId, byte tipoRechazo)
        {
            int existeTerna = 0;
            using (var db = new GestionDocenteEntities())
            {
                var terna = db.TernasxModulo.Include("DocenteScore").Where(x => x.ModuloId == moduloId && x.DocenteScoreId == scoreId).FirstOrDefault();
                if (terna != null && terna.TernaModuloId > 0)
                {
                    terna.Rechazado = true;
                    terna.MotivoRechazoId = tipoRechazo;

                    var porcentajeActual = terna.DocenteScore.Porcentaje;
                    var descuento = db.MotivosRechazo.Where(x => x.MotivoRechazoId == tipoRechazo).FirstOrDefault();
                    terna.DocenteScore.Porcentaje = porcentajeActual - (descuento != null ? descuento.PuntosDescuento : 5);
                    db.SaveChanges();

                    existeTerna = db.TernasxModulo.Where(x => x.ModuloId == moduloId && !x.Rechazado).Count();
                }
                else
                    return NotFound();
            }

            return Json(new { success = true, numDocentesTerna = existeTerna });
            
        }
    }
}
