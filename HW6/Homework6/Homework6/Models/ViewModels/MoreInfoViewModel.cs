using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace Homework6.Models.ViewModels
{
    public class MoreInfoViewModel
    {
        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteURL { get; set; }
        //will use below ID to join w People table, this in linqpad already, should be 13 names
        public int SupplierID { get; set; }


    }
}