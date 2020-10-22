using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using MyTrashCollector.ActionFilters;
using MyTrashCollector.Data;
using MyTrashCollector.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyTrashCollector.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static readonly HttpClient httpClient;

        static CustomersController()
        {
            httpClient = new HttpClient();
        }

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var loggedInCustomer = _context.Customers.Include(c => c.Address).FirstOrDefault(c => c.IdentityUserId == userId); - DIFFERENCE?
            var loggedInCustomer = _context.Customers.Include(c => c.Address).Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if(loggedInCustomer == null)
            {
                return RedirectToAction("Create");
            }
            return View(loggedInCustomer);
        }

        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            customer.IdentityUserId = userId;
            customer.Address = await GeocodeAddress(customer.Address);
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Address> GeocodeAddress(Address address)
        {
            string formattedAddress = ParseAddress(address);
            Uri fullURL = new Uri("https://maps.googleapis.com/maps/api/geocode/json?address=" + formattedAddress + "&key=" + APIKeys.GOOGLE_API_KEY);
            var response = await httpClient.GetAsync(fullURL);

            if(response.IsSuccessStatusCode)
            {
                var task = response.Content.ReadAsStringAsync().Result;
                JObject mapsData = JsonConvert.DeserializeObject<JObject>(task);
                address.Latitude = Convert.ToDouble(mapsData["results"][0]["geometry"]["location"]["lat"]);
                address.Longitude = Convert.ToDouble(mapsData["results"][0]["geometry"]["location"]["lng"]);
            }
            
            return address;
        }

        private string ParseAddress(Address address)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < address.StreetAddress.Length; i++)
            {
                if (address.StreetAddress[i] == ' ')
                {
                    stringBuilder.Append("+");
                }
                else
                {
                    stringBuilder.Append(address.StreetAddress[i]);
                }
            }
            stringBuilder.Append(",+");
            for (int i = 0; i < address.AddressCity.Length; i++)
            {
                if (address.AddressCity[i] == ' ')
                {
                    stringBuilder.Append("+");
                }
                else
                {
                    stringBuilder.Append(address.AddressCity[i]);
                }
            }
            stringBuilder.Append(",+");
            for (int i = 0; i < address.AddressState.Length; i++)
            {
                if (address.AddressState[i] == ' ')
                {
                    stringBuilder.Append("+");
                }
                else
                {
                    stringBuilder.Append(address.AddressState[i]);
                }
            }
            return stringBuilder.ToString();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.Address = await GeocodeAddress(customer.Address);
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        public async Task<IActionResult> AddPickup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPickup(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (customer.AdditionalPickupDate != null)
                    {
                        customer.SpecialPickupStatus = true;
                    }
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        public async Task<IActionResult> SuspendService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuspendService(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (customer.SuspendStartDate != null || customer.SuspendEndDate != null)
                    {
                        customer.SpecialPickupStatus = true;
                    }
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }
    }
}
