using GMC.Models.GMC;

namespace GMC.Interface.GMC
{
    public interface ILoginRepo
    {
        Task<bool> ValidateUser(LoginModel model);
    }
}
