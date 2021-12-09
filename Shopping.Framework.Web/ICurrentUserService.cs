namespace Shopping.Framework.Web
{
    public interface ICurrentUserService
    {
        string? Id { get; set; }
        string? TenantId { get; set; }
        string? Name { get; set; }
    }
}