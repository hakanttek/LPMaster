using System;
namespace LPMaster.Domain.Entities.Base;

public interface IUnique<TId>
{
    public TId Id { get; init; }
}
