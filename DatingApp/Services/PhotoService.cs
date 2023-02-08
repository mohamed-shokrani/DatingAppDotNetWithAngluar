using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.Helper;
using DatingApp.Interfaces;
using Microsoft.Extensions.Options;

namespace DatingApp.Services
{
    public class PhotoService : IPhotoServices
    {
        private readonly Cloudinary _cloudinary;// we need to give this our congfiguration of our api key
        public PhotoService(IOptions<CloudinarySettings> config)//and the way that we get our configuration   is when we use ioptions confs
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
               _cloudinary = new Cloudinary(acc);
        }   
        public async Task<ImageUploadResult> addPhotostoAsync(IFormFile file)
        {
           var uploadresult = new ImageUploadResult();

            if (file?.Length>0)
            {
                using var stream = file?.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file?.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")

                };
                uploadresult = await _cloudinary.UploadAsync(uploadParams);

            }
            return uploadresult;
        }

        public async Task<DeletionResult> DeletionPhotoAsync(string publicId)
        {
            var deletParams = new DeletionParams(publicId);

            var res = await _cloudinary.DestroyAsync(deletParams);
            return res;
            
        }
    }
}
 