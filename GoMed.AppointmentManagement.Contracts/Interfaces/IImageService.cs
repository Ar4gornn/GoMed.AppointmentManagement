namespace GoMed.AppointmentManagement.Contracts.Interfaces;

public interface IImageService
{
    Task<string> UploadImage(string imageName, Stream imageStream);
}