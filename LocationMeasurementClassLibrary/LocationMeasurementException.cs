using System;

namespace FlirtyLocation {
    public class LocationMeasurementException : Exception {

        public LocationMeasurementException() : base() { }
        public LocationMeasurementException(string message) : base(message) { }

    }

    public class ExceptionUnexpectedDataTypeEncountered : LocationMeasurementException {
        private Type _expectedType;
        private Type _encounteredType;

        public ExceptionUnexpectedDataTypeEncountered(Type expected, Type encountered) {
            _expectedType = expected;
            _encounteredType = encountered;
        }

        public override string Message
        {
            get
            {
                return "EXCEPTION: expected data type " + _expectedType.Name + " but enountered data type " + _encounteredType.Name;
            }
        }
    }

    internal class MonthException : LocationMeasurementException {
        public MonthException(string message) : base(message) {
        }
    }

    internal class DayException : LocationMeasurementException {
        public DayException(string message)
            : base(message) {
        }
    }

    internal class HourException : LocationMeasurementException {
        public HourException(string message)
            : base(message) {
        }
    }

    internal class MinuteException : LocationMeasurementException {
        public MinuteException(string message)
            : base(message) {
        }
    }

    internal class SecondException : LocationMeasurementException {
        public SecondException(string message)
            : base(message) {
        }
    }

    internal class UtCoffsetException : LocationMeasurementException {
        public UtCoffsetException(string message)
            : base(message) {
        }
    }

    internal class LongitudeException : LocationMeasurementException {
        public LongitudeException(string message)
            : base(message) {
        }
    }

    internal class LatitudeException : LocationMeasurementException {
        public LatitudeException(string message)
            : base(message) {
        }
    }

    internal class ElevationException : LocationMeasurementException {
        public ElevationException(string message)
            : base(message) {
        }
    }
}
