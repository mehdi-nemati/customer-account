using CustomerAccount.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.IntegrationTests;

public static class MockDatabase
{
    public static ApplicationDbContext InitDatabase(Mock<IMediator> _mediatorMock)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new ApplicationDbContext(options, _mediatorMock.Object);

        return context;
    }
}

