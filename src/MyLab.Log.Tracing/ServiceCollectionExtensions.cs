
using System;
using MyLab.Log.Tracing;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Inject service to apply OpenTelemetry trace id for each log message which creates with DSL logger
        /// </summary>
        public static IServiceCollection AddDslLogTracing(this IServiceCollection srv)
        {
            if (srv == null) throw new ArgumentNullException(nameof(srv));

            return srv.AddDslLogContext<TraceIdDslLogContextApplier>();
        }
    }
}