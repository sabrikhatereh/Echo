using AutoMapper;
using Echo.Application.Echos.Commands.AddEcho;
using Echo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Echo.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Automatically maps properties with the same name
            CreateMap<CreateEchoRequestDto, CreateEchoCommand>();
            CreateMap<EchoEntity, EchoViewModel>();
        }
    }
}
