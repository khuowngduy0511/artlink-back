namespace Application.Commons;

public class CurrentTime
{
    public static DateTime GetCurrentTime => DateTime.UtcNow.ToLocalTime();

    public static long GetTimeStamp()
    {
        DateTimeOffset now = DateTime.UtcNow;
        return now.ToUnixTimeMilliseconds();
    }
}
