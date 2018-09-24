using Sor.Models;
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
            List<DetalleTerna> docentesFinal = new List<DetalleTerna>();
            using (var db = new GestionDocenteEntities())
            {
                var existeTerna = db.TernasxModulo.Include("DocenteScore").Where(x => x.ModuloId == moduloId && !x.Rechazado).ToList()
                    .Select(x => new DetalleTerna()
                    {
                        DocenteScoreId = x.DocenteScoreId,
                        Cedula = x.DocenteScore.Cedula,
                        NombreDocente = x.DocenteScore.NombreDocente,
                        HorasActuales = x.DocenteScore.HorasActuales,
                        NivelEstudio = x.DocenteScore.NivelEstudio,
                        EvaluacionEstudiante = x.DocenteScore.Evaluacion,
                        Ausentismos = x.DocenteScore.Ausentismos,
                        CargaNotas = x.DocenteScore.CargaNotas,
                        Porcentaje = x.PorcentajeEnAsignacion,
                        Email = x.DocenteScore.Email,
                        Twiter = x.DocenteScore.Twiter,
                        CalificacionDelPsicologo = x.CalificacionDelPsicologo
                    }).ToList<DetalleTerna>();

                if (existeTerna != null && existeTerna.Count > 0)
                    docentesFinal = existeTerna;
                else
                {
                    var modulo = db.Modulos.Where(x => x.ModuloId == moduloId).FirstOrDefault();
                    if (modulo != null && modulo.ModuloId > 0)
                    {
                        var docentesGeneral = db.DocenteScore.Where(x => !x.TernasxModulo.Any(t => t.Rechazado) && x.DocenteMateria.Any(p => p.MateriaId == modulo.MateriaId && p.Estado)).ToList()
                            .Select(x => new DetalleTerna()
                            {
                                DocenteScoreId = x.DocenteScoreId,
                                Cedula = x.Cedula,
                                NombreDocente = x.NombreDocente,
                                HorasActuales = x.HorasActuales,
                                NivelEstudio = x.NivelEstudio,
                                EvaluacionEstudiante = x.Evaluacion,
                                Ausentismos = x.Ausentismos,
                                CargaNotas = x.CargaNotas,
                                Porcentaje = x.Porcentaje,
                                Email = x.Email,
                                Twiter = x.Twiter
                            }).ToList<DetalleTerna>();

                        foreach (var docente in docentesGeneral)
                        {
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
                             PorcentajeEnAsignacion = x.Porcentaje,
                             DocenteScoreId = x.DocenteScoreId
                         }).ToList<TernasxModulo>();

                        db.TernasxModulo.AddRange(ternas);
                        db.SaveChanges();
                    }
                }
            }

            return Ok(docentesFinal.OrderByDescending(x => x.Porcentaje).Take(3));
        }

        [HttpGet]
        public IHttpActionResult CrearTercero(string cedula, string nombre, int moduloId)
        {
            DocenteScore newDocente = new DocenteScore();
            using (var db = new GestionDocenteEntities())
            {
                var existeDocente = db.DocenteScore.Where(x => x.Cedula == cedula).FirstOrDefault();
                if (existeDocente != null && existeDocente.DocenteScoreId > 1)
                    return NotFound();
                else
                {
                    DocenteScore docente = new DocenteScore();
                    docente.NombreDocente = nombre;
                    docente.Cedula = cedula;
                    docente.NivelEstudio = "MAESTRIA";
                    docente.Twiter = "@tejedorl";
                    docente.Email = "ltejedor4@gmail.com";
                    docente.Porcentaje = 100;
                    newDocente = db.DocenteScore.Add(docente);
                    db.SaveChanges();

                }
            }

            using (var bd = new GestionDocenteEntities())
            {
                var materia = bd.Modulos.Where(x => x.ModuloId == moduloId).Select(x => x.MateriaId).FirstOrDefault();
                if (materia > 0 && newDocente != null && newDocente.DocenteScoreId > 0)
                {
                    DocenteMateria docenteMat = new DocenteMateria();
                    docenteMat.MateriaId = materia;
                    docenteMat.ScoreDocenteId = newDocente.DocenteScoreId;
                    docenteMat.Estado = true;
                    bd.DocenteMateria.Add(docenteMat);
                    bd.SaveChanges();
                }
            }

            return Ok(newDocente.DocenteScoreId);
        }
    }
}
