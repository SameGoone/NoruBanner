using Microsoft.AspNetCore.Mvc;
using NoruBanner.API.Data.Repositories;
using NoruBanner.API.DTOs;

namespace NoruBanner.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EventsController : ControllerBase
	{
		private readonly IBannerEventRepository _eventRepository;

		public EventsController(IBannerEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		[HttpPost("view")]
		public async Task<IActionResult> TrackViewEvent([FromBody] BannerInteractionDto eventDto)
		{
			await _eventRepository.CreateViewEventAsync(eventDto);
			return Ok();
		}

		[HttpPost("click")]
		public async Task<IActionResult> TrackClickEvent([FromBody] BannerInteractionDto eventDto)
		{
			await _eventRepository.CreateClickEventAsync(eventDto);
			return Ok();
		}
	}
}