using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAgenda.Domain.Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        
        public int CustomerId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }

    }
}
