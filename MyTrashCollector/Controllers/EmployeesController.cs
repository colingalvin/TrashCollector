using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTrashCollector.Data;
using MyTrashCollector.Models;
using SQLitePCL;

namespace MyTrashCollector.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInEmployee = _context.Employees.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (loggedInEmployee == null)
            {
                return RedirectToAction("Create");
            }
            var customers = GetDailyCustomers(loggedInEmployee);
            return View("ViewDailyCustomers", customers);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> ViewAllCustomers(int? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInEmployee = _context.Employees.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            var customers = _context.Customers.Include(c => c.Address).Where(c => c.Address.AddressZip == loggedInEmployee.ZipCodeOfResponsibility).ToList();

            return View("ViewAllCustomers", customers);
        }

        //public async Task<IActionResult> ViewDailyCustomers(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees
        //        .FirstOrDefaultAsync(m => m.EmployeeId == id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    var customers = GetDailyCustomers(employee);

        //    return View("ViewCustomers", customers);
        //}

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                employee.IdentityUserId = userId;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Customers
        public async Task<IActionResult> CustomerDetails(int? id)
        {
            var customer = _context.Customers.Include(c => c.Address).Where(c => c.CustomerId == id).FirstOrDefault();
            return View("CustomerDetails", customer);
        }

        public async Task<IActionResult> ConfirmPickup(int? id)
        {
            var customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            ChargeCustomer(customer);
            customer.DailyPickupComplete = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private static void ChargeCustomer(Customer customer)
        {
            customer.AccountBalance += 25;
        }

        private List<Customer> GetDailyCustomers(Employee employee)
        {
            var customers = GetRegularCustomers(employee);
            customers = CheckSpecialRequests(customers);
            customers = RemoveCompletedCustomers(customers);
            return customers;
        }

        private List<Customer> GetRegularCustomers(Employee employee)
        {
            List<Customer> customers = new List<Customer>();
            foreach (Customer customer in _context.Customers.Include(c => c.Address))
            {
                if (customer.Address.AddressZip == employee.ZipCodeOfResponsibility && customer.RegularPickupDay == DateTime.Now.DayOfWeek.ToString() && customer.DailyPickupComplete == false)
                {
                    customers.Add(customer);
                }
            }
            return customers;
        }

        private List<Customer> CheckSpecialRequests(List<Customer> customers)
        {
            customers = SpecialPickupScheduledToday(customers);
            customers = CheckForSuspendedService(customers);
            return customers;
        }

        private List<Customer> SpecialPickupScheduledToday(List<Customer> customers)
        {
            foreach (Customer customer in _context.Customers)
            {
                if (customer.SpecialPickupStatus == true)
                {
                    if (customer.AdditionalPickupDate.Equals(DateTime.Now.Date))
                    {
                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }

        private List<Customer> CheckForSuspendedService(List<Customer> customers)
        {
            foreach (Customer customer in customers.ToList())
            {
                if (customer.SuspendStartDate?.CompareTo(DateTime.Now.Date) <= 0 && customer.SuspendEndDate?.CompareTo(DateTime.Now.Date) > 0)
                {
                    customers.Remove(customer);
                }
            }
            return customers;
        }

        private List<Customer> RemoveCompletedCustomers(List<Customer> customers)
        {
            foreach (Customer customer in customers.ToList())
            {
                if (customer.DailyPickupComplete == true)
                {
                    customers.Remove(customer);
                }
            }
            return customers;
        }
    }
}
