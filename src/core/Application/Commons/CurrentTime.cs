namespace Application.Commons;

public class CurrentTime
{
    // PostgreSQL requires UTC DateTime for 'timestamp with time zone'
    // Changed from ToLocalTime() to UTC to fix PostgreSQL compatibility
    public static DateTime GetCurrentTime => DateTime.UtcNow;

    public static long GetTimeStamp()
    {
        DateTimeOffset now = DateTime.UtcNow;
        return now.ToUnixTimeMilliseconds();
    }
}
