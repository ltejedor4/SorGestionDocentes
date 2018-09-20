using Sor.Models.EDM;
using Sor.Utilities;
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
                var existeTerna = db.TernasxModulo.Include("DocenteScore").Where(x => x.ModuloId == moduloId).ToList()
                    .Select(x => new DocenteScore()
                    {
                        DocenteScoreId = x.DocenteScoreId,
                        Cedula = x.DocenteScore.Cedula,
                        NombreDocente = x.DocenteScore.NombreDocente,
                        HorasActuales = x.DocenteScore.HorasActuales,
                        NivelEstudio = x.DocenteScore.NivelEstudio,
                        Evaluacion = x.DocenteScore.Evaluacion,
                        Ausentismos = x.DocenteScore.Ausentismos,
                        CargaNotas = x.DocenteScore.CargaNotas,
                        Porcentaje = x.DocenteScore.Porcentaje,
                        Email = x.DocenteScore.Email,
                        twiter = x.DocenteScore.twiter
                    }).ToList<DocenteScore>();

                if (existeTerna != null && existeTerna.Count > 0)                
                    docentesFinal = existeTerna;                
                else
                {
                    var modulo = db.Modulos.Where(x => x.ModuloId == moduloId).FirstOrDefault();
                    if (modulo != null && modulo.ModuloId > 0)
                    {
                        var docentesGeneral = db.DocenteScore.Where(x => x.DocenteMateria.Any(p => p.MateriaId == modulo.MateriaId)).ToList()
                            .Select(x => new DocenteScore()
                            {
                                DocenteScoreId = x.DocenteScoreId,
                                Cedula = x.Cedula,
                                NombreDocente = x.NombreDocente,
                                HorasActuales = x.HorasActuales,
                                NivelEstudio = x.NivelEstudio,
                                Evaluacion = x.Evaluacion,
                                Ausentismos = x.Ausentismos,
                                CargaNotas = x.CargaNotas,
                                Porcentaje = x.Porcentaje,
                                Email = x.Email,
                                twiter = x.twiter
                            }).ToList<DocenteScore>();

                        foreach (var docente in docentesGeneral)
                        {
                            //TODO: Consumir servicio BRMS
                            var docenteJava = ConvertJava.NetToJava(docente);
                            docenteJava.horasActuales += modulo.HorasSemana;
                            var response = await ConsultaApi.Post<DocenteScore>("https://reglasgestiondocente.herokuapp.com/", "getScore", docenteJava);
                            if (response.IsSuccess)
                            {
                                DocenteScore result = (DocenteScore)response.Result;
                                if (result != null && result.DocenteScoreId > 0)
                                {
                                    docente.Porcentaje = result.Porcentaje;
                                    docentesFinal.Add(docente);
                                }

                            }
                        }
                    }

                    var terna = docentesFinal.OrderByDescending(x => x.Porcentaje).Take(3);
                    if (terna.Count() > 0)
                    {
                        var ternas = terna.ToList()
                         .Select(x => new TernasxModulo()
                         {
                             ModuloId = moduloId,
                             DocenteScoreId = x.DocenteScoreId
                         }).ToList<TernasxModulo>();

                        db.TernasxModulo.AddRange(ternas);
                        db.SaveChanges();
                    }
                }
            }
            
            return Ok(docentesFinal.OrderByDescending(x => x.Porcentaje).Take(3));
        }
    }
}
