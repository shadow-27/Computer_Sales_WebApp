using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Computers_Sales_WebApplication.Models
{
    public class ComputersClass
    {
        public string Producent { get; set; }
        public string Model { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }
    }
}