using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_assignments.Infrastructure.Repository;
using project_assignments.Models;

namespace project_assignments.Controllers
{
    public class TeamController : Controller
    {
        private readonly IRepositoryWrapper _context;

        public TeamController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
            List<Team>? teams = await _context.Teams.FindAllAsync(t => t.Department) as List<Team>;
            return View(teams);
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            Team? team = await _context.Teams.FindOneAsync(t => t.TeamId == id, t => t.Department);
            if (team == null) return NotFound();

            return View(team);
        }

        // GET: Team/Create
        public async Task<IActionResult> Create()
        {
            List<Department>? departments = await _context.Departments.FindAllAsync() as List<Department>;

            var departmentList = departments?.Select(dept => new 
            {
                dept.DepartmentId,
                DisplayText = $"{dept.DepartmentCode} - {dept.DepartmentName}"
            });
            
            ViewData["DepartmentId"] = new SelectList(departmentList, "DepartmentId", "DisplayText");

            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,TeamCode,TeamName,Status,DepartmentId,CreatedAt,UpdatedAt")] Team team)
        {
            if (ModelState.IsValid)
            {
                await _context.Teams.CreateAsync(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<Department>? departments = await _context.Departments.FindAllAsync() as List<Department>;

            var departmentList = departments?.Select(dept => new
            {
                dept.DepartmentId,
                DisplayText = $"{dept.DepartmentCode} - {dept.DepartmentName}"
            });

            ViewData["DepartmentId"] = new SelectList(departmentList, "DepartmentId", "DisplayText");

            return View(team);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var team = await _context.Teams.FindOneAsync(t => t.TeamId == id);
            if (team == null) return NotFound();

            List<Department>? departments = await _context.Departments.FindAllAsync() as List<Department>;

            var departmentList = departments?.Select(dept => new
            {
                dept.DepartmentId,
                DisplayText = $"{dept.DepartmentCode} - {dept.DepartmentName}"
            });

            ViewData["DepartmentId"] = new SelectList(departmentList, "DepartmentId", "DisplayText");

            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,TeamCode,TeamName,Status,DepartmentId,CreatedAt,UpdatedAt")] Team team)
        {
            if (id != team.TeamId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Teams.UpdateAsync(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TeamExists(team.TeamId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.FindAllAsync(), "DepartmentId", "DepartmentCode", team.DepartmentId);
            return View(team);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team? team = await _context.Teams.FindOneAsync(t => t.TeamId == id, t => t.Department);
            if (team == null) return NotFound();

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindOneAsync(t => t.TeamId == id);
            if (team != null) await _context.Teams.DeleteAsync(team);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TeamExists(int id)
        {
            return await _context.Teams.ExistsAsync(t => t.TeamId == id);
        }
    }
}