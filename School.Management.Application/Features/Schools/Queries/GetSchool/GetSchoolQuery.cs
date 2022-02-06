﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Management.Application.Features.Schools.Queries.GetSchool
{
    public class GetSchoolQuery : IRequest<GetSchoolQueryResponse>
    {
        public long? SchoolId { get; set; }
    }
}
