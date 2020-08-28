using PieShopDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PieShopDemo.ViewModel
{
    public class LogView
    {
        public IEnumerable<RegisterUser> register { get; set; }
    }
}