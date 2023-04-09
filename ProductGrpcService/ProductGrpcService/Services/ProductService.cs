using System.Text.Json;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using ProductGrpcService.Database;
using Grpc.Core;
using ProductGrpcService.Domain.Models;

namespace ProductGrpcService.Services;

public class ProductService : Product.ProductBase, IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProductModel>> ListAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<ProductModel> CreateAsync(ProductModel productModel)
    {
        await _context.Products.AddAsync(productModel);
        await _context.SaveChangesAsync();
        return productModel;
    }

    public override async Task<ProductResponse> Create(ProductRequest request, ServerCallContext context)
    {
        var productModel = new ProductModel()
        {
            Name = request.Name,
            Price = request.Price
        };
        
        await _context.Products.AddAsync(productModel);
        await _context.SaveChangesAsync();
        
        return new ProductResponse()
        {
            Id = productModel.Id,
            Name = productModel.Name,
            Price = productModel.Price
        };
    }

    public override async Task<ListProducts> List(Empty request, ServerCallContext context)
    {
        var models = await _context.Products
            .AsNoTracking()
            .ToListAsync();
            
        var products = models.Select(p => new ProductResponse()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price
        }).ToList();

        var rs = new ListProducts();
        rs.Products.AddRange(products);
        
        return rs;
    }
}