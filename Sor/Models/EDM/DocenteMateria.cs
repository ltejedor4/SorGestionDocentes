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
    
    public partial class DocenteMateria
    {
        public int MateriaxDocenteId { get; set; }
        public short MateriaId { get; set; }
        public int ScoreDocenteId { get; set; }
    
        public virtual Materias Materias { get; set; }
        public virtual DocenteScore DocenteScore { get; set; }
    }
}