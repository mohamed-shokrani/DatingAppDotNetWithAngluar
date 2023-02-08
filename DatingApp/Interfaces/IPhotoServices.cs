using CloudinaryDotNet.Actions;

namespace DatingApp.Interfaces
{
    public interface IPhotoServices
    {
        Task<ImageUploadResult> addPhotostoAsync(IFormFile file);
        Task<DeletionResult> DeletionPhotoAsync(string publicId);
    }
}
