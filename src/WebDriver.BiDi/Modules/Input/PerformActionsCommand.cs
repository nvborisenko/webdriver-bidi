using OpenQA.Selenium.BiDi.Internal;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal class PerformActionsCommand : Command<PerformActionsParameters>
{
    public override string Method => "input.performActions";
}

public class PerformActionsParameters : EmptyCommandParameters
{
    public string Context { get; set; }

    public List<SourceActions> Actions { get; set; } = [];
}

[JsonDerivedType(typeof(KeySourceActions))]
public abstract class SourceActions
{
    public abstract string Type { get; }

    public static KeySourceActions Press(string text)
    {
        var keySourceActions = new KeySourceActions();

        foreach (var character in text)
        {
            keySourceActions.Actions.AddRange([
                new KeyDownAction { Value = character},
                new KeyUpAction { Value = character}
                ]);
        }

        return keySourceActions;
    }
}

public class KeySourceActions : SourceActions
{
    public override string Type => "key";

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public List<KeySourceAction> Actions { get; set; } = [];

    public new KeySourceActions Press(string text)
    {
        Actions.AddRange(SourceActions.Press(text).Actions);

        return this;
    }
}

[JsonDerivedType(typeof(KeyDownAction))]
[JsonDerivedType(typeof(KeyUpAction))]
public abstract class KeySourceAction
{
    public abstract string Type { get; }
}

public class KeyDownAction : KeySourceAction
{
    public override string Type => "keyDown";

    public char Value { get; set; }
}

public class KeyUpAction : KeySourceAction
{
    public override string Type => "keyUp";

    public char Value { get; set; }
}