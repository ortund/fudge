using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Utility
{
    public static class DateCalculations
    {
        private const int SECOND = 1;
        private const int MINUTE = 60 * SECOND;
        private const int HOUR = 60 * MINUTE;
        private const int DAY = 24 * HOUR;
        private const int MONTH = 30 * DAY;
        private const int ALTMONTH1 = 31 * DAY;
        private const int ALTMONTH2 = 28 * DAY;
        private const int ALTMONTH3 = 29 * DAY;

        public static string GetDateString(DateTime date)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * ALTMONTH2)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 28));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            if (delta < 12 * ALTMONTH3)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 29));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            if (delta < 12 * ALTMONTH1)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 31));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}