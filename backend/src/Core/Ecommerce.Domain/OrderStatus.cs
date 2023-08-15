using System.Runtime.Serialization;

namespace Ecommerce.Domain;

public enum OrderStatus{
    [EnumMember(Value = "Pendiente")]
    Pending,
    [EnumMember(Value = "El pago  fué recibido")]
    Completed,
    [EnumMember(Value = "El producto fué enviado")]
    Send,
    [EnumMember(Value = "El pago tuvo algún tipo de error")]
    Error,
}