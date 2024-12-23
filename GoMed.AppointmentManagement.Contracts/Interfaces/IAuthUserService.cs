namespace GoMed.AppointmentManagement.Contracts.Interfaces;

public interface IAuthUserService
{
    Guid UserId { get; init; }
    string UserName { get; init; }
    string UserType { get; init; }
    bool IsDevelopment { get; init; }
    bool IsEqualToUserId(Guid userId);
}