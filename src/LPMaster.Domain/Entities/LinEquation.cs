﻿using LPMaster.Domain.Entities.Base;
using LPMaster.Domain.Enums;
using LPMaster.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class LinEquation : IDescribable, IVerifiable
{
    public int? LeftExpressionId { get; init; }

    [ForeignKey(nameof(LeftExpressionId))]
    public LinExpression? LeftExpression { get; init; }

    public int? RightExpressionId { get; init; }

    [ForeignKey(nameof(RightExpressionId))]
    public LinExpression? RightExpression { get; init; }

    public required Relation Relation { get; init; }

    public required int ModelId { get; init; }

    [ForeignKey(nameof(ModelId))]
    public required LinModel Model { get; init; }

    public string? Description { get; init; }

    public bool Verified
    {
        get
        {
            try
            {
                return
                    (Model.Id == ModelId) &&
                    (LeftExpression?.ModelId is null || LeftExpression.ModelId == ModelId) &&
                    (RightExpression?.ModelId is null || RightExpression.ModelId == ModelId) &&
                    !(LeftExpression.IsConstant() && RightExpression.IsConstant());
            }
            catch (ModelOverlapException)
            {
                return false;
            }
        }
    }
}
