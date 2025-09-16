#  CheckPoint 4 - GeoMasterAPI
---

## 📚 Tecnologias Utilizadas

- ASP.NET Core Web API  
- C#  
- Visual Studio  
- Swagger (para documentação e testes)  
- Postman (para testes de API)

---

## Estrutura do Projeto

## Estrutura do Projeto (ASCII)

GeoMaster.sln
src/
  GeoMaster.Api/
    Controllers/
      CalculosController.cs
      ValidacoesController.cs
      BaseCalculosController.cs
    Program.cs

  GeoMaster.Application/
    Abstractions/
      ICalculadoraService.cs
      IValidacoesService.cs
    Services/
      CalculadoraService.cs
      ValidacoesService.cs
    Factory/
      IFormaFactory.cs
      FormaFactory.cs
      IFormaContivelFactory.cs
      FormaContivelFactory.cs

  GeoMaster.Domain/
    Abstractions/
      ICalculos2D.cs
      ICalculos3D.cs
      IFormaContivel.cs
      FormaCircular.cs
      FormaRetangular.cs
    Shapes/
      Circulo.cs
      Retangulo.cs
      Esfera.cs
    Dtos/
      FormaRequestDto.cs
      ResultadoCalculoDto.cs
      FormaSimplesDto.cs
      FormaContidaRequestDto.cs

---

## 🔗 Rotas da API

### 📌 (CRUD)

| Método | Endpoint                           | Descrição                        |
|--------|------------------------------------|----------------------------------|
| POST    | `/api/v1/calculos/area`   | Post para calcular area    |
| POST    | `/api/v1/calculos/perimetro`   | Post para calcular perimetro    |
| POST    |  `/api/v1/calculos/volume`   | Post para calcular volume    |
| POST    | `/api/v1/calculos/area-superficial`   | Post para calcular area superficial    |
| POST    | `/api/v1/validacoes/forma-contida`   | Post para calcular forma contida    |

---

## 📥 Exemplo de Requisição

### 🔸 POST `/api/v1/calculos/area`

### 🔸 Exemplo de Requisição (POST /api/v1/calculos/area)

```json {
{ "tipoForma": "circulo",  "propriedades": { "raio": 10 } }
````

```json {
{
  "tipoForma": "retangulo",
  "resultado": 40.0,
  "operacao": "area",
  "dataCalculo": "2025-09-15T23:59:59Z"
}
````

### 🔸 POST `/api/v1/calculos/perimetro`

### 🔸 Exemplo de Requisição (POST /api/v1/calculos/perimetro)

```json {
{ "tipoForma": "retangulo", "propriedades": { "largura": 8, "altura": 12 } }
````

```json {
{
  "tipoForma": "retangulo",
  "resultado": 40.0,
  "operacao": "perimetro",
  "dataCalculo": "2025-09-15T23:59:59Z"
}
````

### 🔸 POST `/api/v1/calculos/volume`

### 🔸 Exemplo de Requisição (POST /api/v1/calculos/volume)

```json {
{ "tipoForma": "esfera", "propriedades": { "raio": 8 } }
````

```json {
{
  "tipoForma": "esfera",
  "resultado": 2144.6606,
  "operacao": "volume",
  "dataCalculo": "2025-09-15T23:59:59Z"
}
````

### 🔸 POST `/api/v1/calculos/area-superficial`

### 🔸 Exemplo de Requisição (POST /api/v1/calculos/area-superficial)

```json {
{ "tipoForma": "esfera", "propriedades": { "raio": 8 } }
````

```json {
{
  "tipoForma": "esfera",
  "resultado": 804.2477,
  "operacao": "areaSuperficial",
  "dataCalculo": "2025-09-15T23:59:59Z"
}
````

### 🔸 POST `/api/v1/validacoes/forma-contida`

### 🔸 Exemplo de Requisição (POST /api/v1/validacoes/forma-contida)

```json {
// TRUE — círculo (r=5) em retângulo (10x10)
{
  "formaExterna": { "tipoForma": "retangulo", "propriedades": { "largura": 10, "altura": 10 } },
  "formaInterna": { "tipoForma": "circulo",   "propriedades": { "raio": 5 } }
}

// FALSE — retângulo (9x9) em círculo (r=6)
{
  "formaExterna": { "tipoForma": "circulo",   "propriedades": { "raio": 6 } },
  "formaInterna": { "tipoForma": "retangulo", "propriedades": { "largura": 9, "altura": 9 } }
}
````

```json {
{ "resultado": true }
ou
{ "resultado": false }
````



## 🚀 Instalação e Execução
Clone o repositório:
git clone https://github.com/seu-usuario/seu-projeto.git

Abra o projeto no Visual Studio.

Execute a aplicação (pressionando F5) ou via terminal:

dotnet run

Acesse a documentação Swagger para testar os endpoints:

http://localhost:{porta}/swagger

## 👨‍💻 Autor
João Vitor da Silva Nascimento 
FIAP 
