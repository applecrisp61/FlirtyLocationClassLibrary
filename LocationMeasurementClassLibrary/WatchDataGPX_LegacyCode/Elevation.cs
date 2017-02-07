
namespace FlirtyLocation.WatchDataGPX_LegacyCode {
    public class Elevation {

        // Default Constructor
        public Elevation(double altitude = 0) {
            _elevation = altitude;
        }

        private double _elevation;
        public double ElevationMeters
        {
            get { return _elevation; }
            internal set
            {
                if ((value > 30000) || (value < -500)) { throw new ElevationException("Elevation outside valid range of -500 to +30000m (lower than Dead Sea and higher than Everest)"); }
                _elevation = value;
            }
        }
    }
}
