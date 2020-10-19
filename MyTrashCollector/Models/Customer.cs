using Microsoft.AspNetCore.Identity;
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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [Display(Name = "Account Balance")]
        public double AccountBalance { get; set; }
        public bool SpecialPickupStatus { get; set; }
        [Display(Name = "Start Date for Suspend Service")]
        public DateTime SuspendStartDate { get; set; }
        [Display(Name = "End Date for Suspend Service")]
        public DateTime SuspendEndDate { get; set; }
        [Display(Name = "Regular Pickup Day")]
        public string RegularPickupDay { get; set; }
        [Display(Name = "Additional Pickup Date")]
        public DateTime AdditionalPickupDate { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
