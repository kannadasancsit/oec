using MediatR;
using RL.Backend.Models;
using RL.Data.DataModels;

namespace RL.Backend.Commands;

public class CreatePlanProcedureUsersCommand : IRequest<ApiResponse<PlanProcedureUsers>>
{

}