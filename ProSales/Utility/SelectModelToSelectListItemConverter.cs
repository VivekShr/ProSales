using ProSales.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProSales.Utility
{
    public class SelectModelToSelectListItemConverter
    {

        public static IEnumerable<SelectListItem> Convert(IList<SelectModel> model, string selectedValue = "0")
        {
            return model.Select(x => new  SelectListItem(){
                Text = x.Value,
                Value = x.Id,
                Selected = (x.Id == selectedValue)
            }).ToList();
        }
    }
}