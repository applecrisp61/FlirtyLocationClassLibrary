
using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace FlirtyLocation {
    public class Track : IEnumerable<TrackPoint> {

        private List<TrackPoint> _track;

        public Track() { _track = new List<TrackPoint>(); }
        public Track(WatchDataGPX_LegacyCode.TrackSegmentGpx seg) {
            _track = new List<TrackPoint>();
            foreach(var p in seg) {
                _track.Add(new TrackPoint(p));
            }
        }

        public bool IsEmpty { get { return _track == null; } }
        public void Add(TrackPoint measurement) { _track.Add(measurement); }
        public TrackPoint ElementAt(int idx) { return _track.ElementAt(idx); }
        public void RemoveAt(int idx) { _track.RemoveAt(idx); }
        public void Remove(TrackPoint measurement) { _track.Remove(measurement); }

        #region IEnumerableImplementation.

        public IEnumerator<TrackPoint> GetEnumerator() {
            for (int i = 0; i < _track.Count(); ++i) {
                yield return _track.ElementAt(i);
            }
        }

        private IEnumerator GetEnumerator1() {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator1();
        }

        #endregion IEnumerableImplementation.
    }
}
