using System;
namespace LPMaster.Domain.Entities.Base;

public interface IHasId<TId>
{
    public TId Id { get; init; }
}
