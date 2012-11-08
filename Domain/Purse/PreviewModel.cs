using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Domain.Purse
{
    public class PreviewModel
    {
        public Month CurrentMonth()
        {
            var currentYear = Years.FirstOrDefault(x => x.Name == DateTime.Now.Year);
            return currentYear != null ? currentYear.Months[DateTime.Now.Month - 1] : new Month(DateTime.Now.Month, DateTime.Now.Year);

        }


        public Collection<Year> Years = new Collection<Year>();

        public PreviewModel(int beginYear, int endYear)
        {
            if (endYear < 2012 && beginYear > 2020)
                beginYear = 2012;
            if (endYear < 2012 && endYear > 2030)
                endYear = 2030;
            for (int i = beginYear; i <= endYear; i++)
            {
                Years.Add(new Year(i));
            }
        }
        public void AddDaySpanOperation(int year, int month, int day, SingleOperation operation)
        {
            Years.First(x => x.Name == year).Months[month-1].Days[day-1].AddSpanOperation(operation);
        }
    }

    public class Year
    {
        public Collection<Month> Months = new Collection<Month>();
        public int Name;

        public Year(int numYear)
        {
            Name = numYear;
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
            operation.Id = operation.Id;
            SpanDaysSingleOperations.Add(operation);
            _sumSpan = SpanDaysSingleOperations.Sum(x => x.Value);
        }
      /*  public void ChangeSpanOperation(int id,SingleOperation operation)
        {
            SpanDaysSingleOperations.Where(x => x.Id == id).Select(x => x).First().OperationName = operation.OperationName;
            SpanDaysSingleOperations.Where(x => x.Id == id).Select(x => x).First().Value = operation.Value;
        }
        public void RemoveSpanOperation(int id)
        {
            SpanDaysSingleOperations.Remove(SpanDaysSingleOperations.Where(x => x.Id == id).Select(x => x).First());
        }*/
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
        public int Id { get; set; }
        public string OperationName { get; set; }
        public int Value { get; set; }
    }
}
