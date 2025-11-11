namespace MizeBazi.Store.Domain;

public class DbCartItem 
{
    public long CartId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; } = 1;
    
}

