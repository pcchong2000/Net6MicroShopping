using System.ComponentModel.DataAnnotations;

namespace MicroShoping.DomainBase
{
    public interface IEntityIdBase
    {
        [MaxLength(36)]
        string Id { get; set; }
    }
    public interface IEntityBase : IEntityIdBase
    {
        DateTime CreatTime { get; set; }
        DateTime UpdateTime { get; set; }
        bool IsDeleted { get; set; }
    }
    public interface IEntityTenantBase : IEntityBase
    {
        [MaxLength(36)]
        string TenantCode { get; set; }
    }
    public class EntityBase : IEntityBase
    {
        /// <summary>
        /// ID,统一小写
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }// = SequentialGuid.Instance.Create(SequentialGuid.SequentialGuidDatabaseType.MySql).ToString().ToLower();
        public DateTime CreatTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
    /// <summary>
    /// 带商户的基础实体
    /// </summary>
    public class EntityTenantBase : EntityBase, IEntityTenantBase
    {
        [MaxLength(36)]
        public string TenantCode { get; set; }
    }
}