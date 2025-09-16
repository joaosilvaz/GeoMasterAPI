using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Domain.Shapes
{
    public class Retangulo : FormaRetangular, ICalculos2D, IFormaContivel
    {
        public Retangulo(double largura, double altura) : base(largura, altura) { }

        // Garanta que Largura/Altura sejam public em FormaRetangular ou reexponha:
        public double Largura => base.Largura;
        public double Altura => base.Altura;

        public double CalcularArea() => Largura * Altura;
        public double CalcularPerimetro() => 2 * (Largura + Altura);

        // Contenção
        public bool Contem(IFormaContivel formaInterna) => formaInterna switch
        {
            Circulo c => 2 * c.Raio <= Math.Min(this.Largura, this.Altura), // diâmetro <= menor lado
            Retangulo r => (r.Largura <= this.Largura && r.Altura <= this.Altura) // sem rotação
                           || (r.Largura <= this.Altura && r.Altura <= this.Largura), // com rotação 90°
            _ => throw new NotSupportedException("Forma interna não suportada para contenção em retângulo.")
        };
    }
}
