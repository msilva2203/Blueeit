using Blueeit.Api.Models;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Repositories;

public interface IUserActivityRepository
{
    UserActivity? GetById(int id);

    Task<UserActivity?> GetByIdAsync(int id);

    IEnumerable<UserActivity> GetAll();

    Task<IEnumerable<UserActivity>> GetAllAsync();

    int GetCount();

    Task<int> GetCountAsync();

    UserActivity Create(CreateUserActivityData data);

    Task<UserActivity> CreateAsync(CreateUserActivityData data);

    bool DeleteById(int id);

    Task<bool> DeleteByIdAsync(int id);
}