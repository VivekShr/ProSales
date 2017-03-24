using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProSales.Utility
{
    public class Helper
    {
        public static string FourDigitRandomNumberGenerator()
        {
            var n = new Random();
            int number = n.Next(10000, 99999);
            var key = number.ToString().Substring(1, 4);
            return key;
        }
    }
}