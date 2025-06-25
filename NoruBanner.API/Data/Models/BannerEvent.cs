namespace NoruBanner.API.Models
{
	public class BannerEvent : BaseEntity
	{
		public Guid EventTypeId { get; set; }
		public EventType EventType { get; set; }

		public string BannerId { get; set; }
		public string UserId { get; set; }
	}
}
