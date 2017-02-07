using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FlirtyLocation.WatchDataGPX_LegacyCode {
    public class GPXreader {
        private string _textToRead;

        private int _lineNumber;
        public int LineNumber
        {
            get { return _lineNumber; }
            internal set { _lineNumber = value; }
        }

        private string[] _gpxLines;

        private bool _rootOpened;
        public bool RootOpened { get { return _rootOpened; } }

        private bool _rootClosed;
        public bool RootClosed { get { return _rootClosed; } }

        private List<Object> _stackOfObjects;

        private string _delimiters = " <>\n\t";
        private List<string> _tokenStrings;

        public GPXreader(string textToRead) {

            _textToRead = textToRead;
            _gpxLines = Regex.Split(_textToRead, @"\r ?\n |\r");

            _lineNumber = 0;

            _rootOpened = false;
            _rootClosed = false;
            _stackOfObjects = new List<object>();

            _tokenStrings = new List<string> {
                "trkseg",
                "trkpt",
                "ele",
                "time",
                "extensions",
                "/extensions",
                "/trkpt",
                "/trkseg"
            };
        }

        public int GPXdata_LinesReadIn() { return _gpxLines.Count<string>(); }

        public string IdentifyToken() {
            string token = "";
            int tokenPos = 999999;

            string stringToEvaluate = Helpers.Trim(_gpxLines.ElementAt<string>(_lineNumber), _delimiters);

            foreach (string s in _tokenStrings) {
                int pos = stringToEvaluate.IndexOf(s);
                if ((pos >= 0) && (pos < tokenPos)) {
                    tokenPos = pos;
                    token = s;
                }
            }

            return token;
        }

        public TrackPointGpx ReadTrackPointGpx() {
            // Start by reading in the Track Point line (which will have the Lat and Lon on it)
            string currentString = _gpxLines.ElementAt<string>(_lineNumber);

            int startLat = currentString.IndexOf("lat=") + 5;
            int startLong = currentString.IndexOf("lon=") + 5; ;

            string latStr = "";
            string longStr = "";

            int idx = startLat;
            while (currentString[idx] != '"') { latStr += currentString[idx]; ++idx; }

            idx = startLong;
            while (currentString[idx] != '"') { longStr += currentString[idx]; ++idx; }

            double latitude = Convert.ToDouble(latStr);
            double longitude = Convert.ToDouble(longStr);
            GeographicLocation geo = new GeographicLocation(latitude, longitude);

            // Now move forward and do the Elevation and Time lines
            // For now, will not worry about allowing the order to come in differently
            // Both Suunto and Garmin seem to send the data in this order
            // NOTE: Strava premium GPX downoads do not include time stamps!!
            ++_lineNumber;
            Elevation ele = ReadElevation();
            ++LineNumber;

            TimeMeasure tm = new TimeMeasure();
            string nextToken = IdentifyToken();
            if (nextToken == "time") {
                tm = ReadTimeMeasure();
                ++LineNumber;
            }

            // have read in the data we want
            // DCL To Do: Do I want to get the distance extension for the Suunto watches?
            nextToken = "";
            do {
                nextToken = IdentifyToken();
                ++_lineNumber;
            } while (nextToken != "/trkpt");


            return new TrackPointGpx(geo, ele, tm);
        }

        public Elevation ReadElevation() {
            // start by reading in the line that has the Elevation XML specification on it
            string currentString = _gpxLines.ElementAt<string>(_lineNumber);

            int startEle = currentString.IndexOf("<ele>") + 5;

            string eleStr = "";
            int idx = startEle;
            while (currentString[idx] != '<') { eleStr += currentString[idx]; ++idx; }

            double elevation = Convert.ToDouble(eleStr);
            return new Elevation(elevation);
        }

        public TimeMeasure ReadTimeMeasure() {
            // start by reading in the line that has the Time XML specification on it
            string currentString = _gpxLines.ElementAt<string>(_lineNumber);

            int startOfTime = currentString.IndexOf("<time>") + 6;
            string yearStr = "";
            string monthStr = "";
            string dayStr = "";
            string hourUtcStr = "";
            string minUtcStr = "";
            string secUtcStr = "";

            // Read in all of the day information
            int idx = startOfTime;
            while (currentString[idx] != '-') { yearStr += currentString[idx]; ++idx; }
            ++idx;
            while (currentString[idx] != '-') { monthStr += currentString[idx]; ++idx; }
            ++idx;
            while (currentString[idx] != 'T') { dayStr += currentString[idx]; ++idx; }

            ++idx;
            while (currentString[idx] != ':') { hourUtcStr += currentString[idx]; ++idx; }
            ++idx;
            while (currentString[idx] != ':') { minUtcStr += currentString[idx]; ++idx; }
            ++idx;
            while ((currentString[idx] != '.') && (currentString[idx] != 'Z')) { secUtcStr += currentString[idx]; ++idx; }
            // If we encounter a ".", it's on to milliseconds, and who cares about that in ultra-running...
            // Otherwise, the Z signifies "Zulu-time" is being used and we are at the end of the time specification

            int year = Convert.ToInt32(yearStr);
            int month = Convert.ToInt32(monthStr);
            int day = Convert.ToInt32(dayStr);
            int hourUtc = Convert.ToInt32(hourUtcStr);
            int minUtc = Convert.ToInt32(minUtcStr);
            int secUtc = Convert.ToInt32(secUtcStr);
            int localTimeUtcOffset = 7; // Assume PDT for now (will work out this later... perhaps)

            return new TimeMeasure(year, month, day, hourUtc, minUtc, secUtc, localTimeUtcOffset);
        }

        public TrackSegmentGpx ReadTrackSegmentGpx() {
            TrackSegmentGpx seg = new TrackSegmentGpx();
            ++_lineNumber;

            string nextToken = IdentifyToken();
            while (nextToken == "trkpt") {
                TrackPointGpx point = ReadTrackPointGpx();
                seg.AddTrackPointGpx(point);
                nextToken = IdentifyToken();
            }

            return seg;
        }

        public void AdvanceToTrackSegmentGpx() {

            string nextToken = IdentifyToken();
            while (nextToken != "trkseg") {
                ++_lineNumber;
                nextToken = IdentifyToken();
            }
        }

        public TrackSegmentGpx ReadGPXData_FromTextString() {

            this.AdvanceToTrackSegmentGpx();
            return this.ReadTrackSegmentGpx();
        }

    }
}
