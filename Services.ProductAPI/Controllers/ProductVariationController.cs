using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.Data;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.Dto;

namespace Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private Models.Dto.ResponseProductVariationDto _response;
        private IMapper _mapper;

        public ProductVariationController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseProductVariationDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseProductVariationDto> Get()

        {
            try
            {
                IEnumerable<ProductVariation> productVariations = await _dbContext.ProductVariations.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ProductVariationDto>>(productVariations);
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
        public async Task<ResponseProductVariationDto> Get(int id)
        {
            try
            {
                ProductVariation productVariation = await _dbContext.ProductVariations.FirstAsync(u => u.Id == id);
                _response.Result = _mapper.Map<ProductVariationDto>(productVariation);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseProductVariationDto> Post([FromBody] ProductVariationDto productVariationDTO)
        {
            try
            {
                ProductVariation productVariation = _mapper.Map<ProductVariation>(productVariationDTO);
                await _dbContext.ProductVariations.AddAsync(productVariation);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<ProductVariationDto>(productVariation);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseProductVariationDto> Put([FromBody] ProductVariationDto productVariationDTO)
        {
            try
            {

                ProductVariation? productVariation = await _dbContext.ProductVariations.FindAsync(productVariationDTO.Id);

                if (productVariation == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Brand not found.";
                    return _response;
                }
                _mapper.Map(productVariationDTO, productVariation);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<ProductVariationDto>(productVariation);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task<ResponseProductVariationDto> Delete(int id)
        {
            try
            {
                ProductVariation productVariation = _dbContext.ProductVariations.First(u => u.Id == id);
                _dbContext.ProductVariations.Remove(productVariation);
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
