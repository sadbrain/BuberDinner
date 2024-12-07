using BuberDinner.Application.Common.Interfaces.Services;

namespace BuberDinner.Infrastructure.Common.Services;

public class DateTimeProvider : IDateTimeProvider
{
   DateTime UtcNow { get; }
}
