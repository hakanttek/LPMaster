using LPMaster.Domain.Entities.Base;
using LPMaster.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class Equation : IUnique<int>, IDescribable, IVerifiable
{
    public int Id { get; init; }

    public int LeftExpressionId { get; init; }

    [ForeignKey(nameof(LeftExpressionId))]
    public required Expression LeftExpression { get; init; }

    public int RightExpressionId { get; init; }

    [ForeignKey(nameof(RightExpressionId))]
    public required Expression RightExpression { get; init; }

    public int Relation { get; init; }

    public int ModelId { get; init; }

    [ForeignKey(nameof(ModelId))]
    public required Model Model { get; init; }

    public string? Description { get; init; }

    public bool Verified
    {
        get
        {
            try
            {
                return
                    (Model.Id == ModelId) &&
                    (LeftExpression.ModelId is null || LeftExpression.ModelId == ModelId) &&
                    (RightExpression.ModelId is null || RightExpression.ModelId == ModelId) &&
                    (LeftExpression.ModelId is not null || RightExpression.ModelId is not null);
            }
            catch (ModelOverlapException)
            {
                return false;
            }
        }
    }
}
