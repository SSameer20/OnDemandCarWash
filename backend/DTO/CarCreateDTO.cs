using System.Text.Json.Serialization;

namespace backend.DTO;
public class CarCreateDTO{
    public string LicensePlate { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int UserId { get; set; }

    public ICollection<CarImageDTO> CarImages { get; set; } = new List<CarImageDTO>();
     
}