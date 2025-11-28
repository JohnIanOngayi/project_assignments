using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_assignments.Infrastructure.Repository;
using project_assignments.Models;

namespace project_assignments.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IRepositoryWrapper _context;

        public DepartmentController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.FindAllAsync() as List<Department>);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            Department? department = await _context.Departments.FindOneAsync(m => m.DepartmentId == id);
            if (department == null) return NotFound();

            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentCode,DepartmentName,Status,CreatedAt,UpdatedAt")] Department department)
        {
            if (ModelState.IsValid)
            {
                await _context.Departments.CreateAsync(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Department? department = await _context.Departments.FindOneAsync(d => d.DepartmentId == id);
            if (department == null) return NotFound();

            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentCode,DepartmentName,Status,CreatedAt,UpdatedAt")] Department department)
        {
            if (id != department.DepartmentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Departments.UpdateAsync(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartmentExists(department.DepartmentId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department? department = await _context.Departments.FindOneAsync(d => d.DepartmentId == id);
            if (department == null) return NotFound();

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Department? department = await _context.Departments.FindOneAsync(d => d.DepartmentId == id);

            if (department != null) await _context.Departments.DeleteAsync(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DepartmentExists(int id)
        {
            return await _context.Departments.ExistsAsync(d => d.DepartmentId == id);
        }
    }
}
