using project_assignments.Infrastructure.Contracts;
using project_assignments.Models;

namespace project_assignments.Infrastructure.Entities
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
