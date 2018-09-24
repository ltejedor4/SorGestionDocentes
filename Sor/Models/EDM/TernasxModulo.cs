//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sor.Models.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class TernasxModulo
    {
        public int TernaModuloId { get; set; }
        public int ModuloId { get; set; }
        public int DocenteScoreId { get; set; }
        public bool Rechazado { get; set; }
        public Nullable<byte> MotivoRechazoId { get; set; }
        public bool Asignado { get; set; }
        public string CalificacionDelPsicologo { get; set; }
        public Nullable<decimal> PorcentajeEnAsignacion { get; set; }
    
        public virtual DocenteScore DocenteScore { get; set; }
        public virtual MotivosRechazo MotivosRechazo { get; set; }
        public virtual Modulos Modulos { get; set; }
    }
}
