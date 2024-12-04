using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.Data;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Service;
using Services.ProductAPI.Service.IService;

namespace Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private Models.Dto.ResponseProductVariationDto _response;
        private IMapper _mapper;
        private IColorService _colorService;
        private ISizeService _sizeService;

        public ProductVariationController(AppDbContext dbContext, IMapper mapper, ISizeService sizeService, IColorService colorService)
        {
            _dbContext = dbContext;
            _response = new ResponseProductVariationDto();
            _mapper = mapper;
            _sizeService = sizeService;
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<ResponseProductVariationDto> Get()

        {
            try
            {
                IEnumerable<ProductVariation> productVariations = (IEnumerable<ProductVariation>) await _dbContext.ProductVariations
                    .Include(pv => pv.Product)
                    //    .ThenInclude(p => p.Brand)
                    //.Include(pv => pv.Product.Category)
                    //.Include(pv => pv.Product.Nation)
                    //.Include(pv => pv.Product.Supplier)
                    .ToListAsync();

                IEnumerable <ProductVariationDto> productVariationDtos = _mapper.Map<IEnumerable<ProductVariationDto>>(productVariations);

                IEnumerable<ColorDto> colorDtos = await _colorService.GetColors();
                IEnumerable<SizeDto> sizeDtos = await _sizeService.GetSizes();

                if (colorDtos == null || !colorDtos.Any())
                {
                    throw new Exception("No color found.");
                }
                if (sizeDtos == null || !sizeDtos.Any())
                {
                    throw new Exception("No sizes found.");
                }

                foreach (var productVariationDto in productVariationDtos)
                {
                    productVariationDto.Color = colorDtos.FirstOrDefault(u => u.Id == productVariationDto.Col_Id);
                    if (productVariationDto.Color == null)
                    {
                        throw new Exception($"Color not found for ProductVariation {productVariationDto.Id} with Col_Id {productVariationDto.Col_Id}.");
                    }

                    productVariationDto.Size = sizeDtos.FirstOrDefault(u => u.Id == productVariationDto.Siz_Id);
                    if (productVariationDto.Size == null)
                    {
                        throw new Exception($"Size not found for ProductVariation {productVariationDto.Id} with Siz_Id {productVariationDto.Siz_Id}.");
                    }
                }
                _response.Result = productVariationDtos;
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
                ProductVariation productVariation = await _dbContext.ProductVariations.Include(p => p.Product).FirstAsync(u => u.Id == id);
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseProductVariationDto> Put([FromBody] ProductVariationDto productVariationDTO)
        {
            try
            {

                ProductVariation? productVariation = await _dbContext.ProductVariations.FindAsync(productVariationDTO.Id);

                if (productVariation == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Product Variation not found.";
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
        [Authorize(Roles = "ADMIN")]
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
