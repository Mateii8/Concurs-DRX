namespace ProiectDRX.DTOs;

public class AssetResponseDTO
{
    public int AssetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
}