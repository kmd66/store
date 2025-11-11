namespace MizeBazi.Store.Common.Shared;

public class DataSyncEvent
{
    public long Id { get; set; }
    public Guid UnicId { get; set; }
    public string EntitieName { get; set; }
    public EventType Type { get; set; }
    public byte ExceptionCode { get; set; } = 0;
}
