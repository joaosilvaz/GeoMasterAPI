# 🛵 Challenge - API de Usuários para Aluguel de Motos Mottu

Este projeto foi desenvolvido como parte do Challenge da FIAP em parceria com a empresa **Mottu**, para a disciplina de **Desenvolvimento Web com ASP.NET Core**.  
O objetivo é construir uma **API RESTful** para gerenciamento de usuários, permitindo o cadastro, consulta, atualização e exclusão de perfis. Esses usuários utilizarão o sistema para realizar o aluguel de motos.

---

## 📚 Tecnologias Utilizadas

- ASP.NET Core Web API  
- C#  
- Visual Studio  
- Swagger (para documentação e testes)  
- Postman (para testes de API)

---

## Estrutura do Projeto

GeoMaster.sln
└─ src/
   ├─ GeoMaster.Api/               # Interface HTTP (Controllers, Program/Swagger)
   │  └─ Controllers/
   │     ├─ CalculosController.cs
   │     ├─ ValidacoesController.cs
   │     └─ BaseCalculosController.cs
   │  └─ Program.cs
   │
   ├─ GeoMaster.Application/       # Casos de uso / Serviços e Fábricas (DI)
   │  └─ Abstractions/
   │     ├─ ICalculadoraService.cs
   │     └─ IValidacoesService.cs
   │  └─ Services/
   │     ├─ CalculadoraService.cs
   │     └─ ValidacoesService.cs
   │  └─ Factory/
   │        ├─ IFormaFactory.cs
   │        ├─ FormaFactory.cs
   │        ├─ IFormaContivelFactory.cs
   │        └─ FormaContivelFactory.cs
   │
   └─ GeoMaster.Domain/            # Regras de negócio (Formas, DTOs, Abstrações)
      └─ Abstractions/
      │     ├─ ICalculos2D.cs
      │     ├─ ICalculos3D.cs
      │     ├─ IFormaContivel.cs   // exige bool Contem(IFormaContivel)
      │     ├─ FormaCircular.cs    // base p/ raio
      │     └─ FormaRetangular.cs  // base p/ largura/altura
      └─ Shapes/
      │     ├─ Circulo.cs          // ICalculos2D, IFormaContivel
      │     ├─ Retangulo.cs        // ICalculos2D, IFormaContivel
      │     └─ Esfera.cs           // ICalculos3D
      └─ Dtos/
            ├─ FormaRequestDto.cs          // { tipoForma, propriedades }
            ├─ ResultadoCalculoDto.cs      // From(tipo, resultado, TipoOperacao)
            ├─ FormaSimplesDto.cs          // usado em validações
            └─ FormaContidaRequestDto.cs   // { formaExterna, formaInterna }

---

## 🔗 Rotas da API

### 📌 Usuários (CRUD)

| Método | Endpoint                           | Descrição                        |
|--------|------------------------------------|----------------------------------|
| POST    | `/api/v1/calculos/area`   | Busca um usuário pelo e-mail     |
| POST   | `/api/v1/calculos/perimetro`                        | Cadastra um novo usuário         |
| POST    | `/api/v1/calculos/volume`                   | Atualiza os dados de um usuário  |
| POST | `/api/v1/calculos/area-superficial`                   | Remove um usuário                |
| POST | `/api/v1/validacoes/forma-contida`                   | Remove um usuário                |
---

## 📥 Exemplo de Requisição

### 🔸 POST `/usuarios`

### 🔸 Exemplo de Requisição (POST /usuarios)

