using CustomersAgenda.DataAccess.Interfaces;
using CustomersAgenda.DataAccess.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomersAgenda.WebUI.Infrastructure
{
    public class DataAccessBindingsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomerRepository>().To<CustomerRepository>();
        }
    }
}