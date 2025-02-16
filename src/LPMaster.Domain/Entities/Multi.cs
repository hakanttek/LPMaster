using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class Multi : IVerifiable
{
    public double Coef { get; init; }
    
    public int? DVarId { get; init; }

    [ForeignKey(nameof(DVarId))]
    public DVar? DVar { get; init; }

    public bool IsConstant => DVar is null;

    public int? ModelId => DVar?.ModelId;

    public Model? Model => DVar?.Model;

    public bool Verified => DVarId == DVar?.Id;
}
