using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FlirtyLocation.WatchDataGPX_LegacyCode {
    public class TrackSegmentGpx : IEnumerable<TrackPointGpx> {
        // Note: This code is designed to be reading from files on the local machine, therefore IEnumerable rather than IQueryable

        private List<TrackPointGpx> _trackSegmentGpx;

        public TrackSegmentGpx() { _trackSegmentGpx = new List<TrackPointGpx>(); }

        public void AddTrackPointGpx(TrackPointGpx point) { _trackSegmentGpx.Add(point); }

        public TrackPointGpx ProvideTrackPointGpxAtIndex(int idx) { return _trackSegmentGpx.ElementAt<TrackPointGpx>(idx); }

        public int NumberOfPoints() { return _trackSegmentGpx.Count; }

        internal List<TrackPointGpx> ProvideSegmentPoints() { return _trackSegmentGpx; }

        // ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** ** 
        // INFORMATION ACCESS DISPLAY

        // Provide information regarding a GPS Track Segment that has been read in
        public string InfoString() {
            string info = "";

            info += "Number of points: " + NumberOfPoints().ToString() + Environment.NewLine;
            info += Environment.NewLine;

            info += "_ _ Latitude _ _ _ _ _ _ _ _ _ _" + Environment.NewLine;
            info += "Avg: " + AverageLatitude().ToString() + Environment.NewLine;
            info += "Max: " + MaxLatitude().ToString() + Environment.NewLine;
            info += "Min: " + MinLatitude().ToString() + Environment.NewLine;
            info += Environment.NewLine;

            info += "_ _ Longitude _ _ _ _ _ _ _ _ _ _" + Environment.NewLine;
            info += "Avg: " + AverageLongitude().ToString() + Environment.NewLine;
            info += "Max: " + MaxLongitude().ToString() + Environment.NewLine;
            info += "Min: " + MinLongitude().ToString() + Environment.NewLine;
            info += Environment.NewLine;

            info += "_ _ Elevation (feet)  _ _ _ _ _ _" + Environment.NewLine;
            info += "Avg: " + AverageElevation().ToString() + Environment.NewLine;
            info += "Max: " + MaxElevation().ToString() + Environment.NewLine;
            info += "Min: " + MinElevation().ToString() + Environment.NewLine;
            info += "Ascent: " + CumulativeAscent().ToString() + Environment.NewLine;
            info += "Descent: " + CumulativeDescent().ToString() + Environment.NewLine;
            info += Environment.NewLine;

            info += "_ _ DISTANCE (miles)  _ _ _ _ _ _" + Environment.NewLine;
            info += "Distance: " + CumulativeDistance_Miles().ToString() + Environment.NewLine;
            info += "Distance (flat): " + CumulativeDistance_Flat_Miles().ToString() + Environment.NewLine;

            return info;
        }

        public double AverageLatitude() {
            double sumLatitudes = 0;
            double measures = 0;

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                sumLatitudes += p.GeoCoord.Latitude;
                ++measures;
            }

            return sumLatitudes / measures;
        }

        public double AverageLongitude() {
            double sumLongitude = 0;
            double measures = 0;

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                sumLongitude += p.GeoCoord.Longitude;
                ++measures;
            }

            return sumLongitude / measures;
        }

        public double MaxLatitude() {
            double max = _trackSegmentGpx[0].GeoCoord.Latitude;

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                if (p.GeoCoord.Latitude > max) { max = p.GeoCoord.Latitude; }
            }

            return max;
        }

        public double MaxLongitude() {
            double max = _trackSegmentGpx[0].GeoCoord.Longitude;

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                if (p.GeoCoord.Longitude > max) { max = p.GeoCoord.Longitude; }
            }

            return max;
        }

        public double MinLatitude() {
            double min = _trackSegmentGpx[0].GeoCoord.Latitude;

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                if (p.GeoCoord.Latitude < min) { min = p.GeoCoord.Latitude; }
            }

            return min;
        }

        public double MinLongitude() {
            double min = _trackSegmentGpx[0].GeoCoord.Longitude;

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                if (p.GeoCoord.Longitude < min) { min = p.GeoCoord.Longitude; }
            }

            return min;
        }

        public double AverageElevation() {
            double sumElevation = 0;
            double measures = 0;

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                sumElevation += p.Elevation.ElevationMeters;
                ++measures;
            }

            return (sumElevation / measures) * ConstantsRt.FEET_PER_METER;
        }

        public double MaxElevation() {
            double max = -500; // below the Dead Sea

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                if (p.Elevation.ElevationMeters > max) { max = p.Elevation.ElevationMeters; }
            }

            return max * ConstantsRt.FEET_PER_METER;
        }

        public double MinElevation() {
            double min = 10000; // above Mt Everest

            foreach (TrackPointGpx p in _trackSegmentGpx) {
                if (p.Elevation.ElevationMeters < min) { min = p.Elevation.ElevationMeters; }
            }

            return min * ConstantsRt.FEET_PER_METER;
        }

        // POTENTIAL IMPROVEMENT: Could make this more general by allowing parameters (with defaults) to specify the range over which to measure
        public double CumulativeAscent() {
            double cumul = 0;
            int n = NumberOfPoints();

            for (int i = 0; i < n - 2; ++i) {
                if (_trackSegmentGpx[i + 1].Elevation.ElevationMeters > _trackSegmentGpx[i].Elevation.ElevationMeters) {
                    cumul += (_trackSegmentGpx[i + 1].Elevation.ElevationMeters - _trackSegmentGpx[i].Elevation.ElevationMeters);
                }
            }

            return cumul * ConstantsRt.FEET_PER_METER;
        }

        // POTENTIAL IMPROVEMENT: Could make this more general by allowing parameters (with defaults) to specify the range over which to measure
        public double CumulativeDescent() {
            double cumul = 0;
            int n = NumberOfPoints();

            for (int i = 0; i < n - 2; ++i) {
                if (_trackSegmentGpx[i + 1].Elevation.ElevationMeters < _trackSegmentGpx[i].Elevation.ElevationMeters) {
                    cumul += (_trackSegmentGpx[i].Elevation.ElevationMeters - _trackSegmentGpx[i + 1].Elevation.ElevationMeters);
                }
            }

            return cumul * ConstantsRt.FEET_PER_METER;
        }

        // POTENTIAL IMPROVEMENT: Could make this more general by allowing parameters (with defaults) to specify the range over which to measure
        public double CumulativeDistance_Flat_Miles() {
            double cumul = 0;
            int n = NumberOfPoints();

            for (int i = 0; i < n - 2; ++i) {
                cumul += _trackSegmentGpx[i].DistanceBetween_Flat_Meters(_trackSegmentGpx[i + 1]);
            }

            return cumul / ConstantsRt.METERS_PER_MILE;
        }

        // POTENTIAL IMPROVEMENT: Could make this more general by allowing parameters (with defaults) to specify the range over which to measure
        public double CumulativeDistance_Miles() {
            double cumul = 0;
            int n = NumberOfPoints();

            for (int i = 0; i < n - 2; ++i) {
                cumul += _trackSegmentGpx[i].DistanceBetween_Meters(_trackSegmentGpx[i + 1]);
            }

            return cumul /
                ConstantsRt.METERS_PER_MILE;
        }


        #region IEnumerable_Implementation.

        public IEnumerator<TrackPointGpx> GetEnumerator() {
            foreach (var tp in _trackSegmentGpx) {
                yield return tp;
            }
        }

        private IEnumerator GetEnumerator1() {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator1();
        }


        #endregion IEnumerable_Implementation.
    }
}
