using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Modules.Script;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextScriptModule(BrowsingContext context, ScriptModule scriptModule)
{
    public async Task<PreloadScript> AddPreloadScriptAsync(string functionDeclaration, PreloadScriptOptions? options = default)
    {
        options ??= new();

        options.Contexts = [context];

        return await scriptModule.AddPreloadScriptAsync(functionDeclaration, options).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<RealmInfo>> GetRealmsAsync(GetRealmsOptions? options = default)
    {
        options ??= new();

        options.Context = context;

        return await scriptModule.GetRealmsAsync(options).ConfigureAwait(false);
    }

    public Task<EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise, EvaluateOptions? options = default)
    {
        return scriptModule.EvaluateAsync(expression, awaitPromise, new ContextTarget(context), options);
    }

    public Task<EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise, CallFunctionOptions? options = default)
    {
        return scriptModule.CallFunctionAsync(functionDeclaration, awaitPromise, new ContextTarget(context), options);
    }
}
