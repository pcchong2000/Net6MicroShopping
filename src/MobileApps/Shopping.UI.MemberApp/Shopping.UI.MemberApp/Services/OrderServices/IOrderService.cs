using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.OrderServices
{
    public interface IOrderService
    {
        Task<List<BlogListItemResponseModel>> GetBlogListAsync(BlogListRequestModel request);
        Task<BlogListItemResponseModel> GetBlogAsync(string id);
    }
}
