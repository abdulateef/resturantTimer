using MediatR;
using ResturantOpenningHours.API.Logic;
using ResturantOpenningHours.API.Querry;
using ResturantOpenningHours.Model.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ResturantOpenningHours.API.Handlers
{
    public class GetAllHoursHandler : IRequestHandler<GetOpenningAndClosingHoursQuerry, OpenningAndClosingHoursResponse>
    {

        public GetAllHoursHandler()
        {
            
        }
        public Task<OpenningAndClosingHoursResponse> Handle(GetOpenningAndClosingHoursQuerry request, CancellationToken cancellationToken)
        {
            var TimeConverter = new TimeConverter();
            return Task.FromResult(TimeConverter.Converter(request.openningAndClosingHoursReqests));
        }
    }
}
