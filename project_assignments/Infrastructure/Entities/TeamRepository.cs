using project_assignments.Infrastructure.Contracts;
using project_assignments.Models;

namespace project_assignments.Infrastructure.Entities
{
    public class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
