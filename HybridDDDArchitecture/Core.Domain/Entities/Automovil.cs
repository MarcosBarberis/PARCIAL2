using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Automovil
    {
        public int Id { get; private set; }
        public string Marca { get; private set; } = default!;
        public string Modelo { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public int Fabricacion { get; private set; }
        public string NumeroMotor { get; private set; } = default!;
        public string NumeroChasis { get; private set; } = default!;

        private Automovil() { } // EF

        public Automovil(string marca, string modelo, string color, int fabricacion,
                         string numeroMotor, string numeroChasis)
        {
            Marca = string.IsNullOrWhiteSpace(marca) ? throw new ArgumentNullException(nameof(marca)) : marca.Trim();
            Modelo = string.IsNullOrWhiteSpace(modelo) ? throw new ArgumentNullException(nameof(modelo)) : modelo.Trim();
            Color = string.IsNullOrWhiteSpace(color) ? throw new ArgumentNullException(nameof(color)) : color.Trim();
            Fabricacion = fabricacion;
            NumeroMotor = string.IsNullOrWhiteSpace(numeroMotor) ? throw new ArgumentNullException(nameof(numeroMotor)) : numeroMotor.Trim();
            NumeroChasis = string.IsNullOrWhiteSpace(numeroChasis) ? throw new ArgumentNullException(nameof(numeroChasis)) : numeroChasis.Trim();
        }
    }
}
