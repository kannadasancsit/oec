using System.ComponentModel.DataAnnotations;
using RL.Data.DataModels.Common;

namespace RL.Data.DataModels;

public class Plan : IChangeTrackable
{
    public Plan()
    {
        PlanProcedures = new List<PlanProcedure>();
        PlanProceduresUsers = new List<PlanProcedureUsers>();

    }

    [Key]
    public int PlanId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public virtual ICollection<PlanProcedure> PlanProcedures { get; set; }

    public virtual ICollection<PlanProcedureUsers> PlanProceduresUsers { get; set; }
}
