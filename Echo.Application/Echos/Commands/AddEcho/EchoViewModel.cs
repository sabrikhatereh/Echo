using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FluentValidation;
using MediatR;

namespace Echo.Application.Echos.Commands.AddEcho
{
    public class EchoViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; private set; }

        public DateTime EchoDate { get; private set; }
    }
}
