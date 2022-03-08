﻿using AutoMapper;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Persistence.Handlers;
using GestionPedidosService.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static GestionPedidosService.Domain.Utils.ErrorsUtil;

namespace GestionPedidosService.Business.ServicesQuery.Implements
{
    public class GarmentServiceQuery : IGarmentServiceQuery
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GarmentServiceQuery(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GarmentMin>> GetAllByQuery(GarmentQuery query)
        {
            try
            {
                var garments = await _uof.garmentRepository.GetAllByQuery(query.AtelierId, query.FilterString, query.Category);
                var garmentsMin = _mapper.Map<IEnumerable<GarmentMin>>(garments);
                return garmentsMin;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_GARMENTS_FAILED,
                    ErrorMessages.GET_GARMENTS_FAILED,
                    ex
                    );
            }
        }
    }
}