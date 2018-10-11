using CustomersAgenda.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAgenda.DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAll();
        Customer GetById(int id);
        void Save(Customer customer);
        Customer Delete(int id);
    }
}
