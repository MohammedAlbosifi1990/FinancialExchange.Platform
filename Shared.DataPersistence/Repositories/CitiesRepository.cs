using Shared.Core.Domain.Entities;
using Shared.Core.Repositories;
using Shared.DataPersistence.Data.Db;

namespace Shared.DataPersistence.Repositories;

public class CitiesRepository : GenericRepository<City>, ICitiesRepository
{
    public CitiesRepository(ApplicationDbContext context) : base(context) { }
}