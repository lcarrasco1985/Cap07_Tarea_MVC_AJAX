using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cap07_Tarea_MVC_AJAX.WEB.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public double UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}