using NoruBanner.API.Common;
using NoruBanner.API.DTOs;
using NoruBanner.API.Models;

namespace NoruBanner.API.Data.Repositories;

public class BannerEventRepository : IBannerEventRepository
{
	private readonly AppDbContext _context;

	public BannerEventRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task CreateViewEventAsync(BannerInteractionDto eventDto)
	{
		var newEvent = new BannerEvent
		{
			EventTypeId = Constants.EventTypes.View,
			BannerId = eventDto.BannerId,
			UserId = eventDto.UserId
		};
		_context.BannerEvents.Add(newEvent);
		await _context.SaveChangesAsync();
	}

	public async Task CreateClickEventAsync(BannerInteractionDto eventDto)
	{
		var newEvent = new BannerEvent
		{
			EventTypeId = Constants.EventTypes.Click,
			BannerId = eventDto.BannerId,
			UserId = eventDto.UserId
		};
		_context.BannerEvents.Add(newEvent);
		await _context.SaveChangesAsync();
	}
}