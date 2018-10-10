using CustomersAgenda.Domain.Entities;
using CustomersAgenda.Domain.Interfaces;
using CustomersAgenda.Domain.Repositories;
using CustomersAgenda.WebUI.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomersAgenda.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CustomerController));

        public ICustomerRepository CustomerRepository { get; set; }

        

        // GET: Customer
        public ActionResult Index()
        {
            Logger.Debug("Controller: Customer, Action: Index");
            //Use ninject
            CustomerRepository = new CustomerRepository();
            IQueryable<Customer> customers= CustomerRepository.GetAll();
            List<CustomerViewModel> customerViewModelList = null;
            //Refactor: do it by in another point
            if (customers != null)
            {
                customerViewModelList = new List<CustomerViewModel>();
                foreach (Customer item in customers)
                {
                    customerViewModelList.Add(new CustomerViewModel
                    {
                        Id=item.Id,
                        FirstName=item.FirstName,
                        LastName=item.LastName,
                        BirthDate=item.BirthDate,
                        DueAmount=item.DueAmount
                    });
                }
            }

            return View(customerViewModelList);
        }
    }
}