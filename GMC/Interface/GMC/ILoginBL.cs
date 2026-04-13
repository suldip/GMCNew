using GMC.Models.GMC;

namespace GMC.Interface.GMC
{
    public interface ILoginBL
    {
        Task<bool> ValidateUser(LoginModel model);
    }
}
