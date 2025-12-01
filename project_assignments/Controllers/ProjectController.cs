using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_assignments.Infrastructure.Repository;
using project_assignments.Models;

namespace project_assignments.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IRepositoryWrapper _context;

        public ProjectController(IRepositoryWrapper context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            List<Project>? projects = await _context.Projects.FindAllAsync(p => p.Team) as List<Project>;
            return View(projects);
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            Project? project = await _context.Projects.FindOneAsync(p => p.ProjectId == id, p => p.Team);
            if (project == null) return NotFound();

            return View(project);
        }

        // GET: Project/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TeamId"] = new SelectList(await _context.Teams.FindAllAsync(), "TeamId", "TeamName");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectCode,ProjectName,TeamId,IsBillable,Status,StartDate,EndDate,CreatedAt,UpdatedAt")] Project project)
        {
            if (ModelState.IsValid)
            {
                await _context.Projects.CreateAsync(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<Team>? teams = await _context.Teams.FindAllAsync() as List<Team>;

            var teamList = teams?.Select(team => new
            {
                team.TeamId,
                DisplayText = $"{team.TeamCode} - {team.TeamName}"
            });

            ViewData["TeamId"] = new SelectList(teamList, "TeamId", "DisplayText", project.TeamId);

            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Project? project = await _context.Projects.FindOneAsync(p => p.ProjectId == id);
            if (project == null) return NotFound();

            List<Team>? teams = await _context.Teams.FindAllAsync() as List<Team>;

            var teamList = teams?.Select(team => new
            {
                team.TeamId,
                DisplayText = $"{team.TeamCode} - {team.TeamName}"
            });

            ViewData["TeamId"] = new SelectList(teamList, "TeamId", "DisplayText", project.TeamId);

            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectCode,ProjectName,TeamId,IsBillable,Status,StartDate,EndDate,CreatedAt,UpdatedAt")] Project project)
        {
            if (id != project.ProjectId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Projects.UpdateAsync(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProjectExists(project.ProjectId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            List<Team>? teams = await _context.Teams.FindAllAsync() as List<Team>;

            var teamList = teams?.Select(team => new
            {
                team.TeamId,
                DisplayText = $"{team.TeamCode} - {team.TeamName}"
            });

            ViewData["TeamId"] = new SelectList(teamList, "TeamId", "DisplayText", project.TeamId);
            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project? project = await _context.Projects.FindOneAsync(p => p.ProjectId == id, p => p.Team);
            if (project == null) return NotFound();

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Project? project = await _context.Projects.FindOneAsync(p => p.ProjectId == id);
            if (project != null) await _context.Projects.DeleteAsync(project);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProjectExists(int id)
        {
            return await _context.Projects.ExistsAsync(p => p.ProjectId == id);
        }
    }
}
