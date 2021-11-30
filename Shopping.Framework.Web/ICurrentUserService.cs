namespace Shopping.Framework.Web
{
    public interface ICurrentUserService
    {
        string? UserId { get; set; }
        string? TenantId { get; set; }
        string? Name { get; set; }
    }
}