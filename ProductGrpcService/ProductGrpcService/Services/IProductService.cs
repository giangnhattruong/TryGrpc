using ProductGrpcService.Domain.Models;

namespace ProductGrpcService.Services;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> ListAsync();

    Task<ProductModel> CreateAsync(ProductModel productModel);
}