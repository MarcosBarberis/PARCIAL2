using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ComandQueryBus.DTOs.Automovil
{
    public class UpdateAutomovilDto
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Color { get; set; }
        public int? Fabricacion { get; set; }         // YYYY
        public string? NumeroMotor { get; set; }      // único
        public string? NumeroChasis { get; set; }     // único
    }
}
