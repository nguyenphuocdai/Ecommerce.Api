﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Streetwood.Core.Domain.Abstract.Repositories;
using Streetwood.Core.Domain.Entities;
using Streetwood.Infrastructure.Dto;
using Streetwood.Infrastructure.Services.Abstract.Queries;

namespace Streetwood.Infrastructure.Services.Implementations.Queries
{
    internal class ShipmentQueryService : IShipmentQueryService
    {
        private readonly IShipmentRepository shipmentRepository;
        private readonly IMapper mapper;

        public ShipmentQueryService(IShipmentRepository shipmentRepository, IMapper mapper)
        {
            this.shipmentRepository = shipmentRepository;
            this.mapper = mapper;
        }

        public async Task<IList<ShipmentDto>> GetAsync()
        {
            var shipment = await shipmentRepository.GetListAsync();
            return mapper.Map<IList<ShipmentDto>>(shipment);
        }

        public async Task<ShipmentDto> GetAsync(Guid id)
        {
            var shipment = await shipmentRepository.GetAndEnsureExistAsync(id);
            return mapper.Map<ShipmentDto>(shipment);
        }

        public async Task<Shipment> GetRawAsync(Guid id)
            => await shipmentRepository.GetAndEnsureExistAsync(id);
    }
}
