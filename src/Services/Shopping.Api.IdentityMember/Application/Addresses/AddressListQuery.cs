using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.IdentityMember.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.Application.Addresses
{
    public class AddressListQuery : IRequest<AddressListQueryResponse>
    {
        public string MemberId { get; set; }
    }
    public class AddressListQueryResponse
    {
        public List<Models.Address> List { get; set; }
    }
    public class AddressListQueryHandler : IRequestHandler<AddressListQuery, AddressListQueryResponse>
    {
        private readonly MemberDbContext _context;
        private readonly IMapper _mapper;
        public AddressListQueryHandler(MemberDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AddressListQueryResponse> Handle(AddressListQuery request, CancellationToken cancellationToken)
        {
            AddressListQueryResponse resp = new AddressListQueryResponse() { };

            var list = await _context.Address.Where(a => a.MemberId == request.MemberId && !a.IsDeleted).ToListAsync();
            resp.List = list;
            return resp;
        }
    }
}
