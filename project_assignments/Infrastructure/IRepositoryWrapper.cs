using project_assignments.Infrastructure.Contracts;

namespace project_assignments.Infrastructure
{
    public interface IRepositoryWrapper
    {
        IDepartmentRepository Departments { get; }
        ITeamRepository Teams { get; }
        IProjectRepository Projects { get; }
        IEmployeeRepository Employees { get; }
        IProjectAssignmentRepository ProjectAssignments { get; }
    }
}
