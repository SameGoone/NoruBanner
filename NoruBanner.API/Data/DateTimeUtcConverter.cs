using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NoruBanner.API.Data
{
	public class DateTimeUtcConverter : ValueConverter<DateTime, DateTime>
	{
		public DateTimeUtcConverter()
			: base(
				x => x.ToUniversalTime(), // Сохранение в БД в UTC
				x => x.ToLocalTime()) // Чтение из БД в Local
		{
		}
	}
}
