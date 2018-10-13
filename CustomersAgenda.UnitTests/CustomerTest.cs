using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CustomersAgenda.DataAccess.Interfaces;
using CustomersAgenda.Domain.Model;
using CustomersAgenda.WebUI.Controllers;
using CustomersAgenda.WebUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CustomersAgenda.UnitTests
{
    [TestClass]
    public class CustomerTest
    {
        private Mock<ICustomerRepository> customerRepository;

        public CustomerTest()
        {
            Customer[] customers = new Customer[] {
                new Customer{Id=1, FirstName="Alex", LastName="Klassanov", BirthDate=new DateTime(1985, 7, 23), DueAmount=100m},
                new Customer{Id=2, FirstName="Paolo", LastName="Gentili", BirthDate=new DateTime(1975, 9, 7), DueAmount=100m},
                new Customer{Id=3, FirstName="Metodi", LastName="Georgiev", BirthDate=new DateTime(1987, 4, 9), DueAmount=100m},
                new Customer{Id=4, FirstName="Lorenzo", LastName="Sepicacchi", BirthDate=new DateTime(1978, 6, 10), DueAmount=100m}
            };

            customerRepository = new Mock<ICustomerRepository>();
            customerRepository.Setup(m => m.GetAll()).Returns(customers.AsQueryable());
            customerRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns<int>(customerId =>
                customers.Where(x => x.Id == customerId).FirstOrDefault());
        }


        [TestMethod]
        public void List_Contains_AllCustomers()
        {
            //Arrange
            CustomerController controller = new CustomerController(customerRepository.Object);

            //Act
            IEnumerable<CustomerViewModel> viewModel = controller.List().Model as IEnumerable<CustomerViewModel>;

            //Assert
            Assert.AreEqual(viewModel.Count(), 4);
            Assert.AreEqual(viewModel.ElementAt(0).Id, 1);
            Assert.AreEqual(viewModel.ElementAt(1).Id, 2);
            Assert.AreEqual(viewModel.ElementAt(2).Id, 3);
            Assert.AreEqual(viewModel.ElementAt(3).Id, 4);
        }

        [TestMethod]
        public void Edit_CanEdit_ExistingCustomer()
        {
            //Arrange            
            CustomerController controller = new CustomerController(customerRepository.Object);

            //Act
            int productId = 1;
            CustomerViewModel viewModel = controller.Edit(productId).Model as CustomerViewModel;

            //Assert
            Assert.AreEqual(viewModel.Id, productId);
        }

        [TestMethod]
        public void Edit_CannotEdit_NonExistentCustomer()
        {
            //Arrange
            CustomerController controller = new CustomerController(customerRepository.Object);

            //Act
            int productId = -1;
            CustomerViewModel viewModel = controller.Edit(productId).Model as CustomerViewModel;

            //Assert
            Assert.IsNull(viewModel);
        }

        [TestMethod]
        public void Edit_CanSave_ValidChanges()
        {
            //Arrange
            CustomerController controller = new CustomerController(customerRepository.Object);
            CustomerViewModel customerViewModel = new CustomerViewModel { FirstName = "John", LastName = "Smith", DueAmount = 50, BirthDate = new DateTime(1977, 10, 20) };

            //Act
            ActionResult result=controller.Edit(customerViewModel);

            //Assert: save method was called  and the result is not a view
            customerRepository.Verify(m => m.Save(It.IsAny<Customer>()));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Edit_CannotSave_InvalidChanges()
        {
            //Arrange
            CustomerController controller = new CustomerController(customerRepository.Object);
            CustomerViewModel customerViewModel = new CustomerViewModel();
            controller.ModelState.AddModelError("", new Exception());

            //Act
            ActionResult result = controller.Edit(customerViewModel);          

            //Assert
            customerRepository.Verify(m => m.Save(It.IsAny<Customer>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

    }
}
