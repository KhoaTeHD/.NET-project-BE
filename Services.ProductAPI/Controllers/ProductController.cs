using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
                IEnumerable<Product> products = await _dbContext.Products.Include(gr => gr.ProductVariations).ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
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
                Product? product = await _dbContext.Products
                    .Include(gr => gr.ProductVariations)
                    .FirstOrDefaultAsync(gr => gr.Id == id);

                if (product == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Product not found.";
                    return _response;
                }

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
        [Route("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseProductDto> Post([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                ProductDto productDTO = new ProductDto();
                productDTO.Cat_Id = createProductDto.Cat_Id;
                productDTO.Nat_Id = createProductDto.Nat_Id;
                productDTO.Bra_Id = createProductDto.Bra_Id;
                productDTO.Sup_Id = createProductDto.Sup_Id;
                productDTO.Name = createProductDto.Name;
                productDTO.Status = createProductDto.Status;
                productDTO.ProductVariations = null;

                Product product = _mapper.Map<Product>(productDTO);
                if (product.ProductVariations == null)
                {
                    product.ProductVariations = new List<ProductVariation>();
                }
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

        //[HttpPost]
        //[Route("create-with-variation")]
        //[Authorize(Roles = "ADMIN")]
        //public async Task<ResponseProductDto> PostWithVariation([FromBody] ProductDto productDto)
        //{
        //    try
        //    {
        //        Product product = _mapper.Map<Product>(productDto);
        //        if (product.ProductVariations == null)
        //        {
        //            product.ProductVariations = new List<ProductVariation>();
        //        }
        //        await _dbContext.Products.AddAsync(product);
        //        await _dbContext.SaveChangesAsync();
        //        _response.Result = _mapper.Map<ProductDto>(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseProductDto> Put([FromBody] UpdateProductDto updateProductDto)
        {
            try
            {
                Product? product = await _dbContext.Products.FindAsync(updateProductDto.Id);

                if (product == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Product not found.";
                    return _response;
                }
                ProductDto productDto = new ProductDto();
                productDto.Id = updateProductDto.Id;
                productDto.Cat_Id = updateProductDto.Cat_Id;
                productDto.Nat_Id = updateProductDto.Nat_Id;
                productDto.Bra_Id = updateProductDto.Bra_Id;
                productDto.Sup_Id = updateProductDto.Sup_Id;
                productDto.Name = updateProductDto.Name;
                productDto.Status = updateProductDto.Status;
                productDto.ProductVariations = product.ProductVariations;
                _mapper.Map(productDto, product);

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
        [Authorize(Roles = "ADMIN")]
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
