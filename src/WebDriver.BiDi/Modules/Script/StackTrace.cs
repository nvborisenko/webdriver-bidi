using System.Collections.Generic;
using System.Text;

namespace OpenQA.Selenium.BiDi.Modules.Script;

public record StackTrace(IReadOnlyCollection<StackFrame> CallFrames);
