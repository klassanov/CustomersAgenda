using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomersAgenda.WebUI.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal DueAmount { get; set; }

        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}