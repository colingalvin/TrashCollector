﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrashCollector.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Display(Name = "Street Number")]
        public int StreetNumber { get; set; }
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        [Display(Name = "City")]
        public string AddressCity { get; set; }
        [Display(Name = "State")]
        public string AddressState { get; set; }
        [Display(Name = "Zip Code")]
        public int AddressZip { get; set; }
    }
}