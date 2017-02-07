using System;

namespace FlirtyLocation {
    public struct GeographicLocation {

        public GeographicLocation(double latitude, double longitude) : this() {
            // Normalize values
            Latitude = latitude % 90;
            Longitude = longitude % 180;

        }

        public double Latitude { private set; get; }
        public double Longitude { private set; get; }

        public override string ToString() {
            return String.Format("{0}{1} {2}{3}",
                Dms(Math.Abs(Latitude)),
                Latitude >= 0 ? "N" : "S",
                Dms(Math.Abs(Longitude)),
                Longitude >= 0 ? "E" : "W");
        }

        // provides the DMS down to the tenth of a second which 
        // at Seattle's latitude is ~10 feet of latitude and 7 feet of longitude
        private string Dms(Double decimalDegrees) {
            int degrees = (int)decimalDegrees;
            decimalDegrees -= degrees;
            decimalDegrees *= 60;
            int minutes = (int)decimalDegrees;
            decimalDegrees -= minutes;
            decimalDegrees *= 60;
            double seconds = Math.Round(decimalDegrees, 1);

            // adjust for rounding irregularities
            if (seconds == 60) {
                seconds = 0;
                minutes += 1;
            }
            if (minutes == 60) {
                minutes = 0;
                degrees += 1;
            }

            //char symbolDegrees = (char)176;
            //char symbolMinutes = (char)39;
            //char symbolSeconds = (char)34;
            return String.Format("{0}°{1}\'{2}\"",
                degrees, minutes, seconds);
        }
    }

}
