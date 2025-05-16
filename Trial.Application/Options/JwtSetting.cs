namespace Trial.Application.Options
{
    public class JwtSetting
    {
        // الناشر (Issuer)  
        public string Issuer { get; set; }

        // الجمهور (Audience) 
        public string Audience { get; set; }

        // مفتاح التوقيع السري
        public string SecretKey { get; set; }

        // مدة انتهاء صلاحية التوكن بالدقائق
        public int ExpiryMinutes { get; set; } =  60;
    }
}
