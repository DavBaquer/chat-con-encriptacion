//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatEmcriptacion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class usuario
    {
        public usuario()
        {
            this.mensajeria = new HashSet<mensajeria>();
        }
    
        public int idusuario { get; set; }
        [Required]
        public string nombres { get; set; }
        [Required]
        public string apellidos { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string contrasenia { get; set; }
    
        public virtual ICollection<mensajeria> mensajeria { get; set; }
    }
}
