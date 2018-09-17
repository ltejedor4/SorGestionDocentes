using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sor.Models
{
    public class DocenteScoreJava
    {
        public int docenteScoreId { get; set; }
        public string cedula { get; set; }
        public string nombreDocente { get; set; }
        public decimal horasActuales { get; set; }
        public string nivelEstudio { get; set; }
        public decimal evaluacion { get; set; }
        public byte ausentismos { get; set; }
        public bool cargaNotas { get; set; }
        public Nullable<decimal> porcentaje { get; set; }
    }
}