using Microsoft.AspNetCore.Mvc;
using ProiectDRX.DTOs;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintsController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public ComplaintsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var complaints = await _uow.Complaints.GetAllAsync();
        return Ok(complaints);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var complaint = await _uow.Complaints.GetByIdAsync(id);

        if (complaint == null)
        {
            return NotFound("Reclamația nu există.");
        }

        return Ok(complaint);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComplaintCreateDTO dto)
    {
        var complaint = new Complaint
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = "NEW",
            AssetId = dto.AssetId,
            EmplId = dto.EmplId
        };

        await _uow.Complaints.AddAsync(complaint);
        await _uow.SaveAsync();

        var workflow = new ComplaintWorkflow
        {
            ComplaintId = complaint.ComplaintId,
            EmplId = dto.EmplId,
            OldStatus = null,
            CurrentStatus = "NEW",
            ChangedAt = DateTime.Now
        };

        await _uow.Workflows.AddAsync(workflow);
        await _uow.SaveAsync();

        return Ok(new
        {
            complaintId = complaint.ComplaintId,
            title = complaint.Title,
            description = complaint.Description,
            status = complaint.Status,
            assetId = complaint.AssetId,
            emplId = complaint.EmplId
        });
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        UpdateComplaintStatusDTO dto
    )
    {
        var complaint = await _uow.Complaints.GetByIdAsync(id);

        if (complaint == null)
        {
            return NotFound("Reclamația nu există.");
        }

        var oldStatus = complaint.Status;

        complaint.Status = dto.Status;

        var workflow = new ComplaintWorkflow
        {
            ComplaintId = complaint.ComplaintId,
            EmplId = dto.EmplId,
            OldStatus = oldStatus,
            CurrentStatus = dto.Status,
            ChangedAt = DateTime.Now
        };

        await _uow.Workflows.AddAsync(workflow);
        await _uow.SaveAsync();

        return Ok(new
        {
            message = "Status actualizat.",
            complaintId = complaint.ComplaintId,
            status = complaint.Status
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var complaint = await _uow.Complaints.GetByIdAsync(id);

        if (complaint == null)
        {
            return NotFound("Reclamația nu există.");
        }

        _uow.Complaints.Delete(complaint);
        await _uow.SaveAsync();

        return NoContent();
    }
}