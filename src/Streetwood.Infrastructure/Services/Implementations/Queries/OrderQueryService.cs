﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Streetwood.Core.Domain.Abstract.Repositories;
using Streetwood.Core.Domain.Entities;
using Streetwood.Core.Domain.Enums;
using Streetwood.Core.Exceptions;
using Streetwood.Infrastructure.Dto;
using Streetwood.Infrastructure.Filters;
using Streetwood.Infrastructure.Services.Abstract.Queries;

namespace Streetwood.Infrastructure.Services.Implementations.Queries
{
    internal class OrderQueryService : IOrderQueryService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderQueryService(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<OrderDto> GetAsync(int id)
        {
            var order = await orderRepository.GetFullAndEnsureExistsAsync(id);
            var mapped = mapper.Map<OrderDto>(order);

            return mapped;
        }

        public async Task<Order> GetRawAndEnsureExistsAsync(int id)
            => await orderRepository.GetFullAndEnsureExistsAsync(id);

        public async Task<IList<OrdersListDto>> GetFilteredAsync(OrderQueryFilter filter)
        {
            var orders = orderRepository.GetQueryable()
                .AsTracking()
                .Include(s => s.User)
                .AsQueryable();

            if (filter.Id.HasValue)
            {
                var order = await orders.FirstOrDefaultAsync(s => s.Id == filter.Id.Value);
                if (filter.UserType == UserType.Customer && order.User.Id != filter.UserId)
                {
                   throw new StreetwoodException(ErrorCode.NoAccess);
                }

                return mapper.Map<IList<OrdersListDto>>(new List<Order> { order });
            }

            if (filter.UserType == UserType.Customer)
            {
                orders = orders.Where(s => s.User.Id == filter.UserId);
            }

            if (filter.DateFrom.HasValue)
            {
                orders = orders.Where(s => s.CreationDateTime >= filter.DateFrom.Value);
            }

            if (filter.DateTo.HasValue)
            {
                orders = orders.Where(s => s.CreationDateTime <= filter.DateTo.Value);
            }

            if (filter.IsClosed.HasValue)
            {
                orders = orders.Where(s => s.IsClosed == filter.IsClosed);
            }

            if (filter.IsPayed.HasValue)
            {
                orders = orders.Where(s => s.IsPayed == filter.IsPayed);
            }

            if (filter.IsShipped.HasValue)
            {
                orders = orders.Where(s => s.IsShipped == filter.IsShipped);
            }

            orders = orders.OrderByDescending(x => x.CreationDateTime);

            if (filter.Take.HasValue)
            {
                orders = orders.Take(filter.Take.Value);
            }

            var ordersList = await orders
                .ToListAsync();
            return mapper.Map<IList<OrdersListDto>>(ordersList);
        }
    }
}
