using System;
using System.Collections.Generic;
using System.Text;

namespace ZZAPP.Models
{
    public class CouponInfo
    {       
        public string CouponName  { get; set; }      
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }    
        public string Barcode { get; set; }
        public bool FreeUse { get; set; }
        public string Note { get; set; }
        public string ImageUrl { get; set; }
    }
}
