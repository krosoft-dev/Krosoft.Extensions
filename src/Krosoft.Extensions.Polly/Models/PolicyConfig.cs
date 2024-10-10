﻿namespace Krosoft.Extensions.Polly.Models;

public class PolicyConfig : ICircuitBreakerPolicyConfig, IRetryPolicyConfig
{
    public int BreakDuration { get; set; }
    public int RetryCount { get; set; }
    public int BackoffPower { get; set; }
}