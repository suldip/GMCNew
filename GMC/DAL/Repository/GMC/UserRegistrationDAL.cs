using System.Data;
using System.Data.SqlClient;
using GMC.Models.GMC;

namespace GMC.DAL.Repository.GMC
{
    public class UserRegistrationDAL
    {
        private readonly IConfiguration _configuration;
        string ConnectionString = "";

        public UserRegistrationDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("ConnectionToTele_Dashboard");
        }

        public bool RegisterUser(UserRegistrationModel user)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertUserRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@emailid", user.emailid);
                    cmd.Parameters.AddWithValue("@mobile", user.mobile);
                    cmd.Parameters.AddWithValue("@address", user.address);
                    cmd.Parameters.AddWithValue("@usertype", user.usertype);
                    cmd.Parameters.AddWithValue("@createdby", user.createdby ?? "System");

                    con.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Logs out exception
                return false;
            }
            return rowsAffected > 0;
        }
    }
}
