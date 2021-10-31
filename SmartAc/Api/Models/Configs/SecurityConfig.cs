namespace Api.Models.Configs
{
    public class SecurityConfig
    {
        public string Secret { get; set; }
        public string Password { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
    }
}