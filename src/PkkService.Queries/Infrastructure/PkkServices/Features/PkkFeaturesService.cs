using PkkService.Queries.Application.PkkServices;
using PkkService.Queries.Infrastructure.PkkServices.Features.Contracts;

namespace PkkService.Queries.Infrastructure.PkkServices.Features
{
    public class PkkFeaturesService : IPkkFeaturesService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly PkkServicesOptions _options;

        public PkkFeaturesService(IHttpClientFactory clientFactory, IOptions<PkkServicesOptions> options)
        {
            _clientFactory = clientFactory;
            _options = options.Value;
        }

        /// <inheritdoc/>
        /// 
        public async Task<MatchedPkkObjectsContract> FindMatchedPkkObjectsOnLayer(int pkkLayer,
            string text,
            int? skip = 0,
            int? limit = 40,
            int? tolerance = 0,
            CancellationToken cancellationToken = default)
        {
            using var client = _clientFactory.CreateClient("pkk");

            using var response = await client.GetAsync($"{_options.FeaturesApiMethod}/{pkkLayer}?text={text}&limit={limit}&skip={skip}&tolerance={tolerance}", cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<MatchedPkkObjectsContract>(content);
        }

        /// <inheritdoc/>
        ///
        public async Task<PkkObjectInfoContract> FindPkkObjectInfo(int pkkLayer, string pkkObjectId, CancellationToken cancellationToken)
        {
            using var client = _clientFactory.CreateClient("pkk");

            using var response = await client.GetAsync($"{_options.FeaturesApiMethod}/{pkkLayer}/{pkkObjectId}", cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<PkkObjectInfoContract>(content);
        }
    }
}
