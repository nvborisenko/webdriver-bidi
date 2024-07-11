using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Modules.Script;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextScriptModule
{
    private readonly BrowsingContext _context;
    private readonly ScriptModule _scriptModule;

    public BrowsingContextScriptModule(BrowsingContext context, ScriptModule scriptModule)
    {
        _context = context;
        _scriptModule = scriptModule;
    }

    public async Task<PreloadScript> AddPreloadScriptAsync(string functionDeclaration, PreloadScriptOptions? options = default)
    {
        options ??= new();

        options.Contexts = [_context];

        return await _scriptModule.AddPreloadScriptAsync(functionDeclaration, options).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<RealmInfo>> GetRealmsAsync(GetRealmsOptions? options = default)
    {
        options ??= new();

        options.Context = _context;

        return await _scriptModule.GetRealmsAsync(options).ConfigureAwait(false);
    }

    public Task<EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise, EvaluateOptions? options = default)
    {
        return _scriptModule.EvaluateAsync(expression, awaitPromise, new ContextTarget(_context), options);
    }

    public Task<EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise, CallFunctionOptions? options = default)
    {
        return _scriptModule.CallFunctionAsync(functionDeclaration, awaitPromise, new ContextTarget(_context), options);
    }
}
