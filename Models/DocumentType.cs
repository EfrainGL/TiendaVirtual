using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }
        public string Description { get; set; }

        public ICollection<Employee> Employee{ get; set; }
        public ICollection<Customer> Customer { get; set; }
    }
}