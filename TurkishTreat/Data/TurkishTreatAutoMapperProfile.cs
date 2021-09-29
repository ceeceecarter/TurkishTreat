using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TurkishTreat.Data.Entities;
using TurkishTreat.ViewModel;

namespace TurkishTreat.Data
{
    public class TurkishTreatAutoMapperProfile : Profile
    {
        public TurkishTreatAutoMapperProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(d => d.OrderId, s => s.MapFrom(i => i.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap()
                .ForMember(d => d.Product, opt => opt.Ignore());
        }
    }
}
