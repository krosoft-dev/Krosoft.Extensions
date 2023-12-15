using System.Text.RegularExpressions;
using Krosoft.Extensions.Core.Models.Exceptions;

namespace Krosoft.Extensions.Core.Helpers;

public static class RegexHelper
{
    public static readonly TimeSpan MatchTimeout = TimeSpan.FromMilliseconds(100);
}