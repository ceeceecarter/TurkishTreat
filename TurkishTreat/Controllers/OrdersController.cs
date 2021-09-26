using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TurkishTreat.Data;
using TurkishTreat.Data.Entities;
using TurkishTreat.ViewModel;

namespace TurkishTreat.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        private readonly IProductOrderRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IProductOrderRepository repository, ILogger<OrdersController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get orders: {e}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if (order == null) return NotFound();
                return Ok(order);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get orders: {e}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        return Created($"api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to saved order: {e}");
            }

            return BadRequest("Failed to save order.");
        }
    }
}
