 public class TimeRange
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        private static readonly TimeSpan DAY = new TimeSpan(24, 0, 0);

        public TimeRange(TimeSpan begin, TimeSpan end)
        {
            StartTime = begin;
            EndTime = end;
        }

        public bool Contains(TimeSpan test)
        {
            return StartTime <= test && EndTime >= test;
        }

        public bool Contains(DateTime test)
        {
            var tod = test.TimeOfDay;
            if (tod < StartTime)
            {
                return Contains(tod + DAY);
            }
            return Contains(tod);
        }

        public DateTime GetStartDateTime(DateTime basedate)
        {
            return basedate.Date.Add(StartTime);
        }

        public DateTime GetEndDateTime(DateTime basedate)
        {
            if (basedate.TimeOfDay < StartTime && EndTime > DAY)
            {
                return basedate.Date.Add(EndTime - DAY);
            }
            return basedate.Date.Add(EndTime);
        }
    }
