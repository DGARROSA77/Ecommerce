using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class OrderAddress : BaseDomainModel{
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Department { get; set; }
    public string? PostalCode { get; set; }
    public string? UserName { get; set; }
    public string? Country { get; set; }
}