namespace CreditSystem.Utilities.ConfigHelper
{
    public class ConfigHelper
    {

        private readonly IConfiguration _configuration;

        public ConfigHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GetSecurityKey()
        {
            string? securityKey = null;

            try
            {
                securityKey = _configuration["SecurityKey"];
            }
            catch (Exception)
            {
                return null;
            }

            return securityKey;

        }

    }
}
