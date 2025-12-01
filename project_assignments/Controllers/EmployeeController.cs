using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_assignments.Infrastructure.Repository;
using project_assignments.Models;

namespace project_assignments.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepositoryWrapper _context;

        public EmployeeController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.FindAllAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeCode,EmployeeName,EmployeeEmail,IsActive,CreatedAt,UpdatedAt")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _context.Employees.CreateAsync(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeCode,EmployeeName,EmployeeEmail,IsActive,CreatedAt,UpdatedAt")] Employee employee)
        {
            if (id != employee.EmployeeId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Employees.UpdateAsync(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EmployeeExists(employee.EmployeeId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Employee? employee = await _context.Employees.FindOneAsync(e => e.EmployeeId == id);
            if (employee != null) await _context.Employees.DeleteAsync(employee);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await _context.Employees.ExistsAsync(e => e.EmployeeId == id);
        }
    }
}
