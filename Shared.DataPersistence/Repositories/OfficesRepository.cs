﻿using Shared.Core.Domain.Entities;
using Shared.Core.Repositories;
using Shared.DataPersistence.Data.Db;

namespace Shared.DataPersistence.Repositories;

public class OfficesRepository : GenericRepository<Office>, IOfficesRepository
{
    public OfficesRepository(ApplicationDbContext context) : base(context) { }
}