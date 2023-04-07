using Dapr.Client;
using Grpc.Net.Client;
using Shopping.Api.Order.MemberApplications.Orders;
using Shopping.Framework.Web;
using Shopping.Framework.Web.Product.Grpc.Proto;

namespace Shopping.Api.Order.Grpc.Services
{
    public interface IProductService
    {
        Task<ProductListInQueryResponse> GetProductListHttp(ProductListInQuery query);
        Task<ProductListInQueryResponse> GetProductListGrpc(ProductListInQuery query);
    }
    public class ProductService: IProductService
    {
        private readonly DaprClient _daprClient;
        public ProductService(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        public async Task<ProductListInQueryResponse> GetProductListHttp(ProductListInQuery query)
        {
            var productList = await _daprClient.InvokeMethodAsync<ProductListInQuery, ProductListInQueryResponse>(HttpMethod.Post
                , ApiServiceName.ProductServiceName
                , ProductApiServiceInPath.ProductList
                , query);

            return productList;
        }
        public async Task<ProductListInQueryResponse> GetProductListGrpc(ProductListInQuery query)
        {
            var request = new ProductListInRequestDto()
            {
                StoreId = query.StoreId,
            };
            foreach (var item in query.ProductIds)
            {
                request.ProductIds.Add(item);
            }

            var resp = await _daprClient.InvokeMethodGrpcAsync<ProductListInRequestDto, ProductListInQueryResponseDto>(
                ApiServiceName.ProductServiceName
                , "GetProductListData"
                , request);

            ProductListInQueryResponse productList =new ProductListInQueryResponse();
            productList.Products = new List<ProductListInQueryItemResponse>();
            foreach (var item in resp.Products)
            {
                productList.Products.Add(new ProductListInQueryItemResponse()
                {
                    Code = item.Code,
                    Id = item.Id,
                    ImageUrl = item.ImageUrl,
                    Name = item.Name,
                    Price = (decimal)item.Price,
                    Status = item.Status,
                    StoreId = item.StoreId,
                    TenantId = item.TenantId,
                    StoreName = item.StoreName,
                    ProductModels = item.ProductModels.Select(x => new StoreProductModelDto()
                    {
                        Id = x.Id,
                        Number = x.Number,
                        ProductId = x.ProductId,
                        Value = x.Value,
                        Price = (decimal)x.Price
                    }).ToList()
                });
            }
            return productList;
        }
    }
}
