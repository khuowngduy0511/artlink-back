namespace Infrastructure.Database;

public static class DatabaseSqlHelper
{
    /// <summary>
    /// Returns SQL function to generate a new GUID for SQL Server
    /// </summary>
    public static string NewGuid()
    {
        return "NEWID()";
    }

    /// <summary>
    /// Returns SQL function to get current UTC date/time for SQL Server
    /// </summary>
    public static string GetUtcDate()
    {
        return "GETUTCDATE()";
    }
}
