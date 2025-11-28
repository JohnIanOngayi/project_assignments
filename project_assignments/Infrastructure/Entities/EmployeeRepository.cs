using project_assignments.Infrastructure.Contracts;
using project_assignments.Models;

namespace project_assignments.Infrastructure.Entities
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
