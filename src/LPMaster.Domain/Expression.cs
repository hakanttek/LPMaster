namespace LPMaster.Domain;

public class Expression
{
    public int Id { get; init; }

    public required IEnumerable<Multi> Multis { get; init; }
}
