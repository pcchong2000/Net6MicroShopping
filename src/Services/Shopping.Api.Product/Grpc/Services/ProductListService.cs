using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Shopping.Api.Product.Applications.Queries;
using Shopping.Framework.Web.Product.Grpc.Proto;

namespace Shopping.Api.Product.Grpc.Services
{
    /// <summary>
    /// 注意：不需要定义service.rpc接口，因为Dapr中的GRPC固定调用接口/dapr.proto.runtime.v1.AppCallback/OnInvoke。
    /// </summary>
    public class ProductListService: AppCallback.AppCallbackBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductListService> _logger;

        public ProductListService(IMediator mediator, ILogger<ProductListService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();
            switch (request.Method)
            {
                case "GetProductListData":
                    var dto = request.Data.Unpack<ProductListInRequestDto>();
                    ProductListInQueryResponseDto resp = new ProductListInQueryResponseDto();
                    ProductListInQuery query = new ProductListInQuery(dto.ProductIds.ToList())
                    {
                        StoreId = dto.StoreId,
                    };
                    var result = await _mediator.Send(query);
                    foreach (var item in result.Products)
                    {
                        var dtoItem = new ProductListInQueryItemResponseDto()
                        {
                            Code = item.Code,
                            Id = item.Id,
                            ImageUrl = item.ImageUrl,
                            Name = item.Name,
                            Price = (double)item.Price,
                            Status = (int)item.Status,
                            StoreName = item.StoreName,
                            TenantId = item.TenantId,
                        };
                        foreach (var item11 in item.ProductModels)
                        {
                            dtoItem.ProductModels.Add(new ProductListInProductModelResponseDto()
                            {
                                Id = item11.Id,
                                Number = item11.Number,
                                Price = (double)item11.Price,
                                ProductId = item11.ProductId,
                                Value = item11.Value,

                            });
                        }
                        resp.Products.Add(dtoItem);
                    }
                    response.Data = Any.Pack(resp);
                    break;
            }
            return response;
        }
    }
}
