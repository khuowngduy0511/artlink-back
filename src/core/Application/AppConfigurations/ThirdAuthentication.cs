namespace Application.AppConfigurations;

public class ThirdAuthentication
{
    public Google Google { get; set; } = default!;
}

public class Google
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}
