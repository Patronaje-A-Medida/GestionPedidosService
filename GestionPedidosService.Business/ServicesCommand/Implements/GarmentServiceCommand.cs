using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using GestionPedidosService.Business.ServicesCommand.Interfaces;
using GestionPedidosService.Business.Utils;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Models.FeatureGarments;
using GestionPedidosService.Domain.Models.Garments;
using GestionPedidosService.Domain.Utils;
using GestionPedidosService.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesCommand.Implements
{
    public class GarmentServiceCommand : IGarmentServiceCommand
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IConfigurationManager _configManager;

        public GarmentServiceCommand(IUnitOfWork uof, IMapper mapper, IConfigurationManager configManager)
        {
            _uof = uof;
            _mapper = mapper;
            _configManager = configManager;
        }

        public async Task<bool> Save(GarmentWrite garmentWrite)
        {
            var garment = _mapper.Map<Garment>(garmentWrite);
            var imageFeatures = await UploadBatchGarmentImages(garmentWrite.Images);
            garment.FeatureGarments.AddRange(imageFeatures);
            
            var createdGarment = await _uof.garmentRepository.Add(garment);
            await _uof.SaveChangesAsync();

            return createdGarment != null;
        }

        private async Task<List<FeatureGarment>> UploadBatchGarmentImages(IEnumerable<GarmentImageString> garmentImageFiles)
        {
            var storageOptions = await LogInFirebase();
            var imageFeatures = new List<FeatureGarment>();

            foreach (var garmentImage in garmentImageFiles)
            {
                var imageFromBase64ToStream = ConvertBase64ToStream(garmentImage.Image);
                var imageStream = await imageFromBase64ToStream.ReadAsStreamAsync();
                var imgUrl = await UploadToFirebase(storageOptions, imageStream, garmentImage.FileName, garmentImage.FolderPath);
                imageFeatures.Add(new FeatureGarment
                { 
                    Value = imgUrl,
                    TypeFeature = EGarmentFeatures.images.ToString(),
                    TypeFeatureValue = (int)EGarmentFeatures.images,
                });
            }

            return imageFeatures;
        }

        private async Task<string> UploadToFirebase(FirebaseStorageOptions storageOptions, Stream imageStream, string fileName, string folderPath)
        {
            var storageTask = new FirebaseStorage(_configManager.FirebaseBucket, storageOptions)
                .Child(folderPath)
                //.Child("agregar id o nombre atelier")
                .Child(fileName)
                .PutAsync(imageStream);

            var url = await storageTask;
            return url;
        }

        private async Task<FirebaseStorageOptions> LogInFirebase()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_configManager.FirebaseApiKey));
            var authConfig = await authProvider.SignInWithEmailAndPasswordAsync(_configManager.FirebaseAuthEmail, _configManager.FirebaseAuthPwd);

            var firebaseStorageOptions = new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authConfig.FirebaseToken),
                ThrowOnCancel = true,
            };

            return firebaseStorageOptions;
        }

        /*----------------------------------------------*/
        public async Task<string> UploadGarmentImages(GarmentImageString garmentImage)
        {
            try
            {
                var imageFromBase64ToStream = ConvertBase64ToStream(garmentImage.Image);
                var imageStream = await imageFromBase64ToStream.ReadAsStreamAsync();
                var imgUrl = await UploadToFirebase(imageStream, garmentImage.FileName, garmentImage.FolderPath);

                return imgUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadGarmentImages(GarmentImageFile garmentImage)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    garmentImage.ImageFile.CopyTo(memoryStream);
                    var streamContent = ConvertByteArrayToStream(memoryStream.ToArray());
                    var imageStream = await streamContent.ReadAsStreamAsync();
                    var imgUrl = await UploadToFirebase(imageStream, garmentImage.FileName, garmentImage.FolderPath);

                    return imgUrl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private StreamContent ConvertBase64ToStream(string imageBase64)
        {
            byte[] imageByteArray = Convert.FromBase64String(imageBase64);
            StreamContent streamContent = new StreamContent(new MemoryStream(imageByteArray));
            return streamContent;
        }

        private StreamContent ConvertByteArrayToStream(byte[] imageByteArray)
        {
            StreamContent streamContent = new StreamContent(new MemoryStream(imageByteArray));
            return streamContent;
        }

        private async Task<string> UploadToFirebase(Stream imageStream, string fileName, string folderPath)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_configManager.FirebaseApiKey));
            var authConfig = await authProvider.SignInWithEmailAndPasswordAsync(_configManager.FirebaseAuthEmail, _configManager.FirebaseAuthPwd);
            
            var firebaseStorageOptions = new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authConfig.FirebaseToken),
                ThrowOnCancel = true,
            };

            var storageTask = new FirebaseStorage(_configManager.FirebaseBucket, firebaseStorageOptions)
                .Child(folderPath)
                .Child(fileName)
                .PutAsync(imageStream);

            var url = await storageTask;
            return url;
        }
    }
}
