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
    public class StockItemViewModel
    {
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


    }

}