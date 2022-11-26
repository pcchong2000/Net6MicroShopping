using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Product.Data;
using Shopping.Framework.DomainBase.Base;
using Shopping.Framework.Web;

namespace Shopping.Api.Product.Applications.Queries
{
    public class StoreProductCategoryTenantPageQuery : IRequest<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>>
    {
        public string StoreId { get; set; }
    }
    public class StoreProductCategoryTenantPageQueryItemResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Sort { get; set; }

    }
    public class StoreProductCategoryTenantPageQueryHandler : IRequestHandler<StoreProductCategoryTenantPageQuery, ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>>
    {
        private readonly ProductDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public StoreProductCategoryTenantPageQueryHandler(ProductDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>> Handle(StoreProductCategoryTenantPageQuery request, CancellationToken cancellationToken)
        {
            ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse> resp = new ResponsePageBase<StoreProductCategoryTenantPageQueryItemResponse>();

            var list = await _context.StoreProductCategory.Where(a => a.TenantId == _currentUser.TenantId && a.StoreId == request.StoreId)
                .Select(a => new StoreProductCategoryTenantPageQueryItemResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Sort = a.Sort,

                }).ToListAsync();
            resp.List = list;

            return resp;
        }
    }
}
