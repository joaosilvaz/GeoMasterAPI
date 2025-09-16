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

🔸 Exemplo de Resposta (201 Created)
```json {
  "id": 1,
  "nome": "João Vitor",
  "email": "joao@mottu.com",
  "senha": "senhaSegura123"
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
