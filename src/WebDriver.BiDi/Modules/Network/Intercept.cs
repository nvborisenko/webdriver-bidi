﻿namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Intercept
{
    public Intercept(string id)
    {
        Id = id;
    }

    public string Id { get; private set; }
}
