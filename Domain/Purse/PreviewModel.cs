using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Domain.Purse
{
    /// <summary>
    /// Class that provides a description of all user opertions
    /// </summary>
    public class PreviewModel
    {
        private readonly Collection<Year> _years = new Collection<Year>();
        private Month _currentMonth;
        private readonly Collection<string> _autocompleteTags = new Collection<string>(); 

        /// <summary>
        /// Add a new operation
        /// </summary>
        /// <param name="year">Year of operation</param>
        /// <param name="month">Month op operation</param>
        /// <param name="day">Day of operation</param>
        /// <param name="operation">Object of class <see cref="SingleOperation"/>, that provides a description of the new operation</param>
        public void AddDaySpanOperation(int year, int month, int day, SingleOperation operation)
        {
            if(_years.SingleOrDefault(x=>x.Name == year) == null)
                _years.Add(new Year(year));
            _years.Single(x => x.Name == year).GetMonth(month).GetDay(day).AddSpanOperation(operation);
            if(!_autocompleteTags.Contains(operation.OperationName))
                _autocompleteTags.Add(operation.OperationName);
        }
        /// <summary>
        /// Return tags for autocomplete
        /// </summary>
        /// <returns>Collection string</returns>
        public Collection<string> GetAutocompleteTags()
        {
            return _autocompleteTags;
        }
        /// <summary>
        /// Returns object <see cref="Month"/> of current month (for time if not usage <see cref="SetCurrentMonth"/>)
        /// </summary>
        /// <Returns>Object <see cref="Month"/></Returns>
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
        /// <summary>
        /// Returns object <see cref="Month"/> of next month
        /// </summary>
        /// <param name="month">Number of current month</param>
        /// <param name="year">Number of current year</param>
        /// <Returns>Object <see cref="Month"/></Returns>
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
        /// <summary>
        /// Returns object <see cref="Month"/> of last month
        /// </summary>
        /// <param name="month">Number of current month</param>
        /// <param name="year">Number of current year</param>
        /// <Returns>Object <see cref="Month"/></Returns>
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
        /// <summary>
        /// Set athe current mont
        /// </summary>
        /// <param name="month">Number of the month</param>
        /// <param name="year">Number of the year</param>
        public void SetCurrentMonth(int month, int year)
        {
            var currentYear = GetYear(year);
            _currentMonth = currentYear != null ? currentYear.GetMonth(month) : new Month(month, year);
        }
        /// <summary>
        /// Returns object <see cref="Year"/> of the year
        /// </summary>
        /// <param name="name">Number of the year</param>
        /// <Returns>Object <see cref="Year"/></Returns>
        public Year GetYear(int name)
        {
            var year = _years.SingleOrDefault(x => x.Name == name);
            if(year == null)
                _years.Add(new Year(name));
            return _years.Single(x => x.Name == name);
        }
    }
    /// <summary>
    /// Class that provides a description of the year
    /// </summary>
    public class Year
    {
        private readonly Collection<Month> _months = new Collection<Month>();
        private readonly Collection<SingleOperation> _yearSpanStatistics = new Collection<SingleOperation>();
        /// <summary>
        /// Number name of month
        /// </summary>
        public int Name;
        /// <summary>
        /// Constructor of <see cref="Year"/>
        /// </summary>
        /// <param name="numYear">Number of year</param>
        public Year(int numYear)
        {
            Name = numYear;
            for (int i = 1; i <= 12; i++)
            {
                _months.Add(new Month(i, numYear));
            }
        }
        /// <summary>
        /// Returns object <see cref="Month"/> for number
        /// </summary>
        /// <param name="name">Number of the month</param>
        /// <Returns></Returns>
        public Month GetMonth(int name)
        {
            return _months[name - 1];
        }
        /// <summary>
        /// Calculate statistics of year span operation
        /// </summary>
        /// <returns>Collection <see cref="SingleOperation"/> object<</returns>
        public Collection<SingleOperation> YearSpanStatistics()
        {
            foreach (var month in _months)
            {
                foreach (var item in month.SpanStatistics())
                {
                    SingleOperation item1 = item;
                    var y = _yearSpanStatistics.Where(x => item1 != null && x.OperationName == item1.OperationName).Select(x => x);
                    if (!y.Any())
                        _yearSpanStatistics.Add(new SingleOperation
                        {
                            OperationName = item.OperationName,
                            Value = item.Value
                        });
                    else
                        _yearSpanStatistics.Single(x => x.OperationName == item.OperationName).Value += item.Value;
                }
            }
            return _yearSpanStatistics;
        } 
    }
    /// <summary>
    /// Class that provides a description of the month
    /// </summary>
    public class Month
    {
        private readonly DateTime _date;
        private readonly Collection<Day> _days = new Collection<Day>();
        private readonly int _thisMonth;
        private readonly int _thisYear;
        private readonly Collection<SingleOperation> _monthSpanStatistics = new Collection<SingleOperation>();
        /// <summary>
        /// String name of the month
        /// </summary>
        public string Name;
        /// <summary>
        /// Constructor of <see cref="Month"/>
        /// </summary>
        /// <param name="numMonth">Number of month</param>
        /// <param name="numYear">Number of year</param>
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
        /// <summary>
        /// Returns object <see cref="Day"/> by date
        /// </summary>
        /// <param name="date">NUmber of date</param>
        /// <Returns>Object <see cref="Day"/></Returns>
        public Day GetDay(int date)
        {
            return _days[date - 1];
        }
        /// <summary>
        /// Returns all <see cref="Day"/>" of this month/>
        /// </summary>
        /// <returns>Collection <see cref="Day"/></returns>
        public Collection<Day> GetDays()
        {
            return _days;
        }
        /// <summary>
        /// Returns number of this year
        /// </summary>
        /// <returns>Number of this year</returns>
        public int GetThisYear()
        {
            return _thisYear;
        }
        /// <summary>
        /// Returns number of this month
        /// </summary>
        /// <returns>Number of this month</returns>
        public int GetThisMonth()
        {
            return _thisMonth;
        }
        /// <summary>
        /// Returns a sum of value all operations of this month
        /// </summary>
        /// <returns>Sum</returns>
        public int MonthSpanSum()
        {
            return _days.Sum(x => x.GetSumSpan());
        }
        /// <summary>
        /// Calculate statistics of month span operation
        /// </summary>
        /// <returns>Collection <see cref="SingleOperation"/> object</returns>
        public Collection<SingleOperation> SpanStatistics()
        {
            foreach (var day in _days)
            {
                foreach (var item in day.SpanDaysSingleOperations)
                {
                    SingleOperation item1 = item;
                    var y = _monthSpanStatistics.Where(x => item1 != null && x.OperationName == item1.OperationName).Select(x => x);
                    if (!y.Any())
                        _monthSpanStatistics.Add(new SingleOperation
                            {
                                OperationName = item.OperationName,
                                Value = item.Value
                            });
                    else
                        _monthSpanStatistics.Single(x => x.OperationName == item.OperationName).Value += item.Value;
                }
            }
            return _monthSpanStatistics;
        }
        /// <summary>
        /// Converts this month to Json(<see cref="MonthJSON"/>)
        /// </summary>
        /// <returns></returns>
        public MonthJSON ToJSON()
        {
            var daysJson = new Collection<DayJSON>();
            foreach (var day in _days)
            {
                daysJson.Add(day.ToJSON());
            }
            var temp = new MonthJSON
                {
                    Days = daysJson,
                    Name = Name,
                    ThisMonth = _thisMonth,
                    ThisYear = _thisYear,
                    MonthSumSpan = MonthSpanSum()
                };
            return temp;
        }
    }
    /// <summary>
    /// Class that provides a description of the day
    /// </summary>
    public class Day
    {
        private int _sumRent;
        private int _sumSpan;
        /// <summary>
        /// Number of this day
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// String name of this day
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Collection <see cref="SingleOperation"/> of all rent operation of this day
        /// </summary>
        public Collection<SingleOperation> RentDaysSingleOperations = new Collection<SingleOperation>();
        /// <summary>
        /// Collection <see cref="SingleOperation"/> of all span operation of this day
        /// </summary>
        public Collection<SingleOperation> SpanDaysSingleOperations = new Collection<SingleOperation>();
        /// <summary>
        /// Constructor of <see cref="Day"/>
        /// </summary>
        /// <param name="number">Number of the day</param>
        /// <param name="name">Name of the day</param>
        public Day(int number,string name)
        {
            Number = number;
            Name = name;
            _sumRent = RentDaysSingleOperations.Sum(x => x.Value);
            _sumSpan = SpanDaysSingleOperations.Sum(x => x.Value);
        }
        /// <summary>
        /// Add a rent operation to the day
        /// </summary>
        /// <param name="operation">Object <see cref="SingleOperation"/></param>
        public void AddRentOperation(SingleOperation operation)
        {
            RentDaysSingleOperations.Add(operation);
            _sumRent = RentDaysSingleOperations.Sum(x => x.Value);
        }
        /// <summary>
        /// Add a span operation to the day
        /// </summary>
        /// <param name="operation">Object <see cref="SingleOperation"/></param>
        public void AddSpanOperation(SingleOperation operation)
        {
            operation.Id = operation.Id;
            SpanDaysSingleOperations.Add(operation);
            _sumSpan = SpanDaysSingleOperations.Sum(x => x.Value);
        }
        /// <summary>
        /// Returns a sum of all rent operation of this day
        /// </summary>
        /// <returns>Sum</returns>
        public int GetSumRent()
        {
            return _sumRent;
        }
        /// <summary>
        /// Returns a sum of all span operation of this day
        /// </summary>
        /// <returns>Sum</returns>
        public int GetSumSpan()
        {
            return _sumSpan;
        }
        /// <summary>
        /// Converts this day to Json(<see cref="DayJSON"/>)
        /// </summary>
        /// <returns></returns>
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
    /// <summary>
    /// Class that provides a brief description of the operation
    /// </summary>
    public class SingleOperation
    {
        /// <summary>
        /// Unique idetifier of the opertaion
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the opertaion
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// Value of the opertaion
        /// </summary>
        public int Value { get; set; }
    }
    /// <summary>
    /// Class that provides a description of the MonthJSON
    /// </summary>
    public class MonthJSON
    {
        /// <summary>
        /// Returns all <see cref="DayJSON"/>" of this month/>
        /// </summary>
        /// <returns>Collection <see cref="DayJSON"/></returns>
        public Collection<DayJSON> Days = new Collection<DayJSON>();
        /// <summary>
        /// Returns number of this month
        /// </summary>
        /// <returns>Number of this month</returns>
        public int ThisMonth;
        /// <summary>
        /// Returns number of this year
        /// </summary>
        /// <returns>Number of this year</returns>
        public int ThisYear;
        /// <summary>
        /// String name of the month
        /// </summary>
        public string Name;
        /// <summary>
        /// Returns a sum of value all operations of this month
        /// </summary>
        /// <returns>Sum</returns>
        public int MonthSumSpan;
    }
    /// <summary>
    /// Class that provides a description of the DayJSON
    /// </summary>
    public class DayJSON
    {
        /// <summary>
        /// Returns a sum of all rent operation of this day
        /// </summary>
        /// <returns>Sum</returns>
        public int SumRent;
        /// <summary>
        /// Returns a sum of all span operation of this day
        /// </summary>
        /// <returns>Sum</returns>
        public int SumSpan;
        /// <summary>
        /// Number of this day
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// String name of the day
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Collection <see cref="SingleOperation"/> of all rent operation of this day
        /// </summary>
        public Collection<SingleOperation> RentDaysSingleOperations;
        /// <summary>
        /// Collection <see cref="SingleOperation"/> of all span operation of this day
        /// </summary>
        public Collection<SingleOperation> SpanDaysSingleOperations;
    }
}
