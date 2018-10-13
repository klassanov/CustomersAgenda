using CustomersAgenda.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomersAgenda.WebUI.ViewModels
{
    public class CustomerPaymentsViewModel
    {
        public string CustomerName { get; set; }
        public decimal DueAmount { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
    }
}