using OpenQA.Selenium.BiDi.Communication;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal class PerformActionsCommand(PerformActionsCommandParameters @params) : Command<PerformActionsCommandParameters>(@params);

internal class PerformActionsCommandParameters(BrowsingContext.BrowsingContext context) : CommandParameters
{
    public BrowsingContext.BrowsingContext Context { get; } = context;

    public IEnumerable<SourceActions>? Actions { get; set; }
}

public class PerformActionsOptions : CommandOptions
{
    public IEnumerable<SourceActions>? Actions { get; set; } = [];
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(KeySourceActions), "key")]
public abstract class SourceActions
{
    public static KeySourceActions Press(string text)
    {
        var keySourceActions = new KeySourceActions();

        foreach (var character in text)
        {
            keySourceActions.Actions.AddRange([
                new KeyDownAction(character.ToString()),
                new KeyUpAction(character.ToString())
                ]);
        }

        return keySourceActions;
    }
}

public class KeySourceActions : SourceActions
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public List<KeySourceAction> Actions { get; set; } = [];

    public new KeySourceActions Press(string text)
    {
        Actions.AddRange(SourceActions.Press(text).Actions);

        return this;
    }

    public KeySourceActions Pause(uint? duration = default)
    {
        Actions.Add(new KeyPauseAction { Duration = duration });

        return this;
    }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(KeyPauseAction), "pause")]
[JsonDerivedType(typeof(KeyDownAction), "keyDown")]
[JsonDerivedType(typeof(KeyUpAction), "keyUp")]
public abstract class KeySourceAction;

public class KeyPauseAction : KeySourceAction
{
    public uint? Duration { get; set; }
}

public class KeyDownAction(string value) : KeySourceAction
{
    public string Value { get; } = value;
}

public class KeyUpAction(string value) : KeySourceAction
{
    public string Value { get; } = value;
}
