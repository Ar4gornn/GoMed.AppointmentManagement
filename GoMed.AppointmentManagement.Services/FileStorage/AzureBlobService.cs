using Azure.Storage.Blobs;
using GoMed.AppointmentManagement.Contracts.Interfaces;

namespace GoMed.AppointmentManagement.Services.FileStorage;

public class AzureBlobService(BlobServiceClient blobServiceClient) : IImageService
{
    private const string ContainerName = "professional-user-image-container";

    public async Task<string> UploadImage(string imageName, Stream imageStream)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(imageName);
        await blobClient.UploadAsync(imageStream, true);
        return blobClient.Uri.ToString();
    }
}