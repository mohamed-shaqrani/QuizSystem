using Infrastructure.ViewModel;

namespace Infrastructure.AuthService;
public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegisterModel model, string role);
    Task<AuthModel> GetTokenAsync(TokenRequestModel model);

}
