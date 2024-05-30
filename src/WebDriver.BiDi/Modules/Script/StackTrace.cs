using System.Collections.Generic;
using System.Text;

namespace OpenQA.Selenium.BiDi.Modules.Script;

public class StackTrace(IReadOnlyCollection<StackFrame> callFrames)
{
    public IReadOnlyCollection<StackFrame> CallFrames { get; } = callFrames;

    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var stackFrame in CallFrames)
        {
            builder.AppendLine(stackFrame.ToString());
        }

        return builder.ToString();
    }
}
