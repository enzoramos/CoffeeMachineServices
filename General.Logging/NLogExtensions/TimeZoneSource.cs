using NLog.Time;
using System;
using System.ComponentModel.DataAnnotations;

namespace General.Logging.Extension
{
    /// <summary>
    /// Allow using a timezone for log timestamp.
    /// </summary>
    [TimeSource("TimeZoneTimeSource")]
    internal class TimeZoneSource : TimeSource
    {
        /// <summary>
        /// the timezone info.
        /// </summary>
        private TimeZoneInfo _tzi;

        /// <summary>
        /// The tinezone id.
        /// </summary>
        [Required]
        public string ZoneId
        {
            get { return _tzi.Id; }
            set { _tzi = TimeZoneInfo.FindSystemTimeZoneById(value); }
        }

        /// <summary>
        /// Give access to current time converted to timezone.
        /// </summary>
        public override DateTime Time
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _tzi); }
        }

        /// <summary>
        /// Convert systemTime to the timezone.
        /// </summary>
        /// <param name="systemTime">The system tine to convert.</param>
        /// <returns>The converted time.</returns>
        public override DateTime FromSystemTime(DateTime systemTime)
        {
            return TimeZoneInfo.ConvertTime(systemTime, _tzi);
        }
    }
}
