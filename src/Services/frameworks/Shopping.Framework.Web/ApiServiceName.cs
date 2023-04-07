using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Web
{
    public static class ApiServiceName
    {
        public static string ProductServiceName = "productapi";
        public static string MemberServiceName = "memberapi";
        public static string TenantServiceName = "tenantapi";
    }
    public static class ProductApiServiceInPath
    {
        public static string ProductList = "Product/list-in";
    }
    public static class TenantApiServiceInPath
    {
        public static string StoreDetail = "store/detail-in/";
    }
}
