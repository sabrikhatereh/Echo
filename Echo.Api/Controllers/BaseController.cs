﻿using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Asp.Versioning;

namespace Echo.Api.Controllers
{
    [Route(BaseApiPath)]
    [ApiController]
    [ApiVersion("1.0")]
    public abstract class BaseController : ControllerBase
    {
        protected const string BaseApiPath = "api/v{version:apiVersion}";
        private IMapper _mapper;

        private IMediator _mediator;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }

}

