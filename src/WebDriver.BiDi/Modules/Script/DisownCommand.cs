using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class DisownCommand(DisownCommand.Parameters @params)
    : Command<DisownCommand.Parameters>("script.disown", @params)
{
    internal class Parameters(IEnumerable<Handle> handles, Target target) : CommandParameters
    {
        public IEnumerable<Handle> Handles { get; } = handles;

        public Target Target { get; } = target;
    }
}