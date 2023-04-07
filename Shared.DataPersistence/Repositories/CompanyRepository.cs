using Shared.Core.Domain.Entities;
using Shared.Core.Repositories;
using Shared.DataPersistence.Data.Db;

namespace Shared.DataPersistence.Repositories;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context) { }
}