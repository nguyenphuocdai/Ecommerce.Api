﻿using AutoMapper;
using Streetwood.Core.Domain.Entities;
using Streetwood.Infrastructure.Dto;

namespace Streetwood.Infrastructure.Mappers.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
            : base("Orders")
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.PayedDateTime, opt => opt.MapFrom(src => src.PayedDateTime ?? default))
                .ForMember(dest => dest.ClosedDateTime, opt => opt.MapFrom(src => src.ClosedDateTime ?? default))
                .ForMember(dest => dest.ShipmentDateTime, opt => opt.MapFrom(src => src.ShipmentDateTime ?? default));

            CreateMap<ProductOrder, ProductOrderDto>()
                .ForMember(dest => dest.ProductOrderCharms, opt => opt.MapFrom(src => src.ProductOrderCharms));

            CreateMap<ProductOrderCharm, ProductOrderCharmDto>();

            CreateMap<Order, OrdersListDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        }
    }
}