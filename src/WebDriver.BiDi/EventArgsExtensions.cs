using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    public static class EventArgsExtensions
    {
        public static async Task ContinueRequestAsync(this Network.BeforeRequestSentEventArgs args, Network.ContinueRequestParameters parameters)
        {
            parameters.Request = args.Request.Id;

            await args.Session.Network.ContinueRequestAsync(parameters).ConfigureAwait(false);
        }

        public static async Task ContinueRequestAsync(this Network.BeforeRequestSentEventArgs args, string? method = default)
        {
            var parameters = new Network.ContinueRequestParameters
            {
                Method = method,
            };

            await args.ContinueRequestAsync(parameters).ConfigureAwait(false);
        }
    }
}
