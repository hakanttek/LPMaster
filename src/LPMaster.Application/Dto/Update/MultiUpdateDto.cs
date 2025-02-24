﻿using LPMaster.Domain.Entities;
using System;
namespace LPMaster.Application.Dto.Update;

public record MultiUpdateDto
{
    public required double Coef { get; set; }

    public DVarUpdateDto? DVar { get; init; }
}
