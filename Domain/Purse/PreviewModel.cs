using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Domain.Purse
{
    public class PreviewModel
    {
        private readonly Collection<Year> _years = new Collection<Year>();
        private Month _currentMonth;

        public void AddDaySpanOperation(int year, int month, int day, SingleOperation operation)
        {
            if(_years.SingleOrDefault(x=>x.Name == year) == null)
                _years.Add(new Year(year));
            _years.Single(x => x.Name == year).GetMonth(month).GetDay(day).AddSpanOperation(operation);
        }
        public Month CurrentMonth()
        {
            var currentYear = GetYear(DateTime.Now.Year);
            if (_currentMonth == null)
            {
                if (currentYear != null)
                    _currentMonth = currentYear.GetMonth(DateTime.Now.Month);
                else
                {
                    _years.Add(new Year(DateTime.Now.Year));
                    _currentMonth = new Month(DateTime.Now.Month, DateTime.Now.Year);
                }
            }
            return _currentMonth;
        }
        public Month NextMonth(int month, int year)
        {
            SetCurrentMonth(month, year);
            Year currentYear;
            if (_currentMonth.GetThisMonth() != 12)
            {
                currentYear = GetYear(_currentMonth.GetThisYear());
                _currentMonth = currentYear.GetMonth(_currentMonth.GetThisMonth() + 1);
                return _currentMonth;
            }
            currentYear = GetYear(_currentMonth.GetThisYear() + 1);
            if (currentYear != null)
                _currentMonth = currentYear.GetMonth(1);
            else
            {
                _years.Add(new Year(_currentMonth.GetThisYear() + 1));
                _currentMonth = new Month(1, _currentMonth.GetThisYear() + 1);
            }
            return _currentMonth;
        }
        public Month PrevMonth(int month, int year)
        {
            SetCurrentMonth(month, year);

            Year currentYear;
            if (_currentMonth.GetThisMonth() != 1)
            {
                currentYear = GetYear(_currentMonth.GetThisYear());
                _currentMonth = currentYear.GetMonth(_currentMonth.GetThisMonth() - 1);
                return _currentMonth;
            }
            currentYear = GetYear(_currentMonth.GetThisYear() - 1);
            if (currentYear != null)
                _currentMonth = currentYear.GetMonth(12);
            else
            {
                _years.Add(new Year(_currentMonth.GetThisYear() - 1));
                _currentMonth = new Month(12, _currentMonth.GetThisYear() - 1);
            }
            return _currentMonth;
        }
        public void SetCurrentMonth(int month, int year)
        {
            var currentYear = GetYear(year);
            _currentMonth = currentYear != null ? currentYear.GetMonth(month) : new Month(month, year);
        }
        public Year GetYear(int name)
        {
            var year = _years.SingleOrDefault(x => x.Name == name);
            if(year == null)
                _years.Add(new Year(name));
            return _years.Single(x => x.Name == name);
        }
    }

    public class Year
    {
        private readonly Collection<Month> _months = new Collection<Month>();
        public int Name;
        public Year(int numYear)
        {
            Name = numYear;
            for (int i = 1; i <= 12; i++)
            {
                _months.Add(new Month(i, numYear));
            }
        }
        public Month GetMonth(int name)
        {
            return _months[name - 1];
        }
    }

    public class Month
    {
        private readonly DateTime _date;
        private readonly Collection<Day> _days = new Collection<Day>();
        private readonly int _thisMonth;
        private readonly int _thisYear;

        public string Name;
        
        public Month(int numMonth,int numYear)
        {
            _thisMonth = numMonth;
            _thisYear = numYear;
            _date = new DateTime(numYear, numMonth, 1);
            for (int i = 0; i < DateTime.DaysInMonth(numYear,numMonth); i++)
            {
                _days.Add(new Day(_date.Day, CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(_date.DayOfWeek)));
                _date = _date.AddDays(1);
            }
            _date = _date.AddDays(-1);
            Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(numMonth);
        }
        public Day GetDay(int date)
        {
            return _days[date - 1];
        }
        public Collection<Day> GetDays()
        {
            return _days;
        }
        public int GetThisYear()
        {
            return _thisYear;
        }
        public int GetThisMonth()
        {
            return _thisMonth;
        }
        public MonthJSON ToJSON()
        {
            Collection<DayJSON> daysJson = new Collection<DayJSON>();
            foreach (var day in _days)
            {
                daysJson.Add(day.ToJSON());
            }
            MonthJSON temp = new MonthJSON
                {
                    Days = daysJson,
                    Name = Name,
                    ThisMonth = _thisMonth,
                    ThisYear = _thisYear
                };
            return temp;
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
            operation.Id = operation.Id;
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
        public DayJSON ToJSON()
        {
            return new DayJSON
                {
                    Name = Name,
                    Number = Number,
                    RentDaysSingleOperations = RentDaysSingleOperations,
                    SpanDaysSingleOperations = SpanDaysSingleOperations,
                    SumRent = _sumRent,
                    SumSpan = _sumSpan
                };
        }
    }

    public class SingleOperation
    {
        public int Id { get; set; }
        public string OperationName { get; set; }
        public int Value { get; set; }
    }

    public class MonthJSON
    {
        public Collection<DayJSON> Days = new Collection<DayJSON>();
        public int ThisMonth;
        public int ThisYear;
        public string Name;
    }

    public class DayJSON
    {
        public int SumRent;
        public int SumSpan;
        public int Number { get; set; }
        public string Name { get; set; }
        public Collection<SingleOperation> RentDaysSingleOperations;
        public Collection<SingleOperation> SpanDaysSingleOperations;
    }
}
