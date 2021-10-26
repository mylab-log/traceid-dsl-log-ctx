using MyLab.Log.Dsl;
using OpenTelemetry.Trace;

namespace MyLab.Log.Tracing
{
    /// <summary>
    /// Extends MyLab.Log.Dsl with trace-id fact
    /// </summary>
    public class TraceIdLogContext : IDslLogContextApplier
    {
        public DslExpression Apply(DslExpression dslExpression)
        {
            return Tracer.CurrentSpan.Context.IsValid
                ? dslExpression.AndFactIs("trace-id", Tracer.CurrentSpan.Context.TraceId.ToHexString())
                : dslExpression;
        }
    }
}
