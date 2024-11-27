namespace GatewaySolution
{
    public class RemoveReasonPhraseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Loại bỏ ReasonPhrase nếu cần
            if (request.Headers.Contains("ReasonPhrase"))
            {
                request.Headers.Remove("ReasonPhrase");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
