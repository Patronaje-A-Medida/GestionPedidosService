using AutoMapper;
using GestionPedidosService.Business.Extension;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.Garments;
using GestionPedidosService.Persistence.Handlers;
using GestionPedidosService.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
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

        public async Task<PagedList<GarmentMin>> GetAllByQuery(GarmentQuery query)
        {
            try
            {
                var garments = await _uof.garmentRepository.GetAllByQuery(query.AtelierId, query.FilterString, query.Category);
                var weas = new List<Garment>();
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);
                var garmentsMin = _mapper.Map<ICollection<GarmentMin>>(weas);
                return garmentsMin.ToPagedList(query.PageNumber, query.PageSize);
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
