namespace LPMaster.Domain;

public class Model
{
    public int Id { get; init; }

    public int Object { get; init; }

    public required Expression ObjectiveFunction { get; init; }

    public required IEnumerable<Equation> Constraints { get; init; }
}
