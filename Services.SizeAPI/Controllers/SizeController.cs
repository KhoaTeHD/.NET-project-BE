using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.SizeAPI.Data;
using Services.SizeAPI.Models;
using Services.SizeAPI.Models.Dto;

namespace Services.SizeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private Models.Dto.ResponseSizeDto _response;
        private IMapper _mapper;

        public SizeController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseSizeDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseSizeDto> Get()

        {
            try
            {
                IEnumerable<Size> sizes = await _dbContext.Sizes.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<SizeDto>>(sizes);
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
        public async Task<ResponseSizeDto> Get(int id)
        {
            try
            {
                Size size = await _dbContext.Sizes.FirstAsync(u => u.Id == id);
                _response.Result = _mapper.Map<SizeDto>(size);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseSizeDto> Post([FromBody] SizeDto sizeDTO)
        {
            try
            {
                Size size = _mapper.Map<Size>(sizeDTO);
                await _dbContext.Sizes.AddAsync(size);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<SizeDto>(size);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseSizeDto> Put([FromBody] SizeDto sizeDTO)
        {
            try
            {

                Size? size = await _dbContext.Sizes.FindAsync(sizeDTO.Id);

                if (size == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Brand not found.";
                    return _response;
                }
                _mapper.Map(sizeDTO, size);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<SizeDto>(size);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task<ResponseSizeDto> Delete(int id)
        {
            try
            {
                Size size = _dbContext.Sizes.First(u => u.Id == id);
                _dbContext.Sizes.Remove(size);
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
