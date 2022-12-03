using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.DomainBase.Base
{
    public interface IUserBase : IEntityBase
    {
        string UserName { get; set; }
        string? Name { get; set; }
        string? PasswordSecert { get; set; }
        string? PasswordHash { get; set; }
        string? PhoneNumber { get; set; }
        string? Email { get; set; }
    }
    public class UserBase : EntityBase, IUserBase
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? PasswordSecert { get; set; }
        [MaxLength(200)]
        public string? PasswordHash { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }
    }
}
