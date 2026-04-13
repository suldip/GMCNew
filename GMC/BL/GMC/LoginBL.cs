using GMC.Interface.GMC;
using GMC.Models.GMC;

namespace GMC.BL.GMC
{
    public class LoginBL : ILoginBL
    {
        private readonly ILoginRepo _repo;

        public LoginBL(ILoginRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> ValidateUser(LoginModel model)
        {
            return await _repo.ValidateUser(model);
        }
    }
}
