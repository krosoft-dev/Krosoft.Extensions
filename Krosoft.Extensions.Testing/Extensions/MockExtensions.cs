using Microsoft.Extensions.Logging;
using Moq;

namespace Krosoft.Extensions.Testing.Extensions;

public static class MockExtensions
{
    public static void Verify<T>(this Mock<ILogger<T>> mock, LogLevel level, string message, Times times)
    {
        mock.Verify(x => x.Log(level,
                               It.IsAny<EventId>(),
                               It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(message)),
                               It.IsAny<Exception>(),
                               (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                    times);
    }
}