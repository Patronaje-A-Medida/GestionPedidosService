using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesCommand.Interfaces;
using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models.Garments;
using GestionPedidosService.Domain.Utils;
using GestionPedidosService.Persistence.Handlers;
using GestionPedidosService.Persistence.Managers;
using GestionPedidosService.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static GestionPedidosService.Domain.Utils.ErrorsUtil;

namespace GestionPedidosService.Business.ServicesCommand.Implements
{
    public class GarmentServiceCommand : IGarmentServiceCommand
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IConfigurationManager _configManager;
        private readonly IPatternGarmentBaseCollectionServiceCommand _patternGarmentBaseServiceCommand;


        public GarmentServiceCommand(
            IUnitOfWork uof, 
            IMapper mapper, 
            IConfigurationManager configManager, 
            IPatternGarmentBaseCollectionServiceCommand patternGarmentBaseServiceCommand)
        {
            _uof = uof;
            _mapper = mapper;
            _configManager = configManager;
            _patternGarmentBaseServiceCommand = patternGarmentBaseServiceCommand;
        }

        public async Task<bool> Save(GarmentWrite garmentWrite)
        {
            try
            {

                string nameAtelier = (await _uof.atelierRepository.GetById(garmentWrite.AtelierId)).NameAtelier;
                var garment = _mapper.Map<Garment>(garmentWrite);

                if (garmentWrite.Images != null)
                {
                    var imageFeatures = await UploadBatchGarmentImages(garmentWrite.Images, nameAtelier);
                    garment.FeatureGarments.AddRange(imageFeatures);
                }

                if (garmentWrite.Patterns != null)
                {
                    var patternFeatures = await UploadBatchGarmentPatterns(garmentWrite.Patterns, nameAtelier);
                    garment.PatternGarments = patternFeatures;
                }


                var createdGarment = await _uof.garmentRepository.Add(garment);
                await _uof.SaveChangesAsync();

                // logica para guardar los patrones en otra DB
                List<PatternGarmentBase> patternBases = new List<PatternGarmentBase>();

                foreach(var pattern in createdGarment.PatternGarments)
                {
                    patternBases.Add(new PatternGarmentBase 
                    {
                        name_pattern = pattern.NamePattern,
                        garment_id = createdGarment.Id, 
                        image_pattern = pattern.ImagePattern 
                    });
                }
                //

                await _patternGarmentBaseServiceCommand.Add(patternBases);
                return createdGarment != null;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.ADD_GARMENT_FAILED,
                    ErrorMessages.ADD_GARMENT_FAILED,
                    ex
                    );
            }
            catch (ServiceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.ADD_GARMENT_FAILED,
                    ErrorMessages.ADD_GARMENT_FAILED,
                    ex
                    );
            }
        }

        public async Task<bool> Update(GarmentWrite garmentWrite)
        {
            try
            {
                
                var garment = await _uof.garmentRepository.GetByCodeGarment_AtelierId(garmentWrite.CodeGarment, garmentWrite.AtelierId);
                
                //garment.CodeGarment = garmentWrite.CodeGarment;
                garment.NameGarment = garmentWrite.NameGarment;
                garment.FirstRangePrice = garmentWrite.FirstRangePrice;
                garment.SecondRangePrice = garmentWrite.SecondRangePrice;
                garment.Description = garmentWrite.Description;
                garment.Category = garmentWrite.Category;
                garment.Available = garmentWrite.Available;

                var imagesTemp = garment.FeatureGarments.Where(x => x.TypeFeature.Equals("images"));
                var oldFeatures = garment.FeatureGarments;
                var newFeatures = _mapper.Map<Garment>(garmentWrite).FeatureGarments;
                var updateFeatures = new List<FeatureGarment>();
                bool exist = false;
                foreach (var newf in newFeatures)
                {
                    exist = false;
                    foreach (var oldf in oldFeatures)
                    {
                        if (newf.Value == oldf.Value)
                        {
                            exist = true;
                            updateFeatures.Add(oldf);
                            break;
                        }
                    }
                    if (!exist) updateFeatures.Add(newf);
                }

                updateFeatures.AddRange(imagesTemp);

                var deleteFeatrues = oldFeatures
                    .Where(x => !updateFeatures.Exists(y => y.Id == x.Id) && !x.TypeFeature.Equals("images"))
                    .ToList();
                _uof.featureGarmentRepository.Delete(deleteFeatrues);

                garment.FeatureGarments = updateFeatures;
                var garmentUpdated = _uof.garmentRepository.Update(garment);
                await _uof.SaveChangesAsync();
                return garmentUpdated != null;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.UPDATE_GARMENT_FAILED,
                    ErrorMessages.UPDATE_GARMENT_FAILED,
                    ex
                    );
            }
            catch(Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.UPDATE_GARMENT_FAILED,
                    ErrorMessages.UPDATE_GARMENT_FAILED,
                    ex
                    );
            }
        }

        public async Task<bool> UpdateImages(GarmentUpdateImages garmentUpdateImages)
        {
            try
            {
                var garment = await _uof.garmentRepository.GetByCodeGarment_AtelierId(garmentUpdateImages.CodeGarment, garmentUpdateImages.AtelierId);
                var nameAtelier = (await _uof.atelierRepository.GetById(garmentUpdateImages.AtelierId)).NameAtelier;

                if (garmentUpdateImages.Images == null) return false;

                var newImages = garmentUpdateImages.Images.Where(x => x.Id == 0);

                var deleteImages = garment.FeatureGarments
                    .Where(x => x.TypeFeature.Equals("images"))
                    .Where(x => !garmentUpdateImages.Images.Select(y => y.Id).Contains(x.Id));

                var newImageFeatures = new List<FeatureGarment>();
                var imageFeatures = await UploadBatchGarmentImages(
                    _mapper.Map<IEnumerable<GarmentImageString>>(newImages),
                    nameAtelier);
                newImageFeatures.AddRange(imageFeatures);

                _uof.featureGarmentRepository.Delete(deleteImages);
                garment.FeatureGarments.AddRange(newImageFeatures);

                var garmentUpdated = _uof.garmentRepository.Update(garment);
                await _uof.SaveChangesAsync();
                return garmentUpdated != null;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.UPDATE_IMAGE_PATTERN_FILES,
                    ErrorMessages.UPDATE_IMAGE_PATTERN_FILES,
                    ex
                    );
            }
            catch (ServiceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.UPDATE_IMAGE_PATTERN_FILES,
                    ErrorMessages.UPDATE_IMAGE_PATTERN_FILES,
                    ex
                    );
            }
        }

        public async Task<bool> UpdatePatterns(GarmentUpdateImages garmentUpdateImages)
        {
            try
            {
                var garment = await _uof.garmentRepository.GetByCodeGarment_AtelierId(garmentUpdateImages.CodeGarment, garmentUpdateImages.AtelierId);
                var nameAtelier = (await _uof.atelierRepository.GetById(garmentUpdateImages.AtelierId)).NameAtelier;

                if (garmentUpdateImages.Images == null) return false;

                var newPatterns = garmentUpdateImages.Images.Where(x => x.Id == 0);

                var deletePatterns = garment.PatternGarments
                    .Where(x => !garmentUpdateImages.Images.Select(y => y.Id).Contains(x.Id));

                var newPatternImages = new List<PatternGarment>();
                var patternFeatures = await UploadBatchGarmentPatterns(
                    _mapper.Map<IEnumerable<GarmentImageString>>(newPatterns),
                    nameAtelier);
                newPatternImages.AddRange(patternFeatures);

                _uof.patternGarmentRepository.Delete(deletePatterns);
                garment.PatternGarments.AddRange(newPatternImages);

                var garmentUpdated = _uof.garmentRepository.Update(garment);
                await _uof.SaveChangesAsync();
                return garmentUpdated != null;
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.UPDATE_IMAGE_PATTERN_FILES,
                    ErrorMessages.UPDATE_IMAGE_PATTERN_FILES,
                    ex
                    );
            }
            catch (ServiceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.UPDATE_IMAGE_PATTERN_FILES,
                    ErrorMessages.UPDATE_IMAGE_PATTERN_FILES,
                    ex
                    );
            }
        }


        private async Task<List<FeatureGarment>> UploadBatchGarmentImages(IEnumerable<GarmentImageString> garmentImageFiles, string atelier)
        {
            try
            {
                var storageOptions = await LogInFirebase();
                var imageFeatures = new List<FeatureGarment>();

                foreach (var garmentImage in garmentImageFiles)
                {
                    var imageFromBase64ToStream = ConvertBase64ToStream(garmentImage.Image);
                    var imageStream = await imageFromBase64ToStream.ReadAsStreamAsync();
                    var imgUrl = await UploadToFirebase(
                        storageOptions, 
                        imageStream, 
                        garmentImage.FileName, 
                        garmentImage.FolderPath, 
                        atelier
                    );

                    imageFeatures.Add(new FeatureGarment
                    {
                        Value = imgUrl,
                        TypeFeature = EGarmentFeatures.images.ToString(),
                        TypeFeatureValue = (int)EGarmentFeatures.images,
                    });
                }

                return imageFeatures;
            } catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.ADD_IMAGE_PATTERN_FILES,
                    ErrorMessages.ADD_IMAGE_PATTERN_FILES,
                    ex
                    );
            }
            
        }

        private async Task<List<PatternGarment>> UploadBatchGarmentPatterns(IEnumerable<GarmentImageString> garmentImageFiles, string atelier)
        {
            try
            {
                var storageOptions = await LogInFirebase();
                var patterns = new List<PatternGarment>();

                foreach (var garmentImage in garmentImageFiles)
                {
                    var imageFromBase64ToStream = ConvertBase64ToStream(garmentImage.Image);
                    var imageStream = await imageFromBase64ToStream.ReadAsStreamAsync();
                    var imgUrl = await UploadToFirebase(
                        storageOptions,
                        imageStream,
                        garmentImage.FileName,
                        garmentImage.FolderPath,
                        atelier
                    );

                    patterns.Add(new PatternGarment
                    {
                        NamePattern = garmentImage.FileName,
                        ImagePattern = imgUrl,
                        ResizedStatus = 0,
                        TypePattern = "base",
                    });
                }

                return patterns;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.ADD_IMAGE_PATTERN_FILES,
                    ErrorMessages.ADD_IMAGE_PATTERN_FILES,
                    ex
                    );
            }
        }
                
        private async Task<string> UploadToFirebase(
            FirebaseStorageOptions storageOptions, 
            Stream imageStream, 
            string fileName, 
            string folderPath,
            string atelier)
        {
            fileName = fileName.Replace(".png", "");
            fileName = fileName.Replace(".jpg", "");
            fileName = fileName.Replace(".jpeg", "");

            fileName = $"{fileName}-{DateTime.Now.Ticks}";
            
            var storageTask = new FirebaseStorage(_configManager.FirebaseBucket, storageOptions)
                .Child(folderPath)
                .Child(atelier)
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

        private StreamContent ConvertBase64ToStream(string imageBase64)
        {
            imageBase64 = imageBase64.Replace("data:image/png;base64,", "");
            imageBase64 = imageBase64.Replace("data:image/jpeg;base64,", "");
            byte[] imageByteArray = Convert.FromBase64String(imageBase64);
            StreamContent streamContent = new StreamContent(new MemoryStream(imageByteArray));
            return streamContent;
        }

        private StreamContent ConvertByteArrayToStream(byte[] imageByteArray)
        {
            StreamContent streamContent = new StreamContent(new MemoryStream(imageByteArray));
            return streamContent;
        }





        /*--------------------PRUEBAS--------------------------*/
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
