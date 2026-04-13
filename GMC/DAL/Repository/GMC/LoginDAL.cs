using System.Data;
using System.Data.SqlClient;
using GMC.Models.GMC;

namespace GMC.DAL.Repository.GMC
{
    public class LoginDAL
    {
        readonly IConfiguration _config;
        string conn = "";

        public LoginDAL(IConfiguration config)
        {
            _config = config;
            conn = _config["ConnectionStrings:ConnectionToTele_Dashboard"];
        }

        public async Task<bool> ValidateUser(LoginModel model)
        {
            using (SqlConnection connDash = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        await connDash.OpenAsync();
                        cmd.CommandText = "SELECT COUNT(*) FROM UserRegistration WHERE Username = @Username AND Password = @Password";
                        cmd.Connection = connDash;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = model.Username ?? string.Empty;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = model.Password ?? string.Empty;
                        
                        int count = (int)await cmd.ExecuteScalarAsync();
                        return count > 0;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
