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

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var loggedInCustomer = _context.Customers.Include(c => c.Address).FirstOrDefault(c => c.IdentityUserId == userId);
            var loggedInCustomer = _context.Customers.Include(c => c.Address).Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if(loggedInCustomer == null)
            {
                return RedirectToAction("Create");
            }
            return View(loggedInCustomer);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(c => c.Address).FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            string apiKey = "API_KEY_HERE";
            string fullURL = "https://maps.googleapis.com/maps/api/geocode/json?address=" + formattedAddress + "&key=" + apiKey;
            var response = await httpClient.GetAsync(fullURL);

            //if(response.IsSuccessStatusCode)
            //{
                var task = response.Content.ReadAsStringAsync().Result;
                JObject mapsData = JsonConvert.DeserializeObject<JObject>(task);
                address.Latitude = Convert.ToDouble(mapsData["results"][0]["geometry"]["location"]["lat"]);
                address.Longitude = Convert.ToDouble(mapsData["results"][0]["geometry"]["location"]["lng"]);
            //}
            
            return address;
        }

        private string ParseAddress(Address address)
        {
            StringBuilder stringBuilder = new StringBuilder();
            // Iterate through list of selected properties?
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

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            //var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        // GET: Customers/Edit/5
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

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    customer.SpecialPickupStatus = true;
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

        // GET: Customers/Edit/5
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

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    customer.SpecialPickupStatus = true;
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
