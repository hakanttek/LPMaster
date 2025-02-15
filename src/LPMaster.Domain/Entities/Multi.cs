using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class Multi
{
    public double Coef { get; init; }
    
    public double? DVarId { get; init; }

    [ForeignKey(nameof(DVarId))]
    public DVar? DVar { get; init; }

    public int ExpressionId { get; init; }

    public bool IsConstant => DVar is null;

    public int? ModelId => DVar?.ModelId;

    public Model? Model => DVar?.Model;
}
