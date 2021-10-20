using MyLab.Log.Dsl;
using OpenTelemetry.Trace;

namespace MyLab.Log.Tracing
{
    class TraceIdDslLogContextApplier : IDslLogContextApplier
    {
        public DslExpression Apply(DslExpression dslExpression)
        {
            return dslExpression.AndFactIs("trace-id", Tracer.CurrentSpan.Context.TraceId.ToHexString());
        }
    }
}
