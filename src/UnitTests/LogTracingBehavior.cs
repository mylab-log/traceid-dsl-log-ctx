using System;
using Microsoft.Extensions.DependencyInjection;
using MyLab.Log.Dsl;
using Xunit;

namespace UnitTests
{
    public class LogTracingBehavior
    {
        [Fact]
        public void ShouldAddTraceIdAsFact()
        {
            //Arrange
            var sp = new ServiceCollection()
                .AddLogging(l => l.AddDsl())
                .AddDslLogTracing()
                .BuildServiceProvider();

            IDslLogger l = sp.GetService<IDslLogger<LogTracingBehavior>>();

            string currentTraceId = "???";

            //Act
            var log = l.Action("foo").Create();

            //Assert
            Assert.Equal(currentTraceId, log.Facts["trace-id"]);
        }
    }
}
