using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.NationAPI.Data;
using Services.NationAPI.Models;
using Services.NationAPI.Models.Dto;

namespace Services.NationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private Models.Dto.ResponseNationDto _response;
        private IMapper _mapper;

        public NationController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseNationDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseNationDto> Get()

        {
            try
            {
                IEnumerable<Nation> nations = await _dbContext.Nations.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<NationDto>>(nations);
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
        public async Task<ResponseNationDto> Get(int id)
        {
            try
            {
                Nation nation = await _dbContext.Nations.FirstAsync(u => u.Id == id);
                _response.Result = _mapper.Map<NationDto>(nation);
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
        public async Task<ResponseNationDto> Post([FromBody] NationDto brandDTO)
        {
            try
            {
                Nation nation = _mapper.Map<Nation>(brandDTO);
                await _dbContext.Nations.AddAsync(nation);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<NationDto>(nation);
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
        public async Task<ResponseNationDto> Put([FromBody] NationDto nationDTO)
        {
            try
            {

                Nation? nation = await _dbContext.Nations.FindAsync(nationDTO.Id);

                if (nation == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Nation not found.";
                    return _response;
                }
                _mapper.Map(nationDTO, nation);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<NationDto>(nation);
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
        public async Task<ResponseNationDto> Delete(int id)
        {
            try
            {
                Nation nation = _dbContext.Nations.First(u => u.Id == id);
                _dbContext.Nations.Remove(nation);
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
