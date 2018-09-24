using Sor.Models.EDM;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Sor.Controllers
{
    public class ModulosController : ApiController
    {
        public IHttpActionResult Get(int cantidad)
        {
            List<Modulos> modulos = new List<Modulos>();
            using (var db = new GestionDocenteEntities())
            {
                modulos = db.Modulos.Include("Materias").Where(x => !x.Asignado).ToList()
                     .Select(x => new Modulos()
                     {
                         ModuloId = x.ModuloId,
                         Nombre = x.Materias.Nombre,
                         FechaInicio = x.FechaInicio,
                         FechaFin = x.FechaFin,
                         HorasTotales = x.HorasTotales,
                         HorasSemana = x.HorasSemana,
                         EsPago = x.EsPago,
                         MateriaId = x.MateriaId
                     }).ToList<Modulos>();
            }

            if (modulos.Count < 1)
                return NotFound();

            return Ok(modulos.Take(cantidad));
        }

        public IHttpActionResult GetModulo(int moduloId)
        {
            Modulos modulos = new Modulos();
            using (var db = new GestionDocenteEntities())
            {
                modulos = db.Modulos.Include("Materias").Where(x => x.ModuloId == moduloId).ToList()
                     .Select(x => new Modulos()
                     {
                         ModuloId = x.ModuloId,
                         Nombre = x.Materias.Nombre,
                         FechaInicio = x.FechaInicio,
                         FechaFin = x.FechaFin,
                         HorasTotales = x.HorasTotales,
                         HorasSemana = x.HorasSemana,
                         Psicologo = x.Psicologo,
                         Asignado = x.Asignado,
                         EsPago = x.EsPago,
                         MateriaId = x.MateriaId
                     }).FirstOrDefault();

            }

            if (modulos == null)
                return NotFound();

            return Ok(modulos);
        }

        [HttpGet]
        public IHttpActionResult ModulosxDepartamento(byte dptoAcademicoId)
        {
            List<Modulos> modulos = new List<Modulos>();
            using (var db = new GestionDocenteEntities())
            {
                modulos = db.Modulos.Where(x => x.Materias.DeptoAcademicoId == dptoAcademicoId && !x.Asignado).ToList();
            }

            if (modulos.Count < 1)
                return NotFound();

            return Ok(modulos);
        }

        [HttpGet]
        public IHttpActionResult PutAsignaPsicologo(int moduloId, string psicologo)
        {
            if (string.IsNullOrEmpty(psicologo))
                return BadRequest("Not a valid model");

            int result = 0;
            using (var db = new GestionDocenteEntities())
            {
                var modulo = db.Modulos.Where(x => x.ModuloId == moduloId).FirstOrDefault();
                if (modulo != null && modulo.ModuloId > 0)
                {
                    modulo.Psicologo = psicologo;
                    result = db.SaveChanges();
                }
                else
                    return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult AsignaDocente(int moduloId, int docenteId)
        {
            int result = 0;
            using (var db = new GestionDocenteEntities())
            {
                var modulo = db.Modulos.Where(x => x.ModuloId == moduloId).FirstOrDefault();
                if (modulo != null && modulo.ModuloId > 0)
                {
                    modulo.DocenteId = docenteId;
                    modulo.Asignado = true;
                }

                var docente = db.DocenteScore.Where(x => x.DocenteScoreId == docenteId).FirstOrDefault();
                if (docente != null && modulo.DocenteId > 0)
                    docente.HorasActuales += modulo.HorasSemana;

                result = db.SaveChanges();
            }

            return Ok(result);
        }

    }
}
