using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Display(Name = "Descripcion")]
        [StringLength(30, ErrorMessage = "El campo {1} debe de estar entre {2} y {1} carácteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo es obligatorio!")]
        public string Description { get; set; }
    }
}