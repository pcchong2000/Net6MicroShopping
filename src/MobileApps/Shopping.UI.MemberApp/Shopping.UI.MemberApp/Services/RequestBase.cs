using Shopping.UI.MemberApp.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.UI.MemberApp.Services
{
    public class RequestBase
    {
        public string TenantId { get; set; } = Appsettings.TenantId;
    }
}
