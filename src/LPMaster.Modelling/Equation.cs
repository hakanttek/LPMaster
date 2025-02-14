namespace LPMaster.Modelling;

class Equation
{
    public int Id { get; init; }

    public int LeftExpressionId { get; init; }

    public required Expression LeftExpression { get; init; }

    public int RightExpressionId { get; init; }

    public required Expression RightExpression { get; init; }

    public int Relation { get; init; }
}
