using CustomersAgenda.DataAccess.Interfaces;
using CustomersAgenda.DataAccess.Repositories;
using CustomersAgenda.Domain.Model;
using CustomersAgenda.WebUI.ViewModels;
using log4net;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CustomersAgenda.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CustomerController));

        //For property injection use [Inject]
        private ICustomerRepository customerRepository { get; set; }

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        

        // GET: Customer
        public ActionResult Index()
        {
            Logger.Debug("Controller: Customer; Action: Index");
            IQueryable<Customer> customers= customerRepository.GetAll();
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