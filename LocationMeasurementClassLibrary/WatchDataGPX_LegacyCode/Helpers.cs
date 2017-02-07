using System;

namespace FlirtyLocation.WatchDataGPX_LegacyCode {
    public static class Helpers {

        public static string DateTimeStampString(DateTime dateTime) {

            string milli = dateTime.Millisecond.ToString();
            while (milli.Length < 3) {
                milli = "0" + milli;
            }

            return
                dateTime.Month.ToString() + "/"
                + dateTime.Day.ToString() + "/"
                + dateTime.Year.ToString() + " "
                + dateTime.Hour.ToString() + ":"
                + dateTime.Minute.ToString() + ":"
                + dateTime.Second.ToString() + "."
                + milli;

        }

        public static string ConvertToFeetString(double? metersInput) {
            if (!metersInput.HasValue) { return ""; }

            return Math.Round(metersInput.Value * ConstantsRt.FEET_PER_METER).ToString();
        }

        public static string ConvertToMilesPerHourString(double? metersPerSecondInput) {
            if (!metersPerSecondInput.HasValue) { return ""; }

            return Math.Round(
                metersPerSecondInput.Value
                * ConstantsRt.FEET_PER_METER
                / ConstantsRt.FEET_PER_MILE
                * ConstantsRt.SECONDS_PER_HOUR, 1).ToString();
        }

        static public string Trim(string sourceString, string trimmables) {
            char[] trimChar = trimmables.ToCharArray();
            return sourceString.Trim(trimChar);
        }

    }
}
