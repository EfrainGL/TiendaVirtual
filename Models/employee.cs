using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Display(Name="Nombre")]
        [StringLength(30, ErrorMessage = "El campo {0} debe de estar entre {2} y {1} carácteres", MinimumLength=3)]
        [Required(ErrorMessage="Este campo es obligatorio!")]
        public string FirstName { get; set; }


        [Display(Name = "Apellido")]
        [StringLength(30, ErrorMessage = "El campo {1} debe de estar entre {2} y {1} carácteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo es obligatorio!")]
        public string LastName { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Salario")]
        public decimal Salary { get; set; }


        
        [Display(Name = "Porcentaje de Bono")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public float BonusPercent { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora de Entrada")]
        public DateTime StartTime { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "URL")]


        public string URL { get; set; }
        [Display(Name = "Tipo de Documento")]
        public int DocumentTypeID{ get; set; }

        [Display(Name = "Edad")]
        public int Age { get { return  DateTime.Today.Year - DateOfBirth.Year;} }

        public virtual DocumentType DocumentType { get; set; }
    }
}