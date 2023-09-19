using MediatR;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.PlanProcedureUsers;

public class CreatePlanProcedureUsersCommandHandler : IRequestHandler<CreatePlanProcedureUsersCommand, ApiResponse<PlanProcedureUsers>>
{
    private readonly RLContext _context;

    public CreatePlanProcedureUsersCommandHandler(RLContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<PlanProcedureUsers>> Handle(CreatePlanProcedureUsersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var planProcedureUsers = new PlanProcedureUsers();
            _context.PlanProceduresUsers.Add(planProcedureUsers);

            await _context.SaveChangesAsync();

            return ApiResponse<PlanProcedureUsers>.Succeed(planProcedureUsers);
        }
        catch (Exception e)
        {
            return ApiResponse<PlanProcedureUsers>.Fail(e);
        }
    }
}