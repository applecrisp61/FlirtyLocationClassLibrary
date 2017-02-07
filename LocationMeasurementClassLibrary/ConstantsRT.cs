
namespace FlirtyLocation {

    // RUN TIME CONSTANTS: As indicated through use of readonly rather than const (which would make them compile time constants)
    public class ConstantsRt {

        // Replace strings with your Azure Mobile App endpoint.
        public static string ApplicationUrl = @"https://pandaexpressgourmet.azurewebsites.net";

        public static readonly double FEET_PER_METER = 3.28084;
        public static readonly double FEET_PER_MILE = 5280;
        public static readonly double METERS_PER_MILE = 1609.34;
        public static readonly double SECONDS_PER_HOUR = 3600;

        public static readonly double DEGREES_LONGITUDE_TO_METERS = 76008; // At Latitude of 47.03359 (average WR 50 latitude)
        public static readonly double DEGREES_LATITUDE_TO_METERS = 111171; // At Latitude of 47.03359 (average WR 50 latitude)

        public static readonly double SEATTLE_LATITUDE = 47.6062;
        public static readonly double SEATTLE_LONGITUDE = -122.3321;

        public static readonly double TROLL_LATITUDE = 47.6510;
        public static readonly double TROLL_LONGITUDE = -122.3473;

        // LOCATION BEACONS
        public static readonly GeographicLocation BEACON_TROLL = new GeographicLocation(47.6510, -122.3473);
        public static readonly GeographicLocation BEACON_KITE_HILL = new GeographicLocation(47.645286, -122.336370);
        public static readonly GeographicLocation BEACON_THIRD_PLACE_BOOKS = new GeographicLocation(47.675928, -122.306443);
        public static readonly GeographicLocation BEACON_GREENLAKE_MILE1 = new GeographicLocation(47.671438, -122.343072);
        public static readonly GeographicLocation BEACON_ROANOKE_PARK = new GeographicLocation(47.643863, -122.320233);
        public static readonly GeographicLocation BEACON_CHINA_HARBOR = new GeographicLocation(47.637323, -122.340102);
        
        /*
        47.671438, -122.343072 // GL mile 1
        47.643863, -122.320233 // Roanoke Park
        47.637323, -122.340102 // China Harbor
        */
    }
}
