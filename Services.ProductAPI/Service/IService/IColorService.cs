﻿using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI.Service.IService
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDto>> GetColors();
    }
}
