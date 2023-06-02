using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerAccount.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Application.Customers.Queries.Customers;

public record GetCustomersQuery : IRequest<List<GetCustomerDto>>
{
    public string? SearchText { get; init; }
}

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<GetCustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetCustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Where(x => string.IsNullOrEmpty(request.SearchText) ? true : x.FullName.Contains(request.SearchText))
            .OrderBy(x => x.Id)
            .ProjectTo<GetCustomerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
