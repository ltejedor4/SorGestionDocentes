using Sor.Models.EDM;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sor.Controllers
{
    public class DocentesController : ApiController
    {

        [HttpGet]
        public async Task<IHttpActionResult> DocentesSugeridos(int moduloId)
        {
            List<DocenteScore> docentesFinal = new List<DocenteScore>();
            using (var db = new GestionDocenteEntities())
            {
                var modulo = db.Modulos.Where(x => x.ModuloId == moduloId).FirstOrDefault();
                if (modulo != null && modulo.ModuloId > 0)
                {
                    var docentesGeneral = db.DocenteScore.Where(x => x.DocenteMateria.Any(p => p.MateriaId == modulo.MateriaId)).ToList();
                    foreach (var docente in docentesGeneral)
                    {
                        //TODO: Consumir servio BRMS
                        //var docenteJava = ConvertJava.NetToJava(docente);
                        //docenteJava.horasActuales += modulo.HorasSemana;
                        //var response = await ConsultaApi.Post<DocenteScore>("http://localhost:8080/", "gestionDocente", docenteJava);
                        //if (response.IsSuccess)
                        //{
                        //    DocenteScore result = (DocenteScore)response.Result;
                        //    if (result != null && result.DocenteScoreId > 0)
                        //    {
                        //docente.Porcentaje = result.Porcentaje;
                        docente.Porcentaje = 100;
                        docentesFinal.Add(docente);
                        //}
                        //}
                    }
                }
            }

            return Ok(docentesFinal.OrderByDescending(x => x.Porcentaje).Take(3));

        }
    }
}
