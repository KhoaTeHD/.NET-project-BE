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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private Models.Dto.ResponseProductDto _response;
        private IMapper _mapper;

        public ProductController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseProductDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseProductDto> Get()

        {
            try
            {
                IEnumerable<Product> nations = await _dbContext.Products.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable< ProductDto>>(nations);
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
        public async Task<ResponseProductDto> Get(int id)
        {
            try
            {
                Product product = await _dbContext.Products.FirstAsync(u => u.Id == id);
                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseProductDto> Post([FromBody] ProductDto productDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDTO);
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseProductDto> Put([FromBody] ProductDto productDTO)
        {
            try
            {

                Product? product = await _dbContext.Products.FindAsync(productDTO.Id);

                if (product == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Brand not found.";
                    return _response;
                }
                _mapper.Map(productDTO, product);

                await _dbContext.SaveChangesAsync();

                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task<ResponseProductDto> Delete(int id)
        {
            try
            {
                Product product = _dbContext.Products.First(u => u.Id == id);
                _dbContext.Products.Remove(product);
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
