using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Domain.Shapes
{
    public class Circulo : FormaCircular, ICalculos2D, IFormaContivel
    {
        public Circulo(double raio) : base(raio) { }

        // Garanta que Raio seja public em FormaCircular ou reexponha assim:
        public double Raio => base.Raio;

        public double CalcularArea() => Math.PI * Math.Pow(Raio, 2);
        public double CalcularPerimetro() => 2 * Math.PI * Raio;

        // Contenção
        public bool Contem(IFormaContivel formaInterna) => formaInterna switch
        {
            Circulo c => c.Raio <= this.Raio, // círculo em círculo: raio interno <= raio externo
            Retangulo r => Math.Sqrt(r.Largura * r.Largura + r.Altura * r.Altura) <= 2 * this.Raio, // diagonal <= diâmetro
            _ => throw new NotSupportedException("Forma interna não suportada para contenção em círculo.")
        };
    }
}
