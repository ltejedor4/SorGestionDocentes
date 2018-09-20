using Sor.Models.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sor.Models
{
    public class DetalleTerna
    {
        public int DocenteScoreId { get; set; }
        public string Cedula { get; set; }
        public string NombreDocente { get; set; }
        public decimal HorasActuales { get; set; }
        public string NivelEstudio { get; set; }
        public decimal EvaluacionEstudiante { get; set; }
        public byte Ausentismos { get; set; }
        public bool CargaNotas { get; set; }
        public Nullable<decimal> Porcentaje { get; set; }
        public string Email { get; set; }
        public string Twiter { get; set; }
        public string CalificacionDelPsicologo { get; set; }

    }
}