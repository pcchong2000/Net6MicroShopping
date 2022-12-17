using Dapr.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Order.Data;
using Shopping.Api.Order.Grpc.Services;
using Shopping.Framework.Web;

namespace Shopping.Api.Order.MemberApplications.Orders
{
    public class OrderCreateCommand : IRequest<OrderCreateCommandResponse>
    {
        public OrderCreateCommand(List<OrderCreateItemDto> orderItems)
        {
            OrderItems = orderItems;
        }
        public List<OrderCreateItemDto> OrderItems { get; set; }
        public string? MemberAddressId { get; set; }
        public string StoreId { get; set; }
        public string StoreName { get; set; }
    }
    public class OrderCreateItemDto
    {
        public OrderCreateItemDto(string productId, string productModelId, int number)
        {
            ProductId = productId;
            ProductModelId = productModelId;
            Number = number;
        }
        public string ProductId { get; set; }
        public string ProductModelId { get; set; }
        public int Number { get; set; }
    }
    public class OrderCreateCommandResponse
    {
        public string? OrderNo { get; set; }
    }
    public class JianKuCunDto
    {
        public List<JianKuCunItemDto> ProductModels { get; set; }
    }
    public class JianKuCunItemDto
    {
        public string ProductId { get; set; }
        public string ProductModelId { get; set; }
        public int Number { get; set; }
    }

    public class ProductListInQuery
    {
        public ProductListInQuery(string storeId, List<string> productIds)
        {
            StoreId = storeId;
            ProductIds = productIds;
        }
        public List<string> ProductIds { get; set; }
        public string StoreId { get; set; }
    }
    public class ProductListInQueryResponse
    {
        public int Status { get; set; }

        public List<ProductListInQueryItemResponse> Products { get; set; }
    }
    public class ProductListInQueryItemResponse
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }

        public List<StoreProductModelDto> ProductModels { get; set; }
    }
    public class StoreProductModelDto
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? Value { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
    }
    public class StoreResponse
    {
        public string? Id { get; set; }
        public string? TenantId { get; set; }
        public string? StoreCode { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Status { get; set; }
    }
    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, OrderCreateCommandResponse>
    {
        private ICurrentUserService _currentUser;
        private readonly OrderDbContext _context;
        private readonly DaprClient _daprClient;
        private readonly IProductService _productService;
        public OrderCreateCommandHandler(ICurrentUserService currentUser, OrderDbContext context
            , DaprClient daprClient
            , IProductService productService)
        {
            _currentUser = currentUser;
            _context = context;
            _productService = productService;
            _daprClient = daprClient;
        }

        public async Task<OrderCreateCommandResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            OrderCreateCommandResponse rensp = new OrderCreateCommandResponse();

            var store = await _daprClient.InvokeMethodAsync<StoreResponse>(HttpMethod.Get
                , ApiServiceName.TenantServiceName
                , TenantApiServiceInPath.StoreDetail + request.StoreId
                );
            if (store == null)
            {
                throw new ApiBaseException("门店不存在");
            }

            var productQuery = new ProductListInQuery(request.StoreId, request.OrderItems.Select(a => a.ProductId).ToList());

            var productList = await _productService.GetProductListGrpc(productQuery);

            var order = new Models.Order(request.StoreId, request.StoreName, _currentUser.Id, _currentUser.Name, store.TenantId!);
            var jianKucunList = new List<JianKuCunItemDto>();

            foreach (var item in request.OrderItems)
            {
                var product = productList.Products.FirstOrDefault(a => a.Id == item.ProductId);
                if (product != null)
                {
                    var productModel = product.ProductModels.Where(a => a.Id == item.ProductModelId).FirstOrDefault();
                    if (productModel != null)
                    {
                        order.AddItem(product.Id, product.Name, product.ImageUrl, productModel.Id!, productModel.Value, product.Price, item.Number);
                        jianKucunList.Add(new JianKuCunItemDto()
                        {
                            ProductModelId = productModel.Id!,
                            Number = item.Number,
                            ProductId = item.ProductId,
                        });
                    }
                    else
                    {
                        throw new ApiBaseException("此型号不存在");
                    }
                }
                else
                {
                    throw new ApiBaseException("商品不存在");
                }
            }
            order.OrderAmount = order.OrderItems.Sum(a => a.Amount);

            if (request.MemberAddressId != null)
            {
                var address = await _context.MemberAddress
                .Where(a => a.Id == request.MemberAddressId && a.MemberId == _currentUser.Id)
                .FirstOrDefaultAsync();
                order.AddOredrAddress(address!);
                await _context.OrderAddress.AddAsync(order.OrderAddress);
            }

            await _context.Order.AddAsync(order);
            await _context.OrderItem.AddRangeAsync(order.OrderItems);
            await _context.SaveChangesAsync();

            await _daprClient.SaveStateAsync("statestore", "order", order);

            await _daprClient.PublishEventAsync("pubsub", "newOrder", new JianKuCunDto { ProductModels = jianKucunList });

            rensp.OrderNo = order.OrderNo;
            return rensp;
        }
    }
}
