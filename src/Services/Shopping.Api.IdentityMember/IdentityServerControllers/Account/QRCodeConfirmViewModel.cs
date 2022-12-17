namespace Shopping.Api.IdentityMember.IdentityServerControllers.Account
{
    public class QRCodeConfirmViewModel
    {
        public int Status { get; set; }
        public string ClientName { get; set; }
        public string QRCode { get; set; }
        public string Message { get; set; }
    }
}
