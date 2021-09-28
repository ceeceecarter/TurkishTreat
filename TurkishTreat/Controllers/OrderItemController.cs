using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TurkishTreat.Data;
using TurkishTreat.Data.Entities;
using TurkishTreat.ViewModel;

namespace TurkishTreat.Controllers
{
    [Route("api/orders/{orderid}/items")]
    public class OrderItemController : Controller
    {
        private readonly IProductOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderItemController(IProductOrderRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repository.GetOrderById(orderId);
            var item = order.Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
        }
    }
}
