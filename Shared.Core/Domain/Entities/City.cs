﻿namespace Shared.Core.Domain.Entities;

public class City : IBaseEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}