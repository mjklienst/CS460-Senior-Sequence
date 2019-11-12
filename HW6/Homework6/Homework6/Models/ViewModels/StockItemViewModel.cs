using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homework6.Models;
using Homework6.DAL;

namespace Homework6.Models.ViewModels
{
    public class PurchaseList
    {
        //For the purchase list
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
    }
    public class StockItemViewModel
    {
        //For the StockItem table
        public string StockItemName { get; set; }
        public string Size { get; set; }
        public decimal? RecommendedRetailPrice { get; set; }
        public decimal TypicalWeightPerUnit { get; set; }
        public int LeadTimeDays { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ValidFrom { get; set; }
        public string CustomFields { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Tags { get; set; }
        public byte[] Photo { get; set; }

        //For the Supplier table
        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteURL { get; set; }
        //will use below ID to join w People table, this in linqpad already, should be 13 names
        public string FullName { get; set; }
        public int SupplierID { get; set; }


        //Purchase history information
        public double Orders { get; set; }
        public decimal GrossSales { get; set; }
        public decimal GrossProfit { get; set; }

        //For the purchase list
        public List<PurchaseList> ListOfPurchases { get; set; }


    }

}