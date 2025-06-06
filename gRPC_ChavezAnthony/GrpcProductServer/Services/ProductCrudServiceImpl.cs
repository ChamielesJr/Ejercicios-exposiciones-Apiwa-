using Grpc.Core;
using GrpcProductServer.Data;

namespace GrpcProductServer.Services
{
    public class ProductCrudServiceImpl : ProductCrudService.ProductCrudServiceBase
    {
        private readonly ProductRepository _repository;

        public ProductCrudServiceImpl(ProductRepository repository)
        {
            _repository = repository;
        }

        public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            if (request?.Product == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "El producto es obligatorio."));
            }

            request.Product.Id = 0;
            var product = await _repository.AddAsync(request.Product);

            return new CreateProductResponse { Product = product };
        }

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));

            return new GetProductResponse { Product = product };
        }

        public override async Task<GetAllProductsResponse> GetAllProducts(GetAllProductsRequest request, ServerCallContext context)
        {
            var products = await _repository.GetAllAsync();
            var response = new GetAllProductsResponse();
            response.Products.AddRange(products);
            return response;
        }

        public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            if (request?.Product == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "El producto es obligatorio."));
            }

            var product = await _repository.UpdateAsync(request.Product);
            if (product == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));

            return new UpdateProductResponse { Product = product };
        }

        public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            var success = await _repository.DeleteAsync(request.Id);
            return new DeleteProductResponse
            {
                Success = success,
                Message = success ? "Producto eliminado" : "Producto no encontrado"
            };
        }
    }
}
