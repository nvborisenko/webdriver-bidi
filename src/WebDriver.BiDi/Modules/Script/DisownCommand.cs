using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class DisownCommand(DisownCommandParameters @params) : Command<DisownCommandParameters>(@params);

internal class DisownCommandParameters(IEnumerable<Handle> handles, Target target) : CommandParameters
{
    public IEnumerable<Handle> Handles { get; } = handles;

    public Target Target { get; } = target;
}