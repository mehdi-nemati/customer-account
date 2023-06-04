using CustomerAccount.Application.Customers.Commands.CreateCustomer;
using CustomerAccount.Application.Customers.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CustomerAccount.WebAPI.Filters;
using CustomerAccount.Application.Customers.Commands.ChangeCustomerBalance;

namespace CustomerAccount.WebAPI.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetCustomerDto>>> GetCustomers([FromQuery] GetCustomersQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCustomerCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    public async Task<ActionResult<int>> ChangeCustomerBalance(ChangeCustomerBalanceCommand command)
    {
        return await _mediator.Send(command);
    }
}
