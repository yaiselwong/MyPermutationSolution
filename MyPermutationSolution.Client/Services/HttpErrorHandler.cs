namespace MyPermutationSolution.Client.Services
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class HttpErrorHandler : DelegatingHandler
    {
        public HttpErrorHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                // Aquí puedes lanzar excepción para que la capture ErrorBoundary
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error {(int)response.StatusCode}: {response.ReasonPhrase} - {content}");
            }

            return response;
        }
    }
}
