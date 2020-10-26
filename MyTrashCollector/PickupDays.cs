using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrashCollector
{
    public static class PickupDays
    {
        public static List<SelectListItem> RegularPickupDays = new List<SelectListItem>()
        {
            new SelectListItem() { Text="Monday", Value="Monday" },
            new SelectListItem() { Text="Tuesday", Value="Tuesday" },
            new SelectListItem() { Text="Wednesday", Value="Wednesday" },
            new SelectListItem() { Text="Thursday", Value="Thursday" },
            new SelectListItem() { Text="Friday", Value="Friday" },
            new SelectListItem() { Text="Saturday", Value="Saturday" },
        };
    }
}
