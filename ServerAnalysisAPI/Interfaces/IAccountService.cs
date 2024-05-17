namespace ServerAnalysisAPI.Interfaces;

public interface IAccountService
{
	Task CreateRolesAsync();
    Task<ServiceResult> RegisterUserAsync(RegisterInput input);
}
