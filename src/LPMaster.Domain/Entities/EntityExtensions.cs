namespace LPMaster.Domain.Entities;

public static class EntityExtensions
{
    public static bool IsZero(this LinExpression? expression) => expression is null;

    public static bool IsConstant(this LinExpression? expression) => expression.IsZero() || expression!.Constant;
}
