using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace Wonders.IntegrationTests
{
    public class InMemoryLogStoreProvider : ILoggerProvider
    {
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            var moq = new Mock<ILogger>();
            moq.Setup(l => l.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<object>(), null, It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel level, EventId id, object obj, Exception ex, Func<object, Exception, string> formatter) =>
                {
                    var eventLog = InMemoryLogStore.Instance;
                    eventLog.AddEntry(obj.ToString());
                });
            return moq.Object;
        }
    }
}