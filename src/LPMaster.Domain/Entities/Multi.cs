using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class Multi : IVerifiable
{
    public double Coef { get; init; }
    
    public int? ColIndex { get; init; }

    public DVar? DVar { get; init; }

    public bool IsConstant => DVar is null;

    public int? ModelId => DVar?.ModelId;

    public Model? Model => DVar?.Model;

    public bool Verified => ColIndex == DVar?.ColIndex;
}
