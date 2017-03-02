﻿using ProSales.Repository;
using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.Service
{
    public interface ICustomerService
    {
        IList<CustomerViewModel> GetCustomers();
        IList<SelectModel> GetCustomerDdl();
    }
}