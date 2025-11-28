using project_assignments.Infrastructure.Contracts;
using project_assignments.Models;

namespace project_assignments.Infrastructure.Entities
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
