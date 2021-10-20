using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MyLab.Log.Dsl;
using OpenTelemetry;
using OpenTelemetry.Trace;
using Xunit;

namespace UnitTests
{
	public class LogTracingBehavior
	{

		[Fact]
		public void ShouldAddTraceIdAsFact()
		{
			//Arrange
			var activitySource = new ActivitySource(string.Empty);
			var builder = Sdk.CreateTracerProviderBuilder();
			builder.AddLegacySource("TestOperationName");
			using var provider = builder.Build();

			using var activity = activitySource.StartActivity("MyTestActivity");
			var sp = new ServiceCollection()
				.AddLogging(l => l.AddDsl())
				.AddDslLogTracing()
				.BuildServiceProvider();

			IDslLogger l = sp.GetService<IDslLogger<LogTracingBehavior>>();
			string currentTraceId = activity.Context.TraceId.ToHexString();

			//Act
			var log = l.Action("foo").Create();

			//Assert
			Assert.Equal(currentTraceId, log.Facts["trace-id"]);
		}
	}
}
