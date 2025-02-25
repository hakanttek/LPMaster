using AutoMapper;
using LPMaster.Application.Common.Models.Read;
using LPMaster.Application.Common.Models.Update;
using LPMaster.Application.Models.Commands;
using LPMaster.Domain.Entities;

namespace LPMaster.Application.Common.Models;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<LinModel, ModelReadDto>();
        CreateMap<CreateModelCommand, LinModel>();
        CreateMap<ModelUpdateDto, LinModel>();
    }
}
