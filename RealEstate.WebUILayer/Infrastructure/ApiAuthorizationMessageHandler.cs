using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace RealEstate.WebUILayer.Infrastructure
{
    public class ApiAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiAuthorizationMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "realestatetoken")?.Value;
            if (!string.IsNullOrWhiteSpace(token) && request.Headers.Authorization is null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
