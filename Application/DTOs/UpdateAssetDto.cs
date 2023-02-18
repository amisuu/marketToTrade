using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateAssetDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Form { get; set; }
        public string Mass { get; set; }
        public string Fineness { get; set; }
        public int Quantity { get; set; }
        public string Producer { get; set; }
        public string Price { get; set; }
        public int Year { get; set; }
        public string Condition { get; set; }
        public YesOrNoEnum IsOriginalPackage { get; set; }
        public YesOrNoEnum IsReceipt { get; set; }
    }
}
