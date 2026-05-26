namespace ProiectDRX.DTOs;

public class ComplaintCreateDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AssetId { get; set; }
    public int EmplId { get; set; }
}