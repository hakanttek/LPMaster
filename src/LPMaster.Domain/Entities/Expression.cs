using LPMaster.Domain.Entities.Base;
using LPMaster.Domain.Exceptions;

namespace LPMaster.Domain.Entities;

public class Expression : IHasId<int>, IDescribable, IVerifiable
{
    public int Id { get; init; }

    public IEnumerable<Multi> Multis { get; init; } = Enumerable.Empty<Multi>();

    public string? Description { get; init; }

    public Model? Model
    {
        get
        {
            try
            {
                return Multis
                    .Where(multi => multi.ModelId is not null)
                    .Select(multi => multi.Model)
                    .Distinct()
                    .SingleOrDefault();
            }
            catch (InvalidOperationException ex)
            {
                throw new ModelOverlapException("The multis of the expression either have no model or have different models.", ex);
            }
        }
    }

    public int? ModelId => Model?.Id;

    public bool Verified => Multis
                    .Where(multi => multi.ModelId is not null)
                    .Select(multi => multi.Model)
                    .Distinct()
                    .Count() == 1;    
}
