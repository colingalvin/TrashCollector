using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public double AccountBalance { get; set; }
        public bool SpecialPickupStatus { get; set; }
        public DateTime SuspendStartDate { get; set; }
        public DateTime SuspendEndDate { get; set; }
        public string RegularPickupDay { get; set; }
        public DateTime AdditionalPickupDate { get; set; }
    }
}
