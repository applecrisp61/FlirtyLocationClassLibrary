using System;

namespace FlirtyLocation.WatchDataGPX_LegacyCode {
    public class TimeMeasure {

        // Constructur
        // Set the defaults to be the approximate start time of the 2015 WR 50 mile race
        public TimeMeasure(int year = 2015,
                                int month = 7,
                                int day = 25,
                                int hourUtc = 13,
                                int minUtc = 3,
                                int secUtc = 12,
                                int utcOffset = 7) {
            _year = year;
            _month = month;
            _day = day;
            _hourUtc = hourUtc;
            _minUtc = minUtc;
            _secUtc = secUtc;
            _localHoursOffsetUtc = utcOffset;
        }


        private int _year;
        public int Year
        {
            get { return _year; }
            internal set { _year = value; }
        }

        private int _month;
        public int Month
        {
            get { return _month; }
            internal set
            {
                if ((value >= 1) && (value <= 12)) { _month = value; }
                else { throw new MonthException("EXCEPTION >> Month not in range 1 to 12"); }
            }
        }

        private int _day;
        public int Day
        {
            get { return _day; }
            internal set
            {
                if (Month == 2) {
                    if (((Year % 4) == 0) && ((Year % 100) != 0)) {
                        if ((value >= 1) && (value <= 29)) { _day = value; }
                        else { throw new DayException("EXCEPTION >> Month is in Feburary (leap year) but day is not in range 1 to 29"); }
                    }
                    else if ((Year % 400) == 0) {
                        if ((value >= 1) && (value <= 29)) { _day = value; }
                        else { throw new DayException("EXCEPTION >> Month is in Feburary (leap year) but day is not in range 1 to 29"); }
                    }
                    else {
                        if ((value >= 1) && (value <= 28)) { _day = value; }
                        else { throw new DayException("EXCEPTION >> Month is in Feburary (non-leap year) but day is not in range 1 to 28"); }
                    }
                }
                if ((Month == 4) || (Month == 6) || (Month == 9) || (Month == 11)) {
                    if ((value >= 1) && (value <= 30)) { _day = value; }
                    else { throw new DayException("EXCEPTION >> Month is in (4, 6, 9, or 11) but day is not in range 1 to 30"); }
                }
                else {
                    if ((value >= 1) && (value <= 31)) { _day = value; }
                    else { throw new DayException("EXCEPTION >> Month is in (1, 3, 5, 7, 8, 10, or 12) but day is not in range 1 to 31"); }
                }
            }
        }


        private int _hourUtc;
        public int HourUtc
        {
            get { return _hourUtc; }
            internal set
            {
                if ((value >= 0) && (value <= 23)) { _hourUtc = value; }
                else { throw new HourException("EXCEPTION >> Hour not in range 0 to 23"); }
            }
        }

        private int _minUtc;
        public int MinUtc
        {
            get { return _minUtc; }
            internal set
            {
                if ((value >= 0) && (value <= 59)) { _minUtc = value; }
                else { throw new MinuteException("EXCEPTION >> Minute not in range 0 to 59"); }
            }
        }

        private int _secUtc;
        public int SecUtc
        {
            get { return _secUtc; }
            internal set
            {
                if ((value >= 0) && (value <= 59)) { _secUtc = value; }
                else { throw new SecondException("EXCEPTION >> Second not in range 0 to 59"); }
            }
        }

        private int _localHoursOffsetUtc;
        public int LocalHoursOffsetUtc
        {
            get { return _localHoursOffsetUtc; }
            internal set
            {
                if ((value >= 0) && (value <= 23)) { _localHoursOffsetUtc = value; }
                else { throw new UtCoffsetException("EXCEPTION >> Local time zone offset from UTC not in range 0 to 23"); }
            }
        }

        public DateTime DateTimeUtc
        {
            get { return new DateTime(Year, Month, Day, HourUtc, MinUtc, SecUtc, 0, DateTimeKind.Utc); }
        }
    }
}
