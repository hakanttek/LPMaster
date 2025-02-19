using LPMaster.Application.Dto.Create;
using LPMaster.Application.Dto.Update;

namespace LPMaster.Application.Dto.Read;

public record ModelReadDto : ModelUpdateDto
{
    public int Id { get; init; }

    private readonly List<DVarCreateDto> _createdDvars = new();

    internal IEnumerable<DVarCreateDto> CreatedDvars => _createdDvars;
    
    private readonly Lock _dvarAddingLocker = new();

    internal void Add(DVarCreateDto dto)
    {
        lock (_dvarAddingLocker)
        {
            _createdDvars.Add(dto);
        }
    }
}
