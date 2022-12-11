using AutoMapper;
using MediatR;
using Shopping.Api.IdentityMember.Application.Members;
using Shopping.Api.IdentityMember.Data;
using Shopping.Framework.Common;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.Api.IdentityMember.Application.Addresses
{
    public class AddressCreateCommand : IRequest<bool>, IToEntity<Models.Address>
    {
        public string MemberId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [MaxLength(50)]
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [MaxLength(50)]
        public string Latitude { get; set; }
        [MaxLength(50)]
        public string Province { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        [MaxLength(50)]
        public string County { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [MaxLength(50)]
        public string Street { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        [MaxLength(100)]
        public string AddressDetail { get; set; }
        /// <summary>
        /// 是否默认地址
        /// </summary>
        public bool IsDefault { get; set; }
    }
    public class AddressCreateCommandHandler : IRequestHandler<AddressCreateCommand, bool>
    {
        private readonly MemberDbContext _context;
        private readonly IMapper _mapper;
        public AddressCreateCommandHandler(MemberDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddressCreateCommand request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Models.Address>(request);
            await _context.Address.AddAsync(address);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
