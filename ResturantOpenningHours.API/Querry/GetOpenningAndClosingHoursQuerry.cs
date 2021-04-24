using MediatR;
using ResturantOpenningHours.Model.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantOpenningHours.API.Querry
{
    public class GetOpenningAndClosingHoursQuerry : IRequest<OpenningAndClosingHoursResponse>
    {
        public OpenningAndClosingHoursReqest openningAndClosingHoursReqests;
        public GetOpenningAndClosingHoursQuerry(OpenningAndClosingHoursReqest hoursReqests)
        {
            openningAndClosingHoursReqests = hoursReqests;
        }
    }
}
