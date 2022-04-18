using AutoParts.Domain.Entities;

namespace AutoParts.Application.Repositories;

public interface ICarRepository : IRepository<Car>
{
    Task Delete(string? modelName);
}