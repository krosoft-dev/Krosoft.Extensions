﻿namespace Krosoft.Extensions.Core.Attributes;

/// <summary>
/// Indicates to Code Analysis that a method validates a particular parameter.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class ValidatedNotNullAttribute : Attribute;