using PkkService.Queries.Application.PkkServices;
using PkkService.Queries.Infrastructure.PkkServices.Typeahead.Contracts;

namespace PkkService.Queries.Infrastructure.PkkServices.Typeahead
{
    public class PkkTypeaheadService : IPkkTypeaheadService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly PkkServicesOptions _options;

        public PkkTypeaheadService(IHttpClientFactory clientFactory, IOptions<PkkServicesOptions> options)
        {
            _clientFactory = clientFactory;
            _options = options.Value;
        }

        /// <inheritdoc/>
        /// 
        public async Task<TypeaheadContract> FindPkkObjectsAddressOnLayer(int pkkLayer, string text, CancellationToken cancellationToken)
        {
            using var client = _clientFactory.CreateClient("pkk");

            using var response = await client.GetAsync($"{_options.TypeaheadApiMethod}/{pkkLayer}?text={text}", cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TypeaheadContract>(content);
        }

    }
}
