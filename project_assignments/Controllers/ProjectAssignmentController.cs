using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_assignments.Infrastructure;
using project_assignments.Models;

namespace project_assignments.Controllers
{
    public class ProjectAssignmentController : Controller
    {
        private readonly RepositoryContext _context;

        public ProjectAssignmentController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: ProjectAssignment
        public async Task<IActionResult> Index()
        {
            var repositoryContext = _context.ProjectAssignments.Include(p => p.Employee).Include(p => p.Project);
            return View(await repositoryContext.ToListAsync());
        }

        // GET: ProjectAssignment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssignment = await _context.ProjectAssignments
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (projectAssignment == null)
            {
                return NotFound();
            }

            return View(projectAssignment);
        }

        // GET: ProjectAssignment/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectCode");
            return View();
        }

        // POST: ProjectAssignment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentId,ProjectId,EmployeeId,RoleOnProject,AllocationPercent,StartDate,EndDate,Status,CreatedAt,UpdatedAt")] ProjectAssignment projectAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode", projectAssignment.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectCode", projectAssignment.ProjectId);
            return View(projectAssignment);
        }

        // GET: ProjectAssignment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssignment = await _context.ProjectAssignments.FindAsync(id);
            if (projectAssignment == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode", projectAssignment.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectCode", projectAssignment.ProjectId);
            return View(projectAssignment);
        }

        // POST: ProjectAssignment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentId,ProjectId,EmployeeId,RoleOnProject,AllocationPercent,StartDate,EndDate,Status,CreatedAt,UpdatedAt")] ProjectAssignment projectAssignment)
        {
            if (id != projectAssignment.AssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectAssignmentExists(projectAssignment.AssignmentId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeCode", projectAssignment.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectCode", projectAssignment.ProjectId);
            return View(projectAssignment);
        }

        // GET: ProjectAssignment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectAssignment = await _context.ProjectAssignments
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (projectAssignment == null)
            {
                return NotFound();
            }

            return View(projectAssignment);
        }

        // POST: ProjectAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectAssignment = await _context.ProjectAssignments.FindAsync(id);
            if (projectAssignment != null)
            {
                _context.ProjectAssignments.Remove(projectAssignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectAssignmentExists(int id)
        {
            return _context.ProjectAssignments.Any(e => e.AssignmentId == id);
        }
    }
}
