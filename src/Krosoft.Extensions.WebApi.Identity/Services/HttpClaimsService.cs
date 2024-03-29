﻿using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Identity.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Krosoft.Extensions.WebApi.Identity.Services;

public class HttpClaimsService : IClaimsService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClaimsService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public T? CheckClaim<T>(string claimName, Func<string, T> funcSucess, bool isRequired)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var claim = _httpContextAccessor.HttpContext.User.FindFirst(claimName);
            if (claim != null)
            {
                return funcSucess(claim.Value);
            }

            if (!isRequired)
            {
                return default;
            }
        }

        throw new KrosoftTechnicalException($"Le claim {claimName} n'existe pas.");
    }

    public T CheckClaims<T>(string claimName, Func<IEnumerable<string>, T> funcSucess)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var claims = _httpContextAccessor.HttpContext.User.FindAll(claimName).ToList();
            if (claims.Count > 0)
            {
                return funcSucess(claims.Select(c => c.Value));
            }
        }

        throw new KrosoftTechnicalException($"Le claim {claimName} n'existe pas.");
    }
}