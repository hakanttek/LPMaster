namespace LPMaster.Domain.Entities;

public static class EntityExtensions
{
    #region LinExpression
    public static bool IsZero(this LinExpression? expression) => expression is null;

    public static bool IsConstant(this LinExpression? expression) => expression.IsZero() || expression!.Constant;
    #endregion

    public static bool IsVerified(this LinExpression? expression) => expression?.Verified ?? false;

    public static bool IsVerified(this IEnumerable<LinEquation>? equations) => equations?.All(e => e.Verified) ?? false;
}
