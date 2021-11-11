using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.StockApi.Models.ViewModels;

public class DeliveryRequestController : Controller
{
    private readonly IMediator _mediator;
    
    public DeliveryRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<List<DeliveryRequestViewModel>> GetAll()
    {
        
    }

    [HttpGet("active")]
    public async Task<List<DeliveryRequestViewModel>> GetAllActive()
    {
        
    }
}