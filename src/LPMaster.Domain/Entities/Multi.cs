namespace LPMaster.Domain.Entities;

public class Multi
{
    public double Coef { get; init; }
    
    public double? DVarId { get; init; }

    public required DVar? DVar { get; init; }

    public int ExpressionId { get; init; }

    public bool IsConstant => DVarId is null;
}
