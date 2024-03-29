﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityTenant.Data;
using Shopping.Api.IdentityTenant.Models;
using Shopping.Framework.Domain.Base;
using Shopping.Identity.AccountApplication.AccountServices;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityTenant.TenantApplications.TenantAdmins
{
    public class RegisterTenantCommand : IRequest<RegisterTenantResponse>
    {
        public string TenantName { get; set; }
        public string TenantDescription { get; set; }
        public string TenantCode { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
    public class RegisterTenantResponse
    {
        public string Id { get; set; }
    }
    public class CreateTodoListCommandHandler : IRequestHandler<RegisterTenantCommand, RegisterTenantResponse>
    {
        private readonly TenantDbContext _context;
        private readonly IAccountManage<TenantAdmin, TenantDbContext> _accountManage;
        public CreateTodoListCommandHandler(TenantDbContext context, IAccountManage<TenantAdmin, TenantDbContext> accountManage)
        {
            _context = context;
            _accountManage = accountManage;
        }

        public async Task<RegisterTenantResponse> Handle(RegisterTenantCommand request, CancellationToken cancellationToken)
        {
            RegisterTenantResponse resp = new RegisterTenantResponse();
            if (await _context.TenantInfo.AnyAsync(a => a.TenantCode == request.TenantCode))
            {
                //resp.Code = ResponseBaseCode.Existed;
                //resp.Message = "商户号已存在";
                return resp;
            }
            if (await _context.TenantAdmin.AnyAsync(a => a.UserName == request.UserName))
            {
                //resp.Code = ResponseBaseCode.Existed;
                //resp.Message = "用户名已存在";
                return resp;
            }
            if (await _context.TenantAdmin.AnyAsync(a => a.Email == request.Email))
            {
                //resp.Code = ResponseBaseCode.Existed;
                //resp.Message = "邮箱已存在";
                return resp;
            }
            if (await _context.TenantAdmin.AnyAsync(a => a.PhoneNumber == request.PhoneNumber))
            {
                //resp.Code = ResponseBaseCode.Existed;
                //resp.Message = "手机号已存在";
                return resp;
            }

            var tenant = new TenantInfo()
            {
                Name = request.TenantName,
                TenantCode = request.TenantCode,
                Description = request.TenantDescription,
                Status = TenantStatus.Apply,
            };
            await _context.TenantInfo.AddAsync(tenant);


            var tenantAdmin = new TenantAdmin()
            {
                TenantId = tenant.Id,
                Name = request.TenantName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
            await _accountManage.Create(tenantAdmin, request.Password);

            resp.Id = tenantAdmin.Id;
            return resp;
        }
    }
}
