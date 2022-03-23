using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Extensions;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.Garments;
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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<OrderDetail>> GetAll()
        {
            return await _context.Set<OrderDetail>().Include(e => e.Garment).Include(e => e.Order).ToListAsync();
        }

        public async Task<OrderDetail> GetByCodeOrder_CodeGarment(string codeOrder, string codeGarment)
        {
            try
            {
                var orderDetail = await _context.OrderDetails
                .AsNoTracking()
                .Include(od => od.Order).ThenInclude(o => o.UserClient).ThenInclude(uc => uc.User)
                .Include(od => od.Order).ThenInclude(o => o.UserAtelier).ThenInclude(ua => ua.User)
                .Include(od => od.Garment).ThenInclude(g => g.FeatureGarments)
                .AsSplitQuery()
                .FirstOrDefaultAsync(od => od.Order.CodeOrder.Equals(codeOrder) && od.Garment.CodeGarment.Equals(codeGarment));
                
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_CONTEXT_ERROR,
                    ErrorMessages.GET_CONTEXT_ERROR,
                    ex);
            }
        }

        public async Task<OrderDetailRead> GetByCodeOrder_CodeGarment2(string codeOrder, string codeGarment)
        {
            var start = DateTime.Now;
            // MEJOR PERFOMANCE
            var dd = await _context.OrderDetails
                    .AsNoTracking()
                    .Select(od => new OrderDetailRead
                    {
                        CodeOrder = od.Order.CodeOrder,
                        OrderDate = od.Order.OrderDate,
                        OrderDetailStatus = od.OrderDetailStatus.ToDescriptionString(),
                        AttendedBy = od.Order.UserAtelier.User.NameUser + " " + od.Order.UserAtelier.User.LastNameUser,
                        Client = new UserClientMin
                        {
                            Email = od.Order.UserClient.User.Email,
                            NameClient = od.Order.UserClient.User.NameUser + " " + od.Order.UserClient.User.LastNameUser,
                            Phone = od.Order.UserClient.Phone
                        },
                        Garment = new CustomGarmentRead
                        {
                            CodeGarment = od.Garment.CodeGarment,
                            NameGarment = od.Garment.NameGarment,
                            Color = od.Color,
                            Quantity = od.Quantity,
                            EstimatedPrice = (od.Garment.FirstRangePrice + od.Garment.SecondRangePrice) / 2,
                            /*Features = od.Garment.FeatureGarments.Select(fg => new FeatureGarmentMin
                            {
                                Id = fg.Id,
                                Type = fg.TypeFeature,
                                Value = fg.Value
                            })*/
                        }
                    })
                    .FirstOrDefaultAsync(od => od.CodeOrder.Equals(codeOrder) && od.Garment.CodeGarment.Equals(codeGarment));

            var finish = DateTime.Now;
            Console.WriteLine($"------ COMPUTE EVALUATION METODO2 ------ " +
                    $"\n start at {start:HH:mm:ss.ffffff}" +
                    $"\n end at {finish:HH:mm:ss.ffffff}" +
                    $"\n difference {finish - start} ");

            return dd;
        }

        public override async Task<OrderDetail> GetById(int id)
        {
            return await _context.Set<OrderDetail>()
                .AsNoTracking()
                .Include(e => e.Garment)
                .Include(e => e.Order)
                .Include(e => e.Garment.FeatureGarments)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}