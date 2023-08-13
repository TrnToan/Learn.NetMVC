using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LearndotNetMVC.Helpers;
using Microsoft.Extensions.Options;

namespace LearndotNetMVC.Services;

public class ImageService
{
	private readonly Cloudinary _cloudinary;

	public ImageService(IOptions<CloudinarySettings> config)
	{
		var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
		_cloudinary = new Cloudinary(acc);
	}

	public ImageUploadResult AddImage(IFormFile file)
	{
		var result = new ImageUploadResult();
		if (file.Length> 0)
		{
			using var stream = file.OpenReadStream();
			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(file.FileName, stream),
				Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
			};
			result = _cloudinary.Upload(uploadParams);
		}
		return result;
	}

	public DeletionResult DeleteImage(string publicId)
	{
		var deleteParams = new DeletionParams(publicId);
		var result = _cloudinary.Destroy(deleteParams);

		return result;
	}
}
