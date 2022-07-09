using PkkService.Queries.Application.Common;
using PkkService.Queries.Application.PkkServices;

namespace PkkService.Queries.Application.Queries.Features.PkkObjectInfo
{
    public class PkkObjectInfoHandler : IRequestHandler<PkkObjectInfoQuery, PkkInfoObjectResponse>
    {
        private readonly IPkkFeaturesService _featuresService;

        public PkkObjectInfoHandler(IPkkFeaturesService featuresService)
        {
            _featuresService = featuresService;
        }

        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="UnvaliablePkkServiceException"></exception>
        public async Task<PkkInfoObjectResponse> Handle(PkkObjectInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _featuresService.FindPkkObjectInfo(request.PkkLayer, request.PkkObjectId, cancellationToken);

                return new PkkInfoObjectResponse
                {
                    Attrs = result.Feature?.Attrs,
                    Type = result.Feature?.Type
                };
            }
            catch (HttpRequestException ex)
            {
                throw new UnvaliablePkkServiceException(ex.Message, ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new UnvaliablePkkServiceException(ex.Message, ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new UnvaliablePkkServiceException(ex.Message, ex);
            }
        }
    }
}
