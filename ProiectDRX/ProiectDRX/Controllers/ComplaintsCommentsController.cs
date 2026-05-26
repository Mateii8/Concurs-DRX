using Microsoft.AspNetCore.Mvc;
using ProiectDRX.DTOs;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintCommentsController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public ComplaintCommentsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet("{complaintId}")]
    public async Task<IActionResult> GetByComplaintId(
        int complaintId
    )
    {
        var comments =
            await _uow.Comments.GetAllAsync();

        var result = comments
            .Where(c => c.ComplaintId == complaintId)
            .OrderBy(c => c.CreatedAt)
            .ToList();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddReply(
        ComplaintReplyDTO dto
    )
    {
        var complaint =
            await _uow.Complaints.GetByIdAsync(
                dto.ComplaintId
            );

        if (complaint == null)
        {
            return NotFound(
                "Reclamația nu există."
            );
        }

        var comment = new ComplaintComment
        {
            ComplaintId = dto.ComplaintId,

            EmplId = dto.EmplId,

            Message = dto.Message,

            CreatedAt = DateTime.Now
        };

        await _uow.Comments
            .AddAsync(comment);

        await _uow.SaveAsync();

        return Ok(new
        {
            message = "Răspuns trimis.",

            complaintId =
                comment.ComplaintId,

            emplId =
                comment.EmplId,

            text =
                comment.Message,

            createdAt =
                comment.CreatedAt
        });
    }
}