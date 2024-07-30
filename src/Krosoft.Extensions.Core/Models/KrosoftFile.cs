﻿namespace Krosoft.Extensions.Core.Models;

public class KrosoftFile
{
    public KrosoftFile(string name, byte[] content)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; }
    public byte[] Content { get; }
}