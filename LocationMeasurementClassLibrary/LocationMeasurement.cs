using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace FlirtyLocation {
    public class LocationMeasurement {
        // required data elements
        private string _id;
        private int _userid;
        private string _username;
        private int _trackid;
        private string _trackname;
        private double _latitude;
        private double _longitude;
        private DateTime _datetimestamp;
        private string _provider;
        private bool _isfromfakeprovider;

        // optional data elements
        private double? _elevation;
        private int? _buildingfloor;
        private double? _speed;
        private double? _bearing;
        private double? _horizontalaccuracy;
        private double? _verticalaccuracy;

        [JsonProperty(PropertyName = nameof(_id))]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [JsonProperty(PropertyName = nameof(_userid))]
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        [JsonProperty(PropertyName = nameof(_username))]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        [JsonProperty(PropertyName = nameof(_trackid))]
        public int TrackId
        {
            get { return _trackid; }
            set { _trackid = value; }
        }

        [JsonProperty(PropertyName = nameof(_trackname))]
        public string TrackName
        {
            get { return _trackname; }
            set { _trackname = value; }
        }

        [JsonProperty(PropertyName = nameof(_latitude))]
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        [JsonProperty(PropertyName = nameof(_longitude))]
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        [JsonProperty(PropertyName = nameof(_datetimestamp))]
        public DateTime DateTimeStamp
        {
            get { return _datetimestamp; }
            set { _datetimestamp = value; }
        }

        [JsonProperty(PropertyName = nameof(_provider))]
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        [JsonProperty(PropertyName = nameof(_isfromfakeprovider))]
        public bool IsFromFakeProvider
        {
            get { return _isfromfakeprovider; }
            set { _isfromfakeprovider = value; }
        }

        [JsonProperty(PropertyName = nameof(_elevation))]
        public double? Elevation
        {
            get { return _elevation; }
            set { _elevation = value; }
        }

        [JsonProperty(PropertyName = nameof(_buildingfloor))]
        public int? BuildingFloor
        {
            get { return _buildingfloor; }
            set { _buildingfloor = value; }
        }

        [JsonProperty(PropertyName = nameof(_speed))]
        public double? Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        [JsonProperty(PropertyName = nameof(_bearing))]
        public double? Bearing
        {
            get { return _bearing; }
            set { _bearing = value; }
        }

        [JsonProperty(PropertyName = nameof(_horizontalaccuracy))]
        public double? HorizontalAccuracy
        {
            get { return _horizontalaccuracy; }
            set { _horizontalaccuracy = value; }
        }

        [JsonProperty(PropertyName = nameof(_verticalaccuracy))]
        public double? VerticalAccuracy
        {
            get { return _verticalaccuracy; }
            set { _verticalaccuracy = value; }
        }

        [Version]
        public string Version { get; set; }

        public string Trackname
        {
            get
            {
                return _trackname;
            }

            set
            {
                _trackname = value;
            }
        }
    }

}
