using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services.MessageServices
{
    public interface IMessageService
    {
        Task<List<MessageListItemResponseModel>> GetMessageListAsync(MessageListRequestModel request);
        Task<MessageListItemResponseModel> GetMessageAsync(string id);
        Task<MessageListItemResponseModel> GetMessageNextAsync(MessageNextRequestModel request);
    }
}
