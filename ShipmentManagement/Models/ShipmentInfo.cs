//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShipmentManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShipmentInfo
    {
        public System.Guid ConsignmentNo { get; set; }
        public string ShipperName { get; set; }
        public string ShipperContact { get; set; }
        public string ShipperAddress { get; set; }
        public string RecipientName { get; set; }
        public string RecipientContact { get; set; }
        public string RecipientAddress { get; set; }
        public string ProcessedBranch { get; set; }
        public string PickupBranch { get; set; }
        public System.DateTime DOPlacingOrder { get; set; }
        public System.DateTime ExpectedDeliveryDate { get; set; }
        public string Status { get; set; }
    }
}
