using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class Order : BaseDomainModel{

    public string? BuyerName { get; set; }
    public string? BuyerUserName { get; set; }
    public OrderAddress? OrderAddress { get; set; }
    public IReadOnlyList <OrderItem>? OrderItems { get; set; }
    [Column(TypeName ="decimal(10,2)")]
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [Column(TypeName ="decimal(10,2)")]
    public decimal Total { get; set; }
    [Column(TypeName ="decimal(10,2)")]
    public decimal Tax { get; set; }
    [Column(TypeName ="decimal(10,2)")]
    public decimal ShippingPrice { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public string? StripeApiKey { get; set; }

    public Order(
        string buyerName,
        string buyerEmail,
        OrderAddress orderAddress,
        decimal subtotal,
        decimal total,
        decimal tax,
        decimal shippingPrice
    ) {
        BuyerName = buyerName;
        BuyerUserName = buyerEmail;
        OrderAddress = orderAddress;
        Subtotal = subtotal;
        Total = total;
        Tax = tax;
        ShippingPrice =  shippingPrice;
    }

}