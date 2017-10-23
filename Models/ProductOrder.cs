using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class ProductOrder: Product
    {
        [DataType(DataType.Currency)]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Este campo es obligatorio!")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]

        public decimal Value { get { return Price * (decimal)Quantity; } }

    }
}