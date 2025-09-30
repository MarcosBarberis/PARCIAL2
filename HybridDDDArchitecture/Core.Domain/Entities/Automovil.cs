using System;

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
            SetMarca(marca);
            SetModelo(modelo);
            SetColor(color);
            SetFabricacion(fabricacion);
            SetNumeroMotor(numeroMotor);
            SetNumeroChasis(numeroChasis);
        }

        // -------- Métodos de dominio (mutaciones controladas) --------

        public void SetMarca(string? marca)
        {
            if (string.IsNullOrWhiteSpace(marca))
                throw new ArgumentNullException(nameof(marca));
            Marca = marca.Trim();
        }

        public void SetModelo(string? modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo))
                throw new ArgumentNullException(nameof(modelo));
            Modelo = modelo.Trim();
        }

        public void SetColor(string? color)
        {
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentNullException(nameof(color));
            Color = color.Trim();
        }

        public void SetFabricacion(int? fabricacion)
        {
            if (!fabricacion.HasValue)
                throw new ArgumentNullException(nameof(fabricacion));
            if (fabricacion < 1900 || fabricacion > 2100)
                throw new ArgumentOutOfRangeException(nameof(fabricacion), "Año de fabricación fuera de rango.");
            Fabricacion = fabricacion.Value;
        }

        public void SetNumeroMotor(string? numeroMotor)
        {
            if (string.IsNullOrWhiteSpace(numeroMotor))
                throw new ArgumentNullException(nameof(numeroMotor));
            NumeroMotor = numeroMotor.Trim();
        }

        public void SetNumeroChasis(string? numeroChasis)
        {
            if (string.IsNullOrWhiteSpace(numeroChasis))
                throw new ArgumentNullException(nameof(numeroChasis));
            NumeroChasis = numeroChasis.Trim();
        }

        /// <summary>
        /// Actualización parcial/total desde DTO (valida internamente).
        /// Solo aplica los campos que vengan con valor (no null / no whitespace).
        /// </summary>
        public void ApplyUpdate(
            string? marca,
            string? modelo,
            string? color,
            int? fabricacion,
            string? numeroMotor,
            string? numeroChasis)
        {
            if (!string.IsNullOrWhiteSpace(marca)) SetMarca(marca);
            if (!string.IsNullOrWhiteSpace(modelo)) SetModelo(modelo);
            if (!string.IsNullOrWhiteSpace(color)) SetColor(color);
            if (fabricacion.HasValue) SetFabricacion(fabricacion);
            if (!string.IsNullOrWhiteSpace(numeroMotor)) SetNumeroMotor(numeroMotor);
            if (!string.IsNullOrWhiteSpace(numeroChasis)) SetNumeroChasis(numeroChasis);
        }
    }
}
