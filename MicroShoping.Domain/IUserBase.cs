using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroShoping.DomainBase
{
    public interface IUserBase : IEntityTenantBase
    {
        string UserName { get; set; }
        string Name { get; set; }
        string PasswordSecert { get; set; }
        string PasswordHash { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
    }
    public class UserBase : EntityTenantBase, IUserBase
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PasswordSecert { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
