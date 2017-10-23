using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Display(Name = "Descripcion")]
        [StringLength(30, ErrorMessage = "El campo {1} debe de estar entre {2} y {1} carácteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo es obligatorio!")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name ="Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }
        

        [Display(Name = "Ultima Compra")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime LastBuy { get; set; }
        
        [Display(Name ="Unidades Disponibles")]

        public int Stock { get; set; }

        [Display(Name ="Comentarios")]


        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        public virtual ICollection<OrderDetails> Products { get; set; }

    }
}