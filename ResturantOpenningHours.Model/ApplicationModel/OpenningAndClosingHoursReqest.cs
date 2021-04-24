using System;
using System.Collections.Generic;
using System.Text;

namespace ResturantOpenningHours.Model.ApplicationModel
{
    public class OpenHourModel
    {
        public string Type { get; set; }
        public int Value { get; set; }
    }
  
    public class OpenningAndClosingHoursReqest
    {
        public List<OpenHourModel> Monday { get; set; }
        public List<OpenHourModel> Tuesday { get; set; }
        public List<OpenHourModel> Wednesday { get; set; }
        public List<OpenHourModel> Thursday { get; set; }
        public List<OpenHourModel> Friday { get; set; }
        public List<OpenHourModel> Saturday { get; set; }
        public List<OpenHourModel> Sunday { get; set; }
    }

}
