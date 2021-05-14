using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Computers_Sales_WebApplication.Models
{
    public class OrdersModel
    {
        public int Order_ID { get; set; }
        public DateTime Order_Date { get; set; }
        public int Computer_ID { get; set; }
        public int Order_Value { get; set; }
        public decimal Discount { get; set; }
        public double Amount { get; set; }
    }
}