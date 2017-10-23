using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required(ErrorMessage ="Este campo es obligatorio.")]
        [Display(Name = "Nombre")]
        [StringLength(30,ErrorMessage ="El campo {0} debe de estar entre {2} y {1} caracteres",MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} debe de estar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string LastName { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} debe de estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} debe de estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Dirección")]
        public string Address{ get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }
    }
}