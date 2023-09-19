using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanProcedureUsersController : ControllerBase
{
    private readonly ILogger<PlanProcedureUsersController> _logger;
    private readonly RLContext _context;

    public PlanProcedureUsersController(ILogger<PlanProcedureUsersController> logger, RLContext context)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<PlanProcedureUsers> Get()
    {
        return _context.PlanProceduresUsers;
    }
}
