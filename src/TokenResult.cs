namespace IdentityServer4ClientForClientCredentialsFlow
{
    public class TokenResult
    {
        public string Access_Token { get; set; }
        public string Expires_In { get; set; }
        public string Token_Type { get; set; }
        public string Scopes { get; set; }

        public override string ToString()
        {
            return $"Bearer {Access_Token}";
        }
    }
}