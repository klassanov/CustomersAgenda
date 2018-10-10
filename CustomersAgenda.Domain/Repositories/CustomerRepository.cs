using CustomersAgenda.Domain.Context;
using CustomersAgenda.Domain.Entities;
using CustomersAgenda.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersAgenda.Domain.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public IQueryable<Customer> GetAll()
        {
            
            IQueryable<Customer> customers = null;
            using (var db = new CustomerContext())
            {
                customers= db.Customers.ToList().AsQueryable();
                //customers = db.Customers;//.ToList().AsQueryable();
            }
            return customers;
           

            //return new List<Customer>().AsQueryable();
        }
    }
}
