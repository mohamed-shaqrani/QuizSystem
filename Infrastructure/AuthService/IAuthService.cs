using Infrastructure.ViewModel;

namespace Infrastructure.AuthService;
public interface IAuthService
{
    public Task<AuthModel> RegisterAsync(RegisterModel model);
}
