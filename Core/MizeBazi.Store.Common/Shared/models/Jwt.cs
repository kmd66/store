namespace MizeBazi.Store.Common.Shared;

public record TokenRequest(string Token);
public class Jwt
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime Expiry { get; set; }
    public string DeviceId { get; set; }
    public long UserId { get; set; }
    public UserRoles Role { get; set; }
}

