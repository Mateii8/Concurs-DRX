using Microsoft.AspNetCore.Mvc;
using ProiectDRX.DTOs;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public AssetsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assets = await _uow.Assets.GetAllAsync();
        return Ok(assets.Select(MapToDTO));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var asset = await _uow.Assets.GetByIdAsync(id);
        if (asset == null) return NotFound();
        return Ok(MapToDTO(asset));
    }

    [HttpGet("employee/{emplId}")]
    public async Task<IActionResult> GetByEmployee(int emplId)
    {
        var assets = await _uow.Assets.GetByEmployeeAsync(emplId);
        return Ok(assets.Select(MapToDTO));
    }

    [HttpPost]
    public async Task<IActionResult> Create(AssetCreateDTO dto)
    {
        var asset = new Asset
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            EmplId = dto.EmplId
        };

        await _uow.Assets.AddAsync(asset);
        await _uow.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = asset.AssetId }, MapToDTO(asset));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AssetCreateDTO dto)
    {
        var asset = await _uow.Assets.GetByIdAsync(id);
        if (asset == null) return NotFound();

        asset.Name = dto.Name;
        asset.SerialNumber = dto.SerialNumber;
        asset.EmplId = dto.EmplId;

        _uow.Assets.Update(asset);
        await _uow.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var asset = await _uow.Assets.GetByIdAsync(id);
        if (asset == null) return NotFound();

        _uow.Assets.Delete(asset);
        await _uow.SaveAsync();

        return NoContent();
    }

    private static AssetResponseDTO MapToDTO(Asset a) => new()
    {
        AssetId = a.AssetId,
        Name = a.Name,
        SerialNumber = a.SerialNumber,
        EmployeeName = a.Empl?.Name ?? ""
    };
}
