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
    
    public partial class DocenteScore
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocenteScore()
        {
            this.DocenteMateria = new HashSet<DocenteMateria>();
            this.TernasxModulo = new HashSet<TernasxModulo>();
        }
    
        public int DocenteScoreId { get; set; }
        public string Cedula { get; set; }
        public string NombreDocente { get; set; }
        public decimal HorasActuales { get; set; }
        public string NivelEstudio { get; set; }
        public decimal Evaluacion { get; set; }
        public byte Ausentismos { get; set; }
        public bool CargaNotas { get; set; }
        public Nullable<decimal> Porcentaje { get; set; }
        public string Email { get; set; }
        public string Twiter { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocenteMateria> DocenteMateria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TernasxModulo> TernasxModulo { get; set; }
    }
}
