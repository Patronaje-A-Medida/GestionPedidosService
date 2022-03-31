using AutoMapper;
using GestionPedidosService.Business.Extension;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
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

        public async Task<IEnumerable<GarmentMinMobile>> GetAllByQueryToMobile(GarmentQuery query)
        {
            try
            {
                var garments = await _uof.garmentRepository.GetAllByQuery(
                    query.AtelierId,
                    query.Categories,
                    query.Occasions,
                    query.Availabilities,
                    query.FilterString
                );
                var garmentsMin = _mapper.Map<IEnumerable<GarmentMinMobile>>(garments);
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

        public async Task<PagedList<GarmentMinWeb>> GetAllByQueryToWeb(GarmentQuery query)
        {
            try
            {
                var garments = await _uof.garmentRepository.GetAllByQuery(
                    query.AtelierId, 
                    query.Categories, 
                    query.Occasions,
                    query.Availabilities,
                    query.FilterString
                );
                /*var weas = new List<Garment>();
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);
                weas.AddRange(garments);*/
                var garmentsMin = _mapper.Map<ICollection<GarmentMinWeb>>(garments);
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

        public async Task<GarmentReadWeb> GetByCodeGarment_AtelierId(string codeGarment, int atelierId)
        {
            try
            {
                var garment = await _uof.garmentRepository.GetByCodeGarment_AtelierId(codeGarment, atelierId);
                var garmentRead = _mapper.Map<GarmentReadWeb>(garment);
                return garmentRead;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_GARMENT_FAILED,
                    ErrorMessages.GET_GARMENT_FAILED,
                    ex
                );
            }
        }

        public async Task<GarmentReadMobile> GetById(int id)
        {
            try
            {
                var garment = await _uof.garmentRepository.GetById(id);
                var garmentRead = _mapper.Map<GarmentReadMobile>(garment);
                return garmentRead;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_GARMENT_FAILED,
                    ErrorMessages.GET_GARMENT_FAILED,
                    ex
                );
            }
        }
    }
}