```json {
  "nome": "João Vitor",
  "email": "joao@mottu.com",
  "senha": "senhaSegura123"
````

11s
📚 Rotas da GeoMaster API — Requests & Responses

Base URL: http://localhost:{porta}
Versão: /api/v1
Content-Type: application/json
Auth: não requer

🔢 Cálculos — /api/v1/calculos
📌 Tabela rápida
Método	Rota	Suporta	Retorno 200 (payload)
POST	/calculos/area	circulo, retangulo	{ tipoForma, resultado, operacao, dataCalculo }
POST	/calculos/perimetro	circulo, retangulo	{ tipoForma, resultado, operacao, dataCalculo }
POST	/calculos/volume	esfera	{ tipoForma, resultado, operacao, dataCalculo }
POST	/calculos/area-superficial	esfera	{ tipoForma, resultado, operacao, dataCalculo }

Nota: operacao é um enum.

Com JsonStringEnumConverter habilitado, retorna texto ("area", "perimetro", "volume", "areaSuperficial").

Sem o converter, retorna número (0, 1, 2, 3).

1) POST /api/v1/calculos/area

Request — exemplos válidos

{ "tipoForma": "circulo",  "propriedades": { "raio": 10 } }

{ "tipoForma": "retangulo", "propriedades": { "largura": 5, "altura": 8 } }


Response — 200 OK (exemplo)

{
  "tipoForma": "retangulo",
  "resultado": 40.0,
  "operacao": "area",
  "dataCalculo": "2025-09-15T23:59:59Z"
}


Erros — exemplos

// 400 BadRequest (dados inválidos)
{
  "title": "Dados de entrada inválidos",
  "detail": "Propriedade 'raio' é obrigatória e deve ser > 0.",
  "status": 400
}

// 422 UnprocessableEntity (operação não suportada p/ a forma)
{
  "title": "Operação não suportada",
  "detail": "Perímetro só é válido para formas 2D. Tipos suportados: circulo, retangulo.",
  "status": 422
}

// 500 Internal Server Error
{
  "title": "Erro interno do servidor",
  "detail": "Ocorreu um erro inesperado ao processar a solicitação",
  "status": 500
}

2) POST /api/v1/calculos/perimetro

Request — exemplos válidos

{ "tipoForma": "circulo",  "propriedades": { "raio": 4 } }

{ "tipoForma": "retangulo", "propriedades": { "largura": 8, "altura": 12 } }


Response — 200 OK (exemplos)

{
  "tipoForma": "retangulo",
  "resultado": 40.0,
  "operacao": "perimetro",
  "dataCalculo": "2025-09-15T23:59:59Z"
}


(círculo com raio 4 retorna 25.1327)

Erros: mesmos formatos de /calculos/area (ver acima).

3) POST /api/v1/calculos/volume

Request — válido

{ "tipoForma": "esfera", "propriedades": { "raio": 8 } }


Response — 200 OK (exemplo)

{
  "tipoForma": "esfera",
  "resultado": 2144.6606,
  "operacao": "volume",
  "dataCalculo": "2025-09-15T23:59:59Z"
}


Erros

// 422 UnprocessableEntity (volume em forma 2D)
{
  "title": "Operação não suportada",
  "detail": "Volume só é válido para formas 3D. Tipos suportados: esfera.",
  "status": 422
}


Outros erros seguem o padrão já mostrado.

4) POST /api/v1/calculos/area-superficial

Request — válido

{ "tipoForma": "esfera", "propriedades": { "raio": 8 } }


Response — 200 OK (exemplo)

{
  "tipoForma": "esfera",
  "resultado": 804.2477,
  "operacao": "areaSuperficial",
  "dataCalculo": "2025-09-15T23:59:59Z"
}


Erros

// 422 UnprocessableEntity (área superficial de forma 2D)
{
  "title": "Operação não suportada",
  "detail": "Área superficial só é válida para formas 3D. Tipos suportados: esfera.",
  "status": 422
}


Outros erros seguem o padrão já mostrado.

✅ Validações — /api/v1/validacoes
POST /api/v1/validacoes/forma-contida

Verifica se a formaInterna cabe dentro da formaExterna.

Request — schema

{
  "formaExterna": {
    "tipoForma": "circulo | retangulo",
    "propriedades": { "raio": n } | { "largura": n, "altura": n }
  },
  "formaInterna": {
    "tipoForma": "circulo | retangulo",
    "propriedades": { "raio": n } | { "largura": n, "altura": n }
  }
}


Request — exemplos

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


Response — 200 OK

{ "resultado": true }


ou

{ "resultado": false }


Erros — exemplos

// 400 BadRequest (payload inválido / valores <= 0 / tipo não suportado / par não suportado)
{
  "title": "Dados inválidos",
  "detail": "Para 'formaInterna' do tipo 'circulo', informe 'raio' > 0.",
  "status": 400
}

// 500 Internal Server Error (inesperado)
{
  "title": "Erro interno do servidor",
  "detail": "Ocorreu um erro inesperado ao processar a validação",
  "status": 500
}

🧪 cURL — exemplos rápidos

Área (retângulo)

curl -X POST "http://localhost:5010/api/v1/calculos/area" \
  -H "Content-Type: application/json" \
  -d '{"tipoForma":"retangulo","propriedades":{"largura":8,"altura":12}}'


Perímetro (círculo)

curl -X POST "http://localhost:5010/api/v1/calculos/perimetro" \
  -H "Content-Type: application/json" \
  -d '{"tipoForma":"circulo","propriedades":{"raio":4}}'


Validação (círculo em retângulo → true)

curl -X POST "http://localhost:5010/api/v1/validacoes/forma-contida" \
  -H "Content-Type: application/json" \
  -d '{"formaExterna":{"tipoForma":"retangulo","propriedades":{"largura":10,"altura":10}},"formaInterna


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
