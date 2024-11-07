using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.BrandAPI.Models.Dto;
using Services.BrandAPI.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ResponseBrandDto = Services.BrandAPI.Models.Dto.ResponseBrandDto;
using Services.BrandAPI.Models;

namespace Services.BrandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private ResponseBrandDto _response;
        private IMapper _mapper;

        public BrandController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseBrandDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseBrandDto> Get()

        {
            try
            {
                IEnumerable<Brand> brands = await _dbContext.Brands.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<BrandDto>>(brands);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseBrandDto> Get(int id)
        {
            try
            {
                Brand brand = await _dbContext.Brands.FirstAsync(u => u.Id == id);
                _response.Result = _mapper.Map<BrandDto>(brand);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseBrandDto> Post([FromBody] BrandDto brandDTO)
        {
            try
            {
                Brand brand = _mapper.Map<Brand>(brandDTO);
                await _dbContext.Brands.AddAsync(brand);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<BrandDto>(brand);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseBrandDto> Put([FromBody] BrandDto brandDTO)
        {
            try
            {

                Brand? brand = await _dbContext.Brands.FindAsync(brandDTO.Id);

                if (brand == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Brand not found.";
                    return _response;
                }
                _mapper.Map(brandDTO, brand);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<BrandDto>(brand);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task<ResponseBrandDto> Delete(int id)
        {
            try
            {
                Brand brand = _dbContext.Brands.First(u => u.Id == id);
                _dbContext.Brands.Remove(brand);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
