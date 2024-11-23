using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.ColorAPI.Data;
using Services.ColorAPI.Models;
using Services.ColorAPI.Models.Dto;

namespace Services.ColorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseColorDto _response;
        private IMapper _mapper;

        public ColorController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseColorDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseColorDto> Get()

        {
            try
            {
                IEnumerable<Color> brands = await _dbContext.Colors.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ColorDto>>(brands);
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
        public async Task<ResponseColorDto> Get(int id)
        {
            try
            {
                Color color = await _dbContext.Colors.FirstAsync(u => u.Id == id);
                _response.Result = _mapper.Map<ColorDto>(color);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseColorDto> Post([FromBody] ColorDto colorDTO)
        {
            try
            {
                Color color = _mapper.Map<Color>(colorDTO);
                await _dbContext.Colors.AddAsync(color);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<ColorDto>(color);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseColorDto> Put([FromBody] ColorDto colorDTO)
        {
            try
            {

                Color? color = await _dbContext.Colors.FindAsync(colorDTO.Id);

                if (color == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Color not found.";
                    return _response;
                }
                _mapper.Map(colorDTO, color);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<ColorDto>(color);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseColorDto> Delete(int id)
        {
            try
            {
                Color color = _dbContext.Colors.First(u => u.Id == id);
                _dbContext.Colors.Remove(color);
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
