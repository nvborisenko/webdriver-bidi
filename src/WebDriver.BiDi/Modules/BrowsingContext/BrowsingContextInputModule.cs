using OpenQA.Selenium.BiDi.Modules.Log;
using System.Threading.Tasks;
using System;
using OpenQA.Selenium.BiDi.Modules.Input;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextInputModule
{
    private readonly BrowsingContext _context;
    private readonly InputModule _inputModule;

    public BrowsingContextInputModule(BrowsingContext context, InputModule inputModule)
    {
        _context = context;
        _inputModule = inputModule;
    }

    public Task PerformActionsAsync(IEnumerable<SourceActions> actions, PerformActionsOptions? options = default)
    {
        options ??= new();

        options.Actions = actions;

        return _inputModule.PerformActionsAsync(_context, options);
    }
}
