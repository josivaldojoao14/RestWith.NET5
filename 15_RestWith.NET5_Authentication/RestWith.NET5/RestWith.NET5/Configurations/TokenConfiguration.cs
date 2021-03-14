namespace RestWith.NET5.Configurations
{
    // É preciso fazer configurações dessas propriedades no 'appsettings.json'
    public class TokenConfiguration
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Secrete { get; set; }

        public int Minutes { get; set; }

        public int DayToExpire { get; set; }
    }
}
