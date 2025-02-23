using AutoMapper;
using LPMaster.Application.Dto.Create;
using LPMaster.Application.Dto.Read;
using LPMaster.Application.Dto.Update;
using LPMaster.Domain.Entities;

namespace LPMaster.Application.Dto;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
        CreateMap<LinModel, ModelReadDto>();
        CreateMap<ModelCreateDto, LinModel>();
        CreateMap<ModelUpdateDto, LinModel>();
    }
}
