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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private Models.Dto.ResponseProductDto _response;
        private IMapper _mapper;
        private IBrandService _brandService;
        private ICategoryService _categoryService;
        private INationService _nationService;
        private ISupplierService _supplierService;
        private IColorService _colorService;
        private ISizeService _sizeService;

        public ProductController(AppDbContext dbContext, IMapper mapper, IBrandService brandService, ICategoryService categoryService, INationService nationService, ISupplierService supplierService, IColorService colorService, ISizeService sizeService)
        {
            _dbContext = dbContext;
            _response = new ResponseProductDto();
            _mapper = mapper;
            _brandService = brandService;
            _categoryService = categoryService;
            _nationService = nationService;
            _supplierService = supplierService;
            _colorService = colorService;
            _sizeService = sizeService;
        }

        //[HttpGet]
        //public async Task<ResponseProductDto> Get()
        //{
        //    try
        //    {
        //        IEnumerable<Product> products = await _dbContext.Products.Include(gr => gr.ProductVariations).ToListAsync();
        //        IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        //        IEnumerable<BrandDto> brandDtos = await _brandService.GetBrands();
        //        if (brandDtos == null || !brandDtos.Any())
        //        {
        //            throw new Exception("No brands found.");
        //        }

        //        foreach (var productDto in productDtos)
        //        {
        //            productDto.Brand = brandDtos.FirstOrDefault(u => u.Id == productDto.Bra_Id);
        //            if (productDto.Brand == null)
        //            {
        //                throw new Exception($"Brand not found for Product {productDto.Id} with Bra_Id {productDto.Bra_Id}.");
        //            }
        //        }

        //        _response.Result = productDtos;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        [HttpGet]
        public async Task<ResponseProductDto> Get()
        {
            try
            {
                IEnumerable<Product> products = await _dbContext.Products.Include(gr => gr.ProductVariations).ToListAsync();
                IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                IEnumerable<BrandDto> brandDtos = await _brandService.GetBrands();
                IEnumerable<CategoryDto> categoryDtos = await _categoryService.GetCategorys();
                IEnumerable<NationDto> nationDtos = await _nationService.GetNations();
                IEnumerable<SupplierDto> supplierDtos = await _supplierService.GetSuppliers();
                IEnumerable<ColorDto> colorDtos = await _colorService.GetColors();
                IEnumerable<SizeDto> sizeDtos = await _sizeService.GetSizes();

                if (brandDtos == null || !brandDtos.Any())
                {
                    throw new Exception("No brands found.");
                }
                if (categoryDtos == null || !categoryDtos.Any())
                {
                    throw new Exception("No categories found.");
                }
                if (nationDtos == null || !nationDtos.Any())
                {
                    throw new Exception("No nations found.");
                }
                //if (supplierDtos == null || !supplierDtos.Any())
                //{
                //    throw new Exception("No suppliers found.");
                //}
                if (colorDtos == null || !colorDtos.Any())
                {
                    throw new Exception("No color found.");
                }
                if (sizeDtos == null || !sizeDtos.Any())
                {
                    throw new Exception("No sizes found.");
                }

                foreach (var productDto in productDtos)
                {
                    productDto.Brand = brandDtos.FirstOrDefault(u => u.Id == productDto.Bra_Id);
                    

                    productDto.Category = categoryDtos.FirstOrDefault(u => u.Id == productDto.Cat_Id);
                    

                    productDto.Nation = nationDtos.FirstOrDefault(u => u.Id == productDto.Nat_Id);
                    

                    productDto.Supplier = supplierDtos.FirstOrDefault(u => u.Supplier_ID == productDto.Sup_Id);
                    

                    foreach (var productVariationDto in productDto.ProductVariations)
                    {
                        productVariationDto.Color = colorDtos.FirstOrDefault(u => u.Id == productVariationDto.Col_Id);
                        //if (productVariationDto.Color == null)
                        //{
                        //    throw new Exception($"Color not found for ProductVariation {productVariationDto.Id} with Col_Id {productVariationDto.Col_Id}.");
                        //}

                        productVariationDto.Size = sizeDtos.FirstOrDefault(u => u.Id == productVariationDto.Siz_Id);
                        //if (productVariationDto.Size == null)
                        //{
                        //    throw new Exception($"Size not found for ProductVariation {productVariationDto.Id} with Siz_Id {productVariationDto.Siz_Id}.");
                        //}
                    }
                }

                _response.Result = productDtos;
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
                productDto.ProductVariations = updateProductDto.ProductVariations;
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

        [HttpGet]
        [Route("products-by-category/{catId:int}")]
        public async Task<ResponseProductDto> GetByCategory(int catId)
        {
            try
            {
                IEnumerable<Product> products = await _dbContext.Products
                    .Include(gr => gr.ProductVariations)
                    .Where(p => p.Cat_Id == catId)
                    .ToListAsync();

                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
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
