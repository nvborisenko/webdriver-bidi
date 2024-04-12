using OpenQA.Selenium.BiDi.Modules.Network;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    public static class EventArgsExtensions
    {
        public static async Task ContinueRequestAsync(this BeforeRequestSentEventArgs args, ContinueRequestParameters parameters)
        {
            parameters.Request = args.Request.Id;

            await args.Session.Network.ContinueRequestAsync(parameters).ConfigureAwait(false);
        }

        public static async Task ContinueRequestAsync(this BeforeRequestSentEventArgs args, string? method = default)
        {
            var parameters = new ContinueRequestParameters
            {
                Method = method,
            };

            await args.ContinueRequestAsync(parameters).ConfigureAwait(false);
        }
    }
}
