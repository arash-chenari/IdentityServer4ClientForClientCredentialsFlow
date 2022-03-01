using System.Collections.Generic;

namespace IdentityServer4ClientForClientCredentialsFlow
{
    public class ClientCredentialConfig
    {
        public string ClientId { get; set; }
        public string TokenEndPointUrl { get; set; }
        public string ClientSecret { get; set; }
        public List<string> ApiScopes { get; set; }
    }
}