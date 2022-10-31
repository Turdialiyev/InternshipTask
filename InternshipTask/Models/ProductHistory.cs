using System.ComponentModel;
using System.Text.Json.Serialization;

namespace InternshipTask.Models;

public class ProductHistory
{
    public ulong Id { get; set; }
    public string? Title { get; set; }
    public int Quantiy { get; set; }
    public double Price { get; set; }
    public bool? IsDeleted { get; set; }
    public decimal TotalPrice { get; set; }
    [JsonIgnore]
    public ulong ProductId { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public DateTime? DaletedAt { get; set; }
}