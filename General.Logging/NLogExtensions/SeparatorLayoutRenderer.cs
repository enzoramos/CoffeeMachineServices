using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace General.Logging.Extension
{
    /// <summary>
    /// Bring the configurable separator to the config level as a layout renderer.
    /// </summary>
    [LayoutRenderer("separator")]
    internal class SeparatorLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(LoggingSettings.Separator);
        }
    }
}
