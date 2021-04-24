using ResturantOpenningHours.Model.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantOpenningHours.API.Logic
{
    public class TimeConverter
    {
        /// <summary>  
        /// this action get the UTC time from the unix time 
        /// </summary>  
        public static string UnixTimeStampToShortTimeString(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime.ToShortTimeString();
        }
        /// <summary>  
        /// this handles the converstion for all the model
        /// </summary> 
        public OpenningAndClosingHoursResponse Converter(OpenningAndClosingHoursReqest list)
        {

            var times = new List<Time>();

            TimeSorteer(list.Monday, list.Sunday, list.Tuesday, "Sunday", "Tuesday", "Monday", times);
            TimeSorteer(list.Tuesday, list.Monday, list.Wednesday, "Monday", "Wednesday", "Tuesday", times);
            TimeSorteer(list.Wednesday, list.Tuesday, list.Thursday, "Tuesday", "Thursday", "Wednesday", times);
            TimeSorteer(list.Thursday, list.Wednesday, list.Friday, "Wednesday", "Friday", "Thursday", times);
            TimeSorteer(list.Friday, list.Thursday, list.Saturday, "Thursday", "Saturday", "Friday", times);
            TimeSorteer(list.Saturday, list.Friday, list.Sunday, "Friday", "Sunday", "Saturday", times);
            TimeSorteer(list.Sunday, list.Saturday, list.Monday, "Saturday", "Monday", "Sunday", times);


            return new OpenningAndClosingHoursResponse
            {
                Monday = string.Format("Monday: {0}", PrintTime(times, "Monday")),
                Tuesday = string.Format("Tuesday: {0}", PrintTime(times, "Tuesday")),
                Wednesday = string.Format("Wednesday: {0}", PrintTime(times, "Wednesday")),
                Thursday = string.Format("Thursday: {0}", PrintTime(times, "Thursday")),
                Friday = string.Format("Friday: {0}", PrintTime(times, "Friday")),
                Saturday = string.Format("Saturday: {0}", PrintTime(times, "Saturday")),
                Sunday = string.Format("Sunday: {0}", PrintTime(times, "Sunday"))
            };


        }

        public List<Time> TimeSorteer(List<OpenHourModel> openHourModel, List<OpenHourModel> previousDay, List<OpenHourModel> nextDay, string previousDayAsWord, string nextDayAsWord, string actualDay, List<Time> times)
        {
            if (openHourModel == null) return times;
            var allitems = openHourModel.OrderBy(x => x.Value).ToList();
            if (openHourModel.Count <= 0)
            {
                Time time = new Time();
                time.Day = actualDay;
                times.Add(time);
            }
            else
            {
                Time time = new Time();
                for (int i = 0; i < openHourModel.Count; i++)
                {
                    if (i == 0 && openHourModel[i].Type.ToLower() == "close")
                    {
                        var previousDayTime = times.Where(x => x.Day.Equals(previousDayAsWord)).FirstOrDefault();
                        if (previousDayTime != null)
                        {
                            previousDayTime.CloseTime = UnixTimeStampToShortTimeString(openHourModel[i].Value);
                            times[times.FindIndex(ind => ind.Day.Equals(previousDayAsWord))] = previousDayTime;
                        }
                    }
                    else
                    {
                        if (openHourModel[i].Type.ToLower().Equals("open"))
                        {
                            time.OpenTime = UnixTimeStampToShortTimeString(openHourModel[i].Value);
                        }
                        else if (openHourModel[i].Type.ToLower().Equals("close"))
                        {
                            time.CloseTime = UnixTimeStampToShortTimeString(openHourModel[i].Value);
                        }
                        time.Day = actualDay;
                        times.Add(time);
                    }
                }

            }
            return times;
        }

        public string PrintTime(List<Time> timer, string day)
        {
            var value = "";
            foreach (var item in timer)
            {
                if (item.Day.Equals(day))
                {
                    if (item.OpenTime == null && item.CloseTime == null)
                    {
                        value = "Closed";
                    }
                    else
                    {
                        value = string.Format("{0} - {1},", item.OpenTime, item.CloseTime);
                    }
                    break;
                }
            }
            return value;
        }
    }

    public class Time
    {
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string Day { get; set; }
    }
}
