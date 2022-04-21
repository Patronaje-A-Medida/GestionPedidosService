using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.Garments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesCommand.Interfaces
{
    public interface IGarmentServiceCommand
    {
        Task<bool> Save(GarmentWrite garmentWrite);

        Task<bool> Update(GarmentWrite garmentWrite);
        Task<bool> Update2(GarmentWrite garmentWrite);
        Task<bool> UpdateBatchGarmentImages(GarmentWrite garmentWrite);
        Task<string> UploadGarmentImages(GarmentImageString garmentImage);
        Task<string> UploadGarmentImages(GarmentImageFile garmentImage);
    }
}
