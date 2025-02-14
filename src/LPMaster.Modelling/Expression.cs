namespace LPMaster.Modelling;

class Expression
{
    public int Id { get; init; }

    public required IEnumerable<Multi> Multis { get; init; }
}
