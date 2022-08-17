using ServiceManagerBackEnd.Interfaces.Services;

namespace ServiceManagerBackEnd.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime TokenExpireDate()
    {
        return DateTime.UtcNow.AddDays(7);
    }
}