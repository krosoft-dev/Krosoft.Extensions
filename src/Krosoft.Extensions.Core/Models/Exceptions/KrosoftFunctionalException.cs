﻿using System.Net;
using Krosoft.Extensions.Core.Models.Exceptions.Http;

namespace Krosoft.Extensions.Core.Models.Exceptions;

/// <summary>
/// Exception à retourner en cas d'erreur technique soulevé par le code de l'application.
/// </summary>
public class KrosoftFunctionalException : HttpException
{
    public KrosoftFunctionalException(ISet<string> errors,
                                      Exception? innerException = null) : base(HttpStatusCode.BadRequest, errors.FirstOrDefault(), innerException)
    {
        Errors = errors;
    }

    public KrosoftFunctionalException(string erreur,
                                      Exception? innerException = null) : this(new HashSet<string> { erreur }, innerException)
    {
    }

    public IEnumerable<string> Errors { get; }
}