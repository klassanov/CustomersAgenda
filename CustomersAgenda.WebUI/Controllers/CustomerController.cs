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
        // private static readonly ILog Logger = LogManager.GetLogger(typeof(CustomerController));
        //For property injection use [Inject]
        private ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        
        public ViewResult List()
        {
            IQueryable<Customer> customers= customerRepository.GetAll();
            List<CustomerViewModel> customerViewModelList = null;            
            if (customers != null)
            {
                customerViewModelList = new List<CustomerViewModel>();
                foreach (Customer item in customers)
                {
                    customerViewModelList.Add(CreateCustomerViewModel(item));
                }
            }
            return View(customerViewModelList);
        }

        public ViewResult Edit(int id)
        {
            Customer customer=customerRepository.GetById(id);
            CustomerViewModel customerViewModel = CreateCustomerViewModel(customer);
            return View(customerViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Save(CreateCustomer(customerViewModel));
                TempData["message"] = string.Format("{0} has been saved", customerViewModel.FirstName);
                return RedirectToAction("List");
            }
            else
            {
                return View(customerViewModel);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new CustomerViewModel());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Customer deletedCustomer=customerRepository.Delete(id);
            if (deletedCustomer != null)
            {
                TempData["message"] = string.Format("{0} has been deleted", deletedCustomer.FirstName);
            }           
            return RedirectToAction("List");
        }

        public CustomerViewModel CreateCustomerViewModel(Customer customer)
        {
            CustomerViewModel customerViewModel = null;
            if (customer != null)
            {
                customerViewModel = new CustomerViewModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    BirthDate = customer.BirthDate,
                    DueAmount = customer.DueAmount
                };
            }
            return customerViewModel;
        }

        public Customer CreateCustomer(CustomerViewModel customerViewModel)
        {
            Customer customer = null;
            if (customerViewModel != null)
            {
                customer = new Customer
                {
                    Id = customerViewModel.Id,
                    FirstName = customerViewModel.FirstName,
                    LastName = customerViewModel.LastName,
                    BirthDate = customerViewModel.BirthDate,
                    DueAmount = customerViewModel.DueAmount
                };
            }
            return customer;
        }


    }
}