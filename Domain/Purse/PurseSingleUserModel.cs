using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Domain.Purse
{
    public class PurseSingleUserModel
    {
        public Collection<Year> Years = new Collection<Year>();

        public PurseSingleUserModel(int beginYear, int endYear)
        {
            for (int i = beginYear; i <= endYear; i++)
            {
                Years.Add(new Year(i));
            }
        }
    }

    public class Year
    {
        public Collection<Month> Months = new Collection<Month>();

        public Year(int numYear)
        {
            for (int i = 1; i <= 12; i++)
            {
                Months.Add(new Month(i, numYear));
            }
        }
    }

    public class Month
    {
        private readonly DateTime _date;

        public Collection<Day> Days = new Collection<Day>();
        public string Name;

        public Month(int numMonth,int numYear)
        {
            _date = new DateTime(numYear, numMonth, 1);
            for (int i = 0; i < DateTime.DaysInMonth(numYear,numMonth); i++)
            {
                Days.Add(new Day(_date.Day, CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(_date.DayOfWeek)));
                _date = _date.AddDays(1);
            }
            Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(numMonth);
        }
    }

    public class Day
    {
        private int _sumRent;
        private int _sumSpan;

        public int Number { get; set; }
        public string Name { get; set; }
        public Collection<SingleOperation> RentDaysSingleOperations = new Collection<SingleOperation>();
        public Collection<SingleOperation> SpanDaysSingleOperations = new Collection<SingleOperation>();

        public Day(int number,string name)
        {
            Number = number;
            Name = name;
            _sumRent = RentDaysSingleOperations.Sum(x => x.Value);
            _sumSpan = SpanDaysSingleOperations.Sum(x => x.Value);
        }
        public void AddRentOperation(SingleOperation operation)
        {
            RentDaysSingleOperations.Add(operation);
            _sumRent = RentDaysSingleOperations.Sum(x => x.Value);
        }
        public void AddSpanOperation(SingleOperation operation)
        {
            SpanDaysSingleOperations.Add(operation);
            _sumSpan = SpanDaysSingleOperations.Sum(x => x.Value);
        }
        public int GetSumRent()
        {
            return _sumRent;
        }
        public int GetSumSpan()
        {
            return _sumSpan;
        }
    }

    public class SingleOperation
    {
        public string OperationName { get; set; }
        public int Value { get; set; }
    }
}
