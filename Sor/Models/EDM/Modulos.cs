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
    
    public partial class Modulos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modulos()
        {
            this.TernasxModulo = new HashSet<TernasxModulo>();
        }
    
        public int ModuloId { get; set; }
        public string Nombre { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public short HorasTotales { get; set; }
        public decimal HorasSemana { get; set; }
        public string Psicologo { get; set; }
        public bool EsPago { get; set; }
        public bool Asignado { get; set; }
        public short MateriaId { get; set; }
        public Nullable<int> DocenteId { get; set; }
    
        public virtual Materias Materias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TernasxModulo> TernasxModulo { get; set; }
    }
}
