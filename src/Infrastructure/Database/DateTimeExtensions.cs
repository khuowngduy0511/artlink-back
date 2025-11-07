namespace Infrastructure.Database;

public static class DateTimeExtensions
{
    /// <summary>
    /// Converts DateTime to UTC kind without changing the actual time value
    /// </summary>
    public static DateTime ToUtcKind(this DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}
