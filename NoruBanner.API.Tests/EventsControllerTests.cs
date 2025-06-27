using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NoruBanner.API.Controllers;
using NoruBanner.API.Data.Repositories;
using NoruBanner.API.DTOs;

public class EventsControllerTests
{
	private readonly Mock<IBannerEventRepository> _mockEventRepository;
	private readonly EventsController _controller;

	public EventsControllerTests()
	{
		_mockEventRepository = new Mock<IBannerEventRepository>();
		_controller = new EventsController(_mockEventRepository.Object);
	}

	[Fact]
	public async Task TrackViewEvent_WhenCalled_ShouldCallCreateViewEventAsyncAndReturnOk()
	{
		// ARRANGE
		var eventDto = new BannerInteractionDto
		{
			BannerId = "banner-123",
			UserId = "user-abc"
		};

		// ACT
		var result = await _controller.TrackViewEvent(eventDto);

		// ASSERT
		result.Should().BeOfType<OkResult>();
		_mockEventRepository.Verify(repo => repo.CreateViewEventAsync(eventDto), Times.Once);
	}

	[Fact]
	public async Task TrackClickEvent_WhenCalled_ShouldCallCreateClickEventAsyncAndReturnOk()
	{
		// ARRANGE
		var eventDto = new BannerInteractionDto
		{
			BannerId = "banner-456",
			UserId = "user-xyz"
		};

		// ACT
		var result = await _controller.TrackClickEvent(eventDto);

		// ASSERT
		result.Should().BeOfType<OkResult>();
		_mockEventRepository.Verify(repo => repo.CreateClickEventAsync(eventDto), Times.Once);
	}
}