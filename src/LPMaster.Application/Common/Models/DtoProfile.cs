using AutoMapper;
using LPMaster.Application.Common.Models.Read;
using LPMaster.Application.Common.Models.Update;
using LPMaster.Application.Models.Commands;
using LPMaster.Domain.Entities;

namespace LPMaster.Application.Common.Models;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
        CreateMap<LinModel, ModelReadDto>();
        CreateMap<CreateModelCommand, LinModel>();
        CreateMap<ModelUpdateDto, LinModel>();
    }
}
