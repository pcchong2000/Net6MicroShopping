// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace Shopping.Api.IdentityMember.IdentityServerControllers.Account
{
    public class LoginInputModel
    {
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
        public string QRCode { get; set; }
        
        public string QRCodeStatus { get; set; }
    }
}