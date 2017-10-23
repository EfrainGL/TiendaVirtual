using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El campo {0} debe de estar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Apellido")]
        [StringLength(30, ErrorMessage = "El campo {0} debe de estar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Tipo de Documento")]
        public int DocumentTypeID { get; set; }

        
        public string FullName { get { return string.Format("{0} {1}", Name, LastName);}  } 

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}