﻿using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Abstractions.Models.Enums;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Core.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Blocking.Services;

public class HttpAccessTokenProvider :  IAccessTokenProvider
{
     

       
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpAccessTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        //On affecte le token que s'il n'y en pas déjà un.
        if (_httpContextAccessor.HttpContext != null)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                if (token != null)
                {
                    return token;
                }
            }

            return string.Empty;
        }

        throw new KrosoftTechniqueException("HttpContext non défini.");
    }
}