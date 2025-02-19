using LPMaster.Application.Dto.Read;

namespace LPMaster.Application.Dto.Create;

public record DVarCreateDto()
{
    public int ModelId => Model.Id;

#pragma warning disable CS8618
    private readonly ModelReadDto _model;
#pragma warning restore CS8618

    public required ModelReadDto Model
    {
        get => _model;
        init
        {
            _model = value;
            _model.Add(this);
        }
    }

    public int ColIndex => _model.CreatedDvars.ToList().IndexOf(this);

    public string? Name { get; init; }

    public string? Description { get; init; }
}
