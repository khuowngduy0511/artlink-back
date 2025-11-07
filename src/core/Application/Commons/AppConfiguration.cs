using Application.AppConfigurations;

namespace Application.Commons;

public class AppConfiguration
{
    public ConnectionStrings ConnectionStrings { get; set; } = default!;
    public JwtConfiguration JwtConfiguration { get; set; } = default!;
    public FirebaseConfiguration FirebaseConfiguration { get; set; } = default!;
    public GoogleStorageConfiguration GoogleStorageConfiguration { get; set; } = default!;
    public ThirdAuthentication ThirdAuthentication { get; set; } = default!;
    public ZaloPayConfiguration ZaloPayConfiguration { get; set; } = default!;
    public PayOSConfiguration PayOSConfiguration { get; set; } = default!;
    public EmailSetting EmailSetting { get; set; } = default!;

}
