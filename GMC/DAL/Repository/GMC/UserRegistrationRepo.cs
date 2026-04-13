using GMC.Models.GMC;

namespace GMC.DAL.Repository.GMC
{
    public interface IUserRegistrationRepo
    {
        bool RegisterUser(UserRegistrationModel user);
    }

    public class UserRegistrationRepo : IUserRegistrationRepo
    {
        private readonly UserRegistrationDAL _dal;

        public UserRegistrationRepo(UserRegistrationDAL dal)
        {
            _dal = dal;
        }

        public bool RegisterUser(UserRegistrationModel user)
        {
            return _dal.RegisterUser(user);
        }
    }
}
