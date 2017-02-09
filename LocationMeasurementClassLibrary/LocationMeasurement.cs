using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace FlirtyLocation {
    public class LocationMeasurement {
        // required data elements
		private string id;
		private int userid;
		private string username;
		private int trackid;
		private string trackname;
		private double latitude;
        private double longitude;
		private DateTime datetimestamp;
        private string provider;
		private bool isfrommockprovider;

        // optional data elements
		private double? elevation;
		private int? buildingfloor;
		private double? speed;
		private double? bearing;
		private double? horizontalaccuracy;
		private double? verticalaccuracy;

		[JsonProperty(PropertyName = nameof(id))] // Note: id is a special column name; must be idß
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = nameof(userid))]
        public int UserId
        {
            get { return userid; }
            set { userid = value; }
        }

        [JsonProperty(PropertyName = nameof(username))]
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        [JsonProperty(PropertyName = nameof(trackid))]
        public int TrackId
        {
            get { return trackid; }
            set { trackid = value; }
        }

        [JsonProperty(PropertyName = nameof(trackname))]
        public string TrackName
        {
            get { return trackname; }
            set { trackname = value; }
        }

        [JsonProperty(PropertyName = nameof(latitude))]
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        [JsonProperty(PropertyName = nameof(longitude))]
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        [JsonProperty(PropertyName = nameof(datetimestamp))]
        public DateTime DateTimeStamp
        {
            get { return datetimestamp; }
            set { datetimestamp = value; }
        }

        [JsonProperty(PropertyName = nameof(provider))]
        public string Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        [JsonProperty(PropertyName = nameof(isfrommockprovider))]
		public bool IsFromMockProvider
        {
            get { return isfrommockprovider; }
            set { isfrommockprovider = value; }
        }

        [JsonProperty(PropertyName = nameof(elevation))]
        public double? Elevation
        {
            get { return elevation; }
            set { elevation = value; }
        }

        [JsonProperty(PropertyName = nameof(buildingfloor))]
        public int? BuildingFloor
        {
            get { return buildingfloor; }
            set { buildingfloor = value; }
        }

        [JsonProperty(PropertyName = nameof(speed))]
        public double? Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        [JsonProperty(PropertyName = nameof(bearing))]
        public double? Bearing
        {
            get { return bearing; }
            set { bearing = value; }
        }

        [JsonProperty(PropertyName = nameof(horizontalaccuracy))]
        public double? HorizontalAccuracy
        {
            get { return horizontalaccuracy; }
            set { horizontalaccuracy = value; }
        }

        [JsonProperty(PropertyName = nameof(verticalaccuracy))]
        public double? VerticalAccuracy
        {
            get { return verticalaccuracy; }
            set { verticalaccuracy = value; }
        }

        [Version]
        public string Version { get; set; }

		// Default empty constructor
		public LocationMeasurement() { }

		/// Copy constructor: Deep copy
		public LocationMeasurement(LocationMeasurement toCopy) {
			TrackId = toCopy.TrackId;
			TrackName = toCopy.TrackName;
			UserId = toCopy.UserId;
			UserName = toCopy.UserName;
			Provider = toCopy.Provider;
			IsFromMockProvider = toCopy.IsFromMockProvider;
			Latitude = toCopy.Latitude;
			Longitude = toCopy.Longitude;
			DateTimeStamp = toCopy.DateTimeStamp;
			Elevation = toCopy.Elevation;
			BuildingFloor = toCopy.BuildingFloor;
			Speed = toCopy.Speed;
			Bearing = toCopy.Bearing;
			HorizontalAccuracy = toCopy.HorizontalAccuracy;
			VerticalAccuracy = toCopy.VerticalAccuracy;
		}
    }

}
