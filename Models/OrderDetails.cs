using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }

        public int ProductID{ get; set; }

        [Display(Name = "Descripcion")]
        [StringLength(30, ErrorMessage = "El campo {1} debe de estar entre {2} y {1} carácteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo es obligatorio!")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Este campo es obligatorio!")]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Este campo es obligatorio!")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Quantity{ get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        
    }
}