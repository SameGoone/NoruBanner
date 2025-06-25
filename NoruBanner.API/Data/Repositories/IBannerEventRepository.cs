using NoruBanner.API.DTOs;
using NoruBanner.API.Models;

namespace NoruBanner.API.Data.Repositories;

public interface IBannerEventRepository
{
	Task CreateViewEventAsync(BannerInteractionDto eventDto);
	Task CreateClickEventAsync(BannerInteractionDto eventDto);
}