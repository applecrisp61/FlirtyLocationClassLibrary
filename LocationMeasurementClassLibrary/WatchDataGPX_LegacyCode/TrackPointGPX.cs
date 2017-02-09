using System;
using System.Text;

namespace FlirtyLocation.WatchDataGPX_LegacyCode {
    public class TrackPointGpx {
        public TrackPointGpx(GeographicLocation gc, Elevation ele, TimeMeasure t, bool gcValid = true, bool eleValid = true) {
            _geoCoord = gc;
            _elevation = ele;
            _time = t;
            _geoCoordValid = gcValid;
            _elevationValid = eleValid;
        }

        private GeographicLocation _geoCoord;
        public GeographicLocation GeoCoord
        {
            get { return _geoCoord; }
            internal set { _geoCoord = value; }
        }

        private Elevation _elevation;
        public Elevation Elevation
        {
            get { return _elevation; }
            internal set { _elevation = value; }
        }

        private TimeMeasure _time;
        public TimeMeasure Time
        {
            get { return _time; }
            internal set { _time = value; }
        }

        private bool _geoCoordValid;
        public bool GeoCoordValid
        {
            get { return _geoCoordValid; }
            internal set { _geoCoordValid = value; }
        }

        private bool _elevationValid;
        public bool ElevationValid
        {
            get { return _elevationValid; }
            internal set { _elevationValid = value; }
        }

        // Returns the distance between this track point and another in meters
        // taking into account only the delta in x and y coordinates (changes in
        // latitude and longitude only... elevation deltas not considered here)
        public double DistanceBetween_Flat_Meters(TrackPointGpx other) {
            double xDistanceMeters = Math.Abs(this.GeoCoord.Longitude - other.GeoCoord.Longitude) * ConstantsRt.DEGREES_LONGITUDE_TO_METERS;
            double yDistanceMeters = Math.Abs(this.GeoCoord.Latitude - other.GeoCoord.Latitude) * ConstantsRt.DEGREES_LATITUDE_TO_METERS;

            // Hypotenuse of triangle: a^2 + b^2 = c^2, where c is hypotenuse; then solve for c
            return Math.Sqrt(Math.Pow(xDistanceMeters, 2) + Math.Pow(yDistanceMeters, 2));
        }

        public double DistanceBetween_Meters(TrackPointGpx other) {
            double flatDistanceMeters = DistanceBetween_Flat_Meters(other);
            double zDistanceMeters = Math.Abs(this.Elevation.ElevationMeters - other.Elevation.ElevationMeters);

            return Math.Sqrt(Math.Pow(flatDistanceMeters, 2) + Math.Pow(zDistanceMeters, 2));
        }

		public override string ToString() {

            var tp = new TrackPoint(this);
            return tp.ToString();
        }

    }
}
