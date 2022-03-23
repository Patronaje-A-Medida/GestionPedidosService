using GestionPedidosService.Domain.Entities;
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
        public async Task<IEnumerable<Garment>> GetAllByQuery(
            int atelierdId, 
            IEnumerable<int> categories, 
            IEnumerable<int> occasions,
            IEnumerable<bool> availabilities,
            string? filterString 
        )
        {
            try
            {
                /*var categoryEnum = Enum.GetValues(typeof(EGarmentCategories))
                .Cast<EGarmentCategories>()
                .FirstOrDefault(v => category == null || v.ToDescriptionString().Equals(category));*/

                var garments = await _context.Garments
                    .AsNoTracking()
                    .Include(g => g.FeatureGarments
                        .Where(f => 
                            f.TypeFeature.Equals(EGarmentFeatures.images.ToString()) || 
                            f.TypeFeature.Equals(EGarmentFeatures.occasion.ToString())
                        )
                    )
                    .Where(g => g.AtelierId == atelierdId)
                    .Where(
                        g => filterString == null ||
                        (
                            g.CodeGarment.ToUpper().Contains(filterString.ToUpper()) ||
                            g.NameGarment.ToUpper().Contains(filterString.ToUpper())
                        )
                    )
                    .Where(g => categories.Count() == 0 || categories.Contains(g.Category))
                    .Where(g => availabilities.Count() == 0 || availabilities.Contains(g.Available))
                    .OrderBy(g => g.Category)
                    .ToListAsync();

                // fuera del context porque no puede traducirlo a sql command
                // se intento por medio de group by, joins, linq methods, linq sintax pero nada
                // falto probrar por raw sql y procedures pero quedará pendiente
                garments = garments.Where(g =>
                        occasions.Count() == 0 ||
                        occasions.Intersect(g.FeatureGarments
                                .Where(f => f.TypeFeature.Equals(EGarmentFeatures.occasion.ToString()))
                                .Select(f => f.TypeFeatureValue)
                        )
                        .Any()
                    )
                    .ToList();

                return garments ?? new List<Garment>();
            }
            catch(Exception ex)
            {
                throw new RepositoryException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_CONTEXT_ERROR,
                    ErrorMessages.GET_CONTEXT_ERROR,
                    ex
                );
            }
        }

        public async Task<Garment> GetByCodeGarment_AtelierId(string codeGarment, int atelierId)
        {
            try
            {
                var garment = await _context.Garments
                .AsNoTracking()
                .Include(g => g.FeatureGarments)
                .Include(g => g.PatternGarments)
                .FirstOrDefaultAsync(g =>
                    g.CodeGarment.Equals(codeGarment) &&
                    g.AtelierId.Equals(atelierId)
                );

                return garment;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_GARMENT_FAILED,
                    ErrorMessages.GET_GARMENT_FAILED,
                    ex
                );
            }
                
        }
    }
}
