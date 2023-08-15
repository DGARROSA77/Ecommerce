using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class Review :  BaseDomainModel{

    [Column(TypeName = "NVARCHAR(100)")]
    public string? Name { get; set; }
    
    public int Rating { get; set; }

    [Column(TypeName = "NVARCHAR(4000)")]
    public string? Comment { get; set; }

    public int ProductId  { get; set; }  

}