using Microsoft.AspNetCore.Mvc;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintsWorkFlowsController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public ComplaintsWorkFlowsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet("complaint/{complaintId}")]
    public async Task<IActionResult> GetByComplaint(int complaintId)
    {
        var workflows = await _uow.Workflows.GetByComplaintAsync(complaintId);
        var result = workflows.Select(w => new
        {
            w.WorkflowId,
            w.ComplaintId,
            w.OldStatus,
            w.CurrentStatus,
            w.ChangedAt,
            ChangedBy = w.Empl?.Name ?? ""
        });
        return Ok(result);
    }

    [HttpGet("complaint/{complaintId}/latest")]
    public async Task<IActionResult> GetLatest(int complaintId)
    {
        var workflow = await _uow.Workflows.GetLatestByComplaintAsync(complaintId);
        if (workflow == null) return NotFound();
        return Ok(new
        {
            workflow.WorkflowId,
            workflow.ComplaintId,
            workflow.OldStatus,
            workflow.CurrentStatus,
            workflow.ChangedAt,
            ChangedBy = workflow.Empl?.Name ?? ""
        });
    }
}
