using CustomersAgenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAgenda.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAll();
    }
}
