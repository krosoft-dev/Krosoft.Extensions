﻿namespace Krosoft.Extensions.WebApi.Models;

public record WebApiSettings
{
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    public string[] ExposedHeaders { get; set; } = Array.Empty<string>();
    public string[] Cultures { get; set; } = Array.Empty<string>();
}