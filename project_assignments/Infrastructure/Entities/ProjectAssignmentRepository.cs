using project_assignments.Infrastructure.Contracts;
using project_assignments.Models;

namespace project_assignments.Infrastructure.Entities
{
    public class ProjectAssignmentRepository : RepositoryBase<ProjectAssignment>, IProjectAssignmentRepository
    {
        public ProjectAssignmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
