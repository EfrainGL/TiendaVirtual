using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class SupplierProduct
    {
        [Key]
        public int SupplierProductID { get; set; }
        public int SupplierID { get; set; }
        public int ProductID { get; set; }

        public virtual Supplier supplier { get; set; }

        public virtual Product product
        {
            get; set;


        }
    }
}