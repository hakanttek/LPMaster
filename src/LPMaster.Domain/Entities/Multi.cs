using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class Multi : IVerifiable
{
    public required double Coef { get; init; }
    
    public int? ColIndex { get; init; }

    public DVar? DVar { get; init; }

    public bool IsConstant => DVar is null;

    public int ModelId => Expression.ModelId;

    public Model Model => Expression.Model;

    public required int ExpressionId { get; init; }

    [ForeignKey(nameof(ExpressionId))]
    public required Expression Expression { get; init; }

    public bool Verified
        => Expression.Id == ExpressionId
        && (DVar == null || Expression.ModelId == DVar.ModelId)
        &&  ColIndex == DVar?.ColIndex;
}
