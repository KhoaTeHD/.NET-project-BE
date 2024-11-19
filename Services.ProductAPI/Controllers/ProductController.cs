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

        [HttpPost]
        [Route("create-with-variation")]
        public async Task<ResponseProductDto> PostWithVariation([FromBody] ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
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

        [HttpPut]
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

        //// API 1: Lấy danh sách Product dựa vào Cat_Id
        //[HttpGet]
        //[Route("by-category")]
        //public async Task<ResponseProductDto> GetByCategory(int? catId)
        //{
        //    try
        //    {
        //        IEnumerable<Product> products;
        //        if (catId.HasValue)
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .Where(p => p.Cat_Id == catId.Value)
        //                .ToListAsync();
        //        }
        //        else
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .ToListAsync();
        //        }
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 2: Lấy danh sách Product có ProductVariation với Price nằm trong khoảng
        //[HttpGet]
        //[Route("by-price-range")]
        //public async Task<ResponseProductDto> GetByPriceRange(decimal minPrice, decimal maxPrice)
        //{
        //    try
        //    {
        //        var products = await _dbContext.Products
        //            .Include(p => p.ProductVariations)
        //            .Where(p => p.ProductVariations.Any(pv => pv.Price >= minPrice && pv.Price <= maxPrice))
        //            .ToListAsync();
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 3: Lấy danh sách Product có ProductVariation với Col_Id nằm trong mảng
        //[HttpGet]
        //[Route("by-color-ids")]
        //public async Task<ResponseProductDto> GetByColorIds([FromQuery] int[]? colorIds)
        //{
        //    try
        //    {
        //        IEnumerable<Product> products;
        //        if (colorIds != null && colorIds.Length > 0)
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .Where(p => p.ProductVariations.Any(pv => colorIds.Contains(pv.Col_Id)))
        //                .ToListAsync();
        //        }
        //        else
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .ToListAsync();
        //        }
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 4: Lấy danh sách Product có Bra_Id nằm trong mảng
        //[HttpGet]
        //[Route("by-brand-ids")]
        //public async Task<ResponseProductDto> GetByBrandIds([FromQuery] int[]? brandIds)
        //{
        //    try
        //    {
        //        IEnumerable<Product> products;
        //        if (brandIds != null && brandIds.Length > 0)
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .Where(p => brandIds.Contains(p.Bra_Id))
        //                .ToListAsync();
        //        }
        //        else
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .ToListAsync();
        //        }
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 5: Lấy danh sách Product có ProductVariation với Siz_Id nằm trong mảng
        //[HttpGet]
        //[Route("by-size-ids")]
        //public async Task<ResponseProductDto> GetBySizeIds([FromQuery] int[]? sizeIds)
        //{
        //    try
        //    {
        //        IEnumerable<Product> products;
        //        if (sizeIds != null && sizeIds.Length > 0)
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .Where(p => p.ProductVariations.Any(pv => sizeIds.Contains(pv.Siz_Id)))
        //                .ToListAsync();
        //        }
        //        else
        //        {
        //            products = await _dbContext.Products
        //                .Include(p => p.ProductVariations)
        //                .ToListAsync();
        //        }
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 6: Lấy danh sách Product với ProductVariation có Price nhỏ nhất
        //[HttpGet]
        //[Route("unique-product-variations")]
        //public async Task<ResponseProductDto> GetUniqueProductVariations()
        //{
        //    try
        //    {
        //        var productVariations = await _dbContext.ProductVariations
        //            .GroupBy(pv => pv.Pro_Id)
        //            .Select(g => g.OrderBy(pv => pv.Price).First())
        //            .ToListAsync();

        //        var products = productVariations.Select(pv => pv.Product).Distinct().ToList();
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 7: Lấy danh sách Product sắp xếp theo giá trị tăng dần của Price
        //[HttpGet]
        //[Route("unique-product-variations-sorted-asc")]
        //public async Task<ResponseProductDto> GetUniqueProductVariationsSortedAsc()
        //{
        //    try
        //    {
        //        var productVariations = await _dbContext.ProductVariations
        //            .GroupBy(pv => pv.Pro_Id)
        //            .Select(g => g.OrderBy(pv => pv.Price).First())
        //            .OrderBy(pv => pv.Price)
        //            .ToListAsync();

        //        var products = productVariations.Select(pv => pv.Product).Distinct().ToList();
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 8: Lấy danh sách Product sắp xếp theo giá trị giảm dần của Price
        //[HttpGet]
        //[Route("unique-product-variations-sorted-desc")]
        //public async Task<ResponseProductDto> GetUniqueProductVariationsSortedDesc()
        //{
        //    try
        //    {
        //        var productVariations = await _dbContext.ProductVariations
        //            .GroupBy(pv => pv.Pro_Id)
        //            .Select(g => g.OrderBy(pv => pv.Price).First())
        //            .OrderByDescending(pv => pv.Price)
        //            .ToListAsync();

        //        var products = productVariations.Select(pv => pv.Product).Distinct().ToList();
        //        _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        //// API 9: Lấy Product chứa ProductVariation mới nhất
        //[HttpGet]
        //[Route("latest-product-variation")]
        //public async Task<ResponseProductDto> GetLatestProductVariation()
        //{
        //    try
        //    {
        //        var latestProductVariation = await _dbContext.ProductVariations
        //            .OrderByDescending(pv => pv.Id)
        //            .FirstOrDefaultAsync();

        //        if (latestProductVariation == null)
        //        {
        //            _response.IsSuccess = false;
        //            _response.Message = "No ProductVariation found.";
        //            return _response;
        //        }

        //        var product = latestProductVariation.Product;
        //        _response.Result = _mapper.Map<ProductDto>(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}
    }
}
