using project_assignments.Infrastructure.Contracts;
using project_assignments.Infrastructure.Entities;

namespace project_assignments.Infrastructure.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext? _repositoryContext;
        private IDepartmentRepository? _departmentRepository;
        private ITeamRepository? _teamRepository;
        private IProjectRepository? _projectRepository;
        private IEmployeeRepository? _employeeRepository;
        private IProjectAssignmentRepository? _projectAssignmentRepository;
        public IDepartmentRepository Departments
        {
            get
            {
                _departmentRepository ??= new DepartmentRepository(_repositoryContext);
                return _departmentRepository;
            }
        }

        public ITeamRepository Teams
        {
            get
            {
                _teamRepository ??= new TeamRepository(_repositoryContext);
                return _teamRepository;
            }
        }

        public IProjectRepository Projects
        {
            get
            {
                _projectRepository ??= new ProjectRepository(_repositoryContext);
                return _projectRepository;
            }
        }

        public IEmployeeRepository Employees
        {
            get
            {
                _employeeRepository ??= new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }

        public IProjectAssignmentRepository ProjectAssignments
        {
            get
            {
                _projectAssignmentRepository ??= new ProjectAssignmentRepository(_repositoryContext);
                return _projectAssignmentRepository;
            }
        }

        public void SaveChanges()
        {
            int _ = _repositoryContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            int _ = await _repositoryContext.SaveChangesAsync();
        }
    }
}
