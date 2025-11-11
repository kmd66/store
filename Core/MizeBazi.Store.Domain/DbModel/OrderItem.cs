namespace MizeBazi.Store.Domain; 
public class DbOrderItem 
{
    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; } //تعداد واحدهای یک محصول در سفارش

    public decimal Price { get; set; } // قیمت در لحظه خرید

}