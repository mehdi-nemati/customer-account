using AutoMapper;
using CustomerAccount.Application.Common.Mapping;
using CustomerAccount.Application.Customers.Queries.Customers;
using CustomerAccount.Domain.Entities;
using CustomerAccount.Infrastructure.Persistence;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Application.IntegrationTests.Customers.Queries;
internal class GetCustomersTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly IMapper mapper;
    public ApplicationDbContext context;

    public GetCustomersTests()
    {
        _mediatorMock = new Mock<IMediator>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        mapper = config.CreateMapper();
        context = MockDatabase.InitDatabase(_mediatorMock);
    }

    [Test]
    public async Task ShouldReturnItem()
    {
        context.Add(new Customer
        {
            Address = "Norway",
            FullName = "Mehdi Nemati",
            NationalCode = "123",
            WalletBalance = 10
        });
        context.SaveChanges();

        var handler = new GetCustomersQueryHandler(context, mapper);
        var result = await handler.Handle(new GetCustomersQuery(), CancellationToken.None);

        result.ToList().Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnZeroItem()
    {
        var handler = new GetCustomersQueryHandler(context, mapper);
        var result = await handler.Handle(new GetCustomersQuery(), CancellationToken.None);

        result.ToList().Should().BeEmpty();
    }

    [Test]
    public async Task ShouldReturnItemThatNameContainMehdi()
    {
        context.Add(new Customer
        {
            Address = "Norway",
            FullName = "Mehdi Nemati",
            NationalCode = "123",
            WalletBalance = 10
        });
        context.Add(new Customer
        {
            Address = "Italia",
            FullName = "Jack Nemati",
            NationalCode = "3365",
            WalletBalance = 100
        });
        context.SaveChanges();


        var handler = new GetCustomersQueryHandler(context, mapper);
        var result = await handler.Handle(new GetCustomersQuery()
        {
            SearchText = "Mehdi"
        }, CancellationToken.None);

        result.ToList().Should().HaveCount(1);
    }
}
