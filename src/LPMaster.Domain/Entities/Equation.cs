﻿namespace LPMaster.Domain.Entities;

public class Equation
{
    public int Id { get; init; }

    public int LeftExpressionId { get; init; }

    public required Expression LeftExpression { get; init; }

    public int RightExpressionId { get; init; }

    public required Expression RightExpression { get; init; }

    public int Relation { get; init; }

    public int ModelId { get; init; }

    public required Model Model { get; init; }
}
