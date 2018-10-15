using CustomersAgenda.DataAccess.Interfaces;
using CustomersAgenda.DataAccess.Repositories;
using CustomersAgenda.Domain;
using CustomersAgenda.Domain.Model;
using CustomersAgenda.WebUI.ViewModels;
using log4net;
using Ninject;
using CustomersAgenda.WebUI.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;

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
            int x = 0;
            int y = 5 / x;

            Customer customer =customerRepository.GetById(id);
            customer = customerRepository.GetByIdWithPayments(id);
            //List<Payment> payments=customer.PaymentsList.ToList();
            ViewBag.Title = "LabelEdit";
            CustomerViewModel customerViewModel = CreateCustomerViewModel(customer);
            return View(customerViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Save(CreateCustomer(customerViewModel));
                TempData[Constants.MESSAGE_SUCCESS_KEY] = string.Format("{0} saved", customerViewModel.FirstName);
                return RedirectToAction("List");
            }
            else
            {
                return View(customerViewModel);
            }
        }

        public ViewResult Create()
        {
            ViewBag.Title = "Create";
            return View("Edit", new CustomerViewModel());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Customer deletedCustomer=customerRepository.Delete(id);
            if (deletedCustomer != null)
            {
                TempData[Constants.MESSAGE_SUCCESS_KEY] = string.Format("{0} deleted", deletedCustomer.FirstName);
            }           
            return RedirectToAction("List");
        }

        public ViewResult Payments(int id)
        {
            Customer customer = customerRepository.GetByIdWithPayments(id);
            CustomerPaymentsViewModel viewModel = new CustomerPaymentsViewModel
            {
                CustomerName = customer.FirstName,
                DueAmount=customer.DueAmount,
                Payments=customer.PaymentsList
            };
            return View(viewModel);
        }

        public ViewResult Error()
        {
            return View();
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