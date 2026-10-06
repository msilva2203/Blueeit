using Blueeit.Api.DTOs.Stats;
using Blueeit.Api.Mappings;

namespace Blueeit.Api.Services;

public interface IStatsService
{
    Task<StatsResponse> GetStatsAsync();
}