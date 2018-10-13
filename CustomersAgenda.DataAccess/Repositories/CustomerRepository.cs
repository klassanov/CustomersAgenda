using CustomersAgenda.DataAccess.Context;
using CustomersAgenda.DataAccess.Interfaces;
using CustomersAgenda.Domain.Model;
using log4net;
using System.Collections.Generic;
using System.Linq;

namespace CustomersAgenda.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private static ILog logger = LogManager.GetLogger(typeof(CustomerRepository));

        public IQueryable<Customer> GetAll()
        {
            IQueryable<Customer> customers = null;
            using (var db = new CustomerContext())
            {
                customers = db.Customers.ToList().AsQueryable();
            }
            logger.DebugFormat("{0} Customers retrieved", customers.Count());
            return customers;
        }

        public Customer GetById(int id)
        {
            Customer customer = null;
            using (var db = new CustomerContext())
            {
                customer=db.Customers.Find(id);
            }
            logger.DebugFormat("Retrieved customer with Id {0}", id);
            return customer;
        }

        public Customer GetByIdWithPayments(int id)
        {
            Customer customer = null;
            using (var db = new CustomerContext())
            {
                customer = db.Customers.Find(id);
                IEnumerable<Payment> paymenstList=customer.PaymentsList;
            }
            logger.DebugFormat("Retrieved customer with Id and payments {0}", id);
            return customer;
        }

        public void Save(Customer customer)
        {
            using (var db = new CustomerContext())
            {
                if (customer.Id == 0)
                {
                    db.Customers.Add(customer);
                }
                else
                {
                    Customer dbEntry = db.Customers.Find(customer.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.FirstName = customer.FirstName;
                        dbEntry.LastName = customer.LastName;
                        dbEntry.BirthDate = customer.BirthDate;
                        dbEntry.DueAmount = customer.DueAmount;
                    }
                }
                db.SaveChanges();
            }
            logger.DebugFormat("Saved customer with id {0}", customer.Id);
        }

        public Customer Delete(int id)
        {
            Customer dbEntry = null;
            using (var db = new CustomerContext())
            {
                dbEntry = db.Customers.Find(id);
                if (dbEntry != null)
                {
                    db.Customers.Remove(dbEntry);
                    db.SaveChanges();
                    logger.DebugFormat("Deleted customer with id {0}", id);
                }
            }
            return dbEntry;
        }

        
    }
}
