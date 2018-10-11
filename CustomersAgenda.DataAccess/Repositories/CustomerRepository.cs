using CustomersAgenda.DataAccess.Context;
using CustomersAgenda.DataAccess.Interfaces;
using CustomersAgenda.Domain.Model;
using System.Linq;

namespace CustomersAgenda.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public IQueryable<Customer> GetAll()
        {

            IQueryable<Customer> customers = null;
            using (var db = new CustomerContext())
            {
                customers = db.Customers.ToList().AsQueryable();
                //customers = db.Customers;//.ToList().AsQueryable();
            }
            return customers;


            //return new List<Customer>().AsQueryable();
        }
    }
}
