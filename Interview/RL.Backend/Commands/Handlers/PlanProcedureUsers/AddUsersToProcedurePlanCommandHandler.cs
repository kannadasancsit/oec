using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.PlanProcedureUsers;

public class AddUsersToProcedurePlanCommandHandler : IRequestHandler<AddUsersToProcedurePlanCommand, ApiResponse<Unit>>
{
    private readonly RLContext _context;

    public AddUsersToProcedurePlanCommandHandler(RLContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<Unit>> Handle(AddUsersToProcedurePlanCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Validate request
            if (request.PlanId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid PlanId"));
            if (request.ProcedureId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));
            if (request.UserId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid UserId"));

            var plan = await _context.Plans
                .Include(p => p.PlanProcedures)
                .FirstOrDefaultAsync(p => p.PlanId == request.PlanId);
            var procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.ProcedureId == request.ProcedureId);
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == request.UserId);
            
            if (plan is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"PlanId: {request.PlanId} not found"));
            if (procedure is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"ProcedureId: {request.ProcedureId} not found"));
            if (user is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"UserId: {request.UserId} not found"));

            //Already has the procedure, so just succeed
            var planProcedureUser = await _context.PlanProceduresUsers.FirstOrDefaultAsync(p => p.PlanId == request.PlanId && p.ProcedureId==request.ProcedureId && p.UserId==request.UserId);
            if (planProcedureUser!=null)
                return ApiResponse<Unit>.Succeed(new Unit());

            _context.PlanProceduresUsers.Add(new Data.DataModels.PlanProcedureUsers
            {
                ProcedureId = procedure.ProcedureId,
                PlanId = plan.PlanId,
                UserId = user.UserId,
            });

            await _context.SaveChangesAsync();

            return ApiResponse<Unit>.Succeed(new Unit());
        }
        catch (Exception e)
        {
            return ApiResponse<Unit>.Fail(e);
        }
    }
}