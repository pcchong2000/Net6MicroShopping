﻿using MediatR;
using MicroShoping.Application;
using MicroShoping.Domain.Base;
using MicroShoping.Domain.Entities.Tenants;
using MicroShoping.EFCore.Tenants;
using Microsoft.EntityFrameworkCore;

namespace Tenant.Api.Applications.Commands
{
    public class CreateStoreCommand : IRequest<CreateStoreResponse>
    {
        public string CreatorId { get; set; }
        public string TenantId { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string StoreCode { get; set; }
        
    }
    public class CreateStoreResponse: RequestBase
    {
       
    }
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, CreateStoreResponse>
    {
        private readonly TenantDbContext _context;
        public CreateStoreCommandHandler(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<CreateStoreResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            CreateStoreResponse resp = new CreateStoreResponse();
            if (await _context.TenantStore.AnyAsync(a => a.TenantId == request.TenantId && a.StoreCode==request.StoreCode))
            {
                resp.Code = RequestBaseCode.Existed;
                resp.Message = "门店编号已存在";
                return resp;
            }

            var store = new TenantStore()
            {
                Name = request.StoreName,
                StoreCode = request.StoreCode,
                Description = request.StoreDescription,
                Status = TenantStoreStatus.Apply,
                CreatorId=request.CreatorId
            };
            await _context.TenantStore.AddAsync(store);
            await _context.SaveChangesAsync();

            resp.Code = RequestBaseCode.Success;
            resp.Message = "";

            return resp;
        }
    }
}
