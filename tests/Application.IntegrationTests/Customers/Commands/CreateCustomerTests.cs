using NUnit.Framework;
using AutoMapper;
using CustomerAccount.Infrastructure.Persistence;
using MediatR;
using Moq;
using CustomerAccount.Application.Common.Mapping;
using CustomerAccount.Application.Customers.Commands.CreateCustomer;
using FluentAssertions;

namespace Application.IntegrationTests.Customers.Commands;
internal class CreateCustomerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    public ApplicationDbContext context;

    public CreateCustomerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        context = MockDatabase.InitDatabase(_mediatorMock);
    }

    [Test]
    public async Task ShouldCreateCustomer()
    {
        CreateCustomerCommand command = new CreateCustomerCommand
        {
            Address = "Norway",
            FullName = "Mehdi Nemati",
            NationalCode = "123",
            WalletBalance = 1
        };

        var handler = new CreateCustomerCommandHandler(context);
        var result = await handler.Handle(command, CancellationToken.None);

        var list = context.Customers.FirstOrDefault();

        result.Should().BeGreaterThan(0);
        list.Should().NotBeNull();
        list!.FullName.Should().Be(command.FullName);
    }
}
