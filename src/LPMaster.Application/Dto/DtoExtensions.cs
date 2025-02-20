using LPMaster.Application.Dto.Create;
using LPMaster.Domain.Entities;

namespace LPMaster.Application.Dto;

public class DtoExtensions
{
    #region ModelCreateDto
    public static TModel SubjectTo<TModel>(TModel model, params EquationCreateDto[] equations) where TModel : ModelCreateDto
    {
        model._constraints.AddRange(equations);
        return model;
    }

    public static TModel Named<TModel>(TModel model, string name) where TModel : ModelCreateDto
    {
        model.Name = name;
        return model;
    }

    public static TModel Defined<TModel>(TModel model, string definition) where TModel : ModelCreateDto
    {
        model.Description = definition;
        return model;
    }
    #endregion
}
