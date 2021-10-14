namespace Domain
{
    public class Order : EntityTenantBase
    {
        public string MemberId { get; set; }
        public string OrderNo { get; set; }
        public decimal OrderAmount { get; set; }
        public int Status { get; set; }
    }
}