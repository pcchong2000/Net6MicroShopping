using Dapr.Client;
using Shopping.Api.Order.Application.Members.Commands;
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
        //private readonly ProductListGrpc.ProductListGrpcClient _orderingGrpcClient;
        private readonly DaprClient _daprClient;
        public ProductService(DaprClient daprClient
            //,ProductListGrpc.ProductListGrpcClient orderingGrpcClient
            )
        {
            //_orderingGrpcClient = orderingGrpcClient;
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


            //var resp = await _orderingGrpcClient.GetProductListDataAsync(request);

            ProductListInQueryResponse productList=new ProductListInQueryResponse();

            return productList;
        }
    }
}
