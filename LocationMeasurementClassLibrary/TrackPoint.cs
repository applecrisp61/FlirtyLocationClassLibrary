using System;
using FlirtyLocation.WatchDataGPX_LegacyCode;

namespace FlirtyLocation {
    public struct TrackPoint : IEquatable<TrackPoint> {

        private GeographicLocation _location;
        private DateTime _dateTimeStamp;
        private PlatformEnum _platform;
        private string _provider; // non-nullable type, but can be the empty string when unknown
        private bool _isFromMockProvider;

        private double? _altitude;
        private int? _buildingFloor;
        private double? _speed;
        private double? _bearing;
        private double? _horizontalAccuracy;
        private double? _verticalAccuracy;

        public TrackPoint(
            GeographicLocation location,
            DateTime datetime,
            PlatformEnum platform,
            string provider,
            bool isFromMockProvider,
            double? altitude = null,
            int? buildingFloor = null,
            double? speed = null,
            double? bearing = null,
            double? horizontalAccuracy = null,
            double? verticalAccuracy = null
            ) : this() {
            _location = location;
            _dateTimeStamp = datetime;
            _platform = platform;
            _isFromMockProvider = isFromMockProvider;
            _provider = provider;
            _altitude = altitude;
            _buildingFloor = buildingFloor;
            _speed = speed;
            _bearing = bearing;
            _horizontalAccuracy = horizontalAccuracy;
            _verticalAccuracy = verticalAccuracy;
        }

        public TrackPoint(TrackPointGpx gpx) : this() {
            _location = gpx.GeoCoord;
            _dateTimeStamp = gpx.Time.DateTimeUtc;
            _platform = PlatformEnum.Suunto;
            _provider = "Suunto Ambit 3 Peak (Dan's watch)";
            _isFromMockProvider = false;
        }

        public GeographicLocation Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public DateTime DateTimeStamp
        {
            get { return _dateTimeStamp; }
            set { _dateTimeStamp = value; }
        }

        public PlatformEnum Platform
        {
            get { return _platform; }
            set { _platform = value; }
        }

        public bool IsMockProvider
        {
            get { return _isFromMockProvider; }
            set { _isFromMockProvider = value; }
        }

        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        public double? Altitude
        {
            get { return _altitude; }
            set { _altitude = value; }
        }

        public int? BuildingFloor
        {
            get { return _buildingFloor; }
            set { _buildingFloor = value; }
        }

        public double? Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public double? Bearing
        {
            get { return _bearing; }
            set { _bearing = value; }
        }

        public double? HorizontalAccuracy
        {
            get { return _horizontalAccuracy; }
            set { _horizontalAccuracy = value; }
        }

        public double? VerticalAccuracy
        {
            get { return _verticalAccuracy; }
            set { _verticalAccuracy = value; }
        }

        public override string ToString() {
            return
                // Required elements
                "Location: " + _location.ToString() + Environment.NewLine
                + "Datetimestamp: " + Helpers.DateTimeStampString(_dateTimeStamp) + Environment.NewLine
                + "Platform: " + _platform.ToString() + Environment.NewLine
                + "Provider: " + _provider + Environment.NewLine
                + "IsMockProvider: " + _isFromMockProvider.ToString() + Environment.NewLine
                // Potentially null / missing elements
                + (_altitude.HasValue ? "Altitude: " + Helpers.ConvertToFeetString(_altitude) + " ft" + Environment.NewLine : "")
                + (_buildingFloor.HasValue ? "BuildingFloor: " + _buildingFloor.ToString() + Environment.NewLine : "")
                + (_speed.HasValue ? "Speed: " + Helpers.ConvertToMilesPerHourString(_speed) + " mph" + Environment.NewLine : "")
                + (_bearing.HasValue ? "Bearing: " + Math.Round(_bearing.Value, 1).ToString() + " degrees East of North" + Environment.NewLine : "")
                + (_horizontalAccuracy.HasValue ? "HorizontalAccuracy: " + Helpers.ConvertToFeetString(_horizontalAccuracy) + " ft" + Environment.NewLine : "")
                + (_verticalAccuracy.HasValue ? "VerticalAccuracy: " + Helpers.ConvertToFeetString(_verticalAccuracy) + " ft" + Environment.NewLine : "");
        }

        #region OperatorOverloads.

        public static bool operator ==(TrackPoint right, TrackPoint left) {
            return right.Equals(left);
        }

        public static bool operator !=(TrackPoint right, TrackPoint left) {
            return !right.Equals(left);
        }

        #endregion OperatorOverloads.

        #region Overrides.

        public override bool Equals(object right) {
            // check null; note that this pointer is never null in C# methods (would throw null pointer exception before call is invoked)
            if (object.ReferenceEquals(right, null)) { return false; }
            if (object.ReferenceEquals(this, right)) { return true; }
            if (this.GetType() != right.GetType()) { return false; }
            return this.Equals((TrackPoint)right);
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        #endregion Overrides.

        #region IEquatableImplementation.

        public bool Equals(TrackPoint other) {
            return this.ToString() == other.ToString();
        }

        #endregion IEquatableImplementation.
    }
}
