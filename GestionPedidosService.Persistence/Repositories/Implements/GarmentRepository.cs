﻿using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Utils;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Handlers;
using GestionPedidosService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static GestionPedidosService.Domain.Utils.ErrorsUtil;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class GarmentRepository : Repository<Garment>, IGarmentRepository
    {
        public GarmentRepository(AppDbContext context) : base(context) { }

        #nullable enable
        public async Task<IEnumerable<Garment>> GetAllByQuery(int atelierdId, string? filterString, int? category)
        {
            try
            {
                /*var categoryEnum = Enum.GetValues(typeof(EGarmentCategories))
                .Cast<EGarmentCategories>()
                .FirstOrDefault(v => category == null || v.ToDescriptionString().Equals(category));*/

                var garments = await _context.Garments
                    .AsNoTracking()
                    .Include(g => g.FeatureGarments.Where(f => f.TypeFeature.Equals(EGarmentFeatures.images.ToString())))
                    .Where(g => g.AtelierId == atelierdId)
                    .Where(
                        g => filterString == null ||
                        (
                            g.CodeGarment.ToUpper().Contains(filterString.ToUpper()) ||
                            g.NameGarment.ToUpper().Contains(filterString.ToUpper())
                        )
                    )
                    .Where(g => category == null || g.Category.Equals(category))
                    .OrderBy(g => g.Category)
                    .ToListAsync();

                return garments ?? new List<Garment>();
            }
            catch(Exception ex)
            {
                throw new RepositoryException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_CONTEXT_ERROR,
                    ErrorMessages.GET_CONTEXT_ERROR,
                    ex);
            }
        }
    }
}