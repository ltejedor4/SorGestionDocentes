using Sor.Models.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sor.Controllers
{
    public class MotivoRechazoController : ApiController
    {
        public IHttpActionResult Get()
        {
            List<MotivosRechazo> motivosRechazo = new List<MotivosRechazo>();
            using (var db = new GestionDocenteEntities())
            {
                motivosRechazo = db.MotivosRechazo.ToList();
            }

            if (motivosRechazo.Count < 1)
                return NotFound();

            return Ok(motivosRechazo);
        }
    }
}
