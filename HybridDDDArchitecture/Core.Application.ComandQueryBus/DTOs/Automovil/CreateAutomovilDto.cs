using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ComandQueryBus.DTOs.Automovil
{
    public class CreateAutomovilDto
    {
        public string Marca { get; set; } = default!;
        public string Modelo { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Fabricacion { get; set; }
        public string NumeroMotor { get; set; } = default!;
        public string NumeroChasis { get; set; } = default!;
    }
}
