using GMC.Interface.GMC;
using GMC.Models.GMC;

namespace GMC.DAL.Repository.GMC
{
    public class LoginRepo : ILoginRepo
    {
        private readonly LoginDAL _dal;

        public LoginRepo(LoginDAL dal)
        {
            _dal = dal;
        }

        public async Task<bool> ValidateUser(LoginModel model)
        {
            return await _dal.ValidateUser(model);
        }
    }
}
