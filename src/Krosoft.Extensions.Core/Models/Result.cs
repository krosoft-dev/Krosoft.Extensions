﻿using Krosoft.Extensions.Core.Models.Exceptions;

namespace Krosoft.Extensions.Core.Models;

public readonly struct Result<T>
{
    public T? Value { get; }
    public Exception? Exception { get; }

    public Result(T value)
    {
        Value = value;
        Exception = null;
    }

    public Result(Exception e)
    {
        Exception = e;
        Value = default;
    }

    public T Validate()
    {
        if (IsFaulted)
        {
            throw new KrosoftMetierException(Exception!.Message);
        }

        return Value!;
    }

    public bool IsSuccess => Exception == null;
    public bool IsFaulted => !IsSuccess;
}








 