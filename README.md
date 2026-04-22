# 🍔 Good Hamburger API - STgenetics Challenge

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp)
![xUnit](https://img.shields.io/badge/xUnit-Testing-success?style=for-the-badge)
![Moq](https://img.shields.io/badge/Moq-Mocking-4D88FF?style=for-the-badge)

## 📖 Sobre o Projeto
Esta é uma API RESTful desenvolvida em C# e .NET para gerenciar o sistema de pedidos da lanchonete "Good Hamburger". O projeto foi construído como desafio técnico para a STgenetics, focando em entregar um código limpo, testável e com regras de negócio sólidas.

---

## 🏗️ Decisões de Arquitetura

Para demonstrar boas práticas de desenvolviemento de software e foco na manutenibilidade, as seguintes decisões técnicas foram adotadas:

* **Clean Architecture e Domínio Rico (DDD):** A entidade `Order` encapsula toda a sua própria regra de negócio. O cálculo de totais, a aplicação de descontos e as travas de segurança (como impedir dois sanduíches no mesmo pedido) vivem na entidade, não nos *Controllers*. A lista de itens é protegida (`SetPropertyAccessMode`) para evitar manipulações externas indevidas.
* **Padrão Repository:** O acesso aos dados foi abstraído em interfaces (`IOrderRepository`, `IProductRepository`). Isso garante que a aplicação não dependa diretamente do Entity Framework, permitindo a troca fácil do banco de dados no futuro.
* **DTOs (Data Transfer Objects):** Utilização do `OrderRequestDto` para isolar a camada de apresentação do domínio. O usuário envia apenas os dados estritamente necessários (IDs), protegendo o sistema contra *Over-Posting*.
* **Banco de Dados In-Memory:** Optei pelo *Entity Framework Core In-Memory*. Permitindo que o projeto seja baixado e executado instantaneamente, sem a necessidade de rodar scripts SQL ou instalar bancos de dados locais. Caso o sistema fosse para produção, bastaria alterar a injeção no `Program.cs` para um banco relacional (ex: SQL Server) sem impacto no resto do código.

---

## 🌐 Endpoints da API

A documentação visual e interativa dos endpoints está disponível via **Swagger** ao rodar o projeto. Abaixo está o resumo das rotas disponíveis:

### Cardápio (Menu)
* `GET /api/menu`
  * Retorna a lista completa dos produtos disponíveis no cardápio (Adicionados automaticamente no banco).

### Pedidos (Order)
* `POST /api/order`
  * Cria um novo pedido. Realiza o cálculo automático de descontos promocionais (ex: 20% no Combo Completo).
  * **Body esperado:** `{ "productIds": ["guid-1", "guid-2", "guid-3"] }`
* `GET /api/order`
  * Retorna todos os pedidos registrados no sistema com seus respectivos itens.
* `GET /api/order/{id}`
  * Retorna os detalhes de um pedido específico.
* `PUT /api/order/{id}`
  * Atualiza um pedido existente (substituindo a lista de lanches e recalculando os valores).
* `DELETE /api/order/{id}`
  * Remove um pedido do sistema.

---

## 🚀 Instruções de Execução

### Pré-requisitos
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download) (ou superior) instalado na máquina.

### Como rodar a API
1. Clone este repositório para a sua máquina:
   ```bash
   git clone https://github.com/DiogoFernandess/Projeto-Pratico-STgenetics
   ``
---
## 🧪 Estratégia de Testes Automatizados

A confiabilidade do sistema e a integridade das regras de negócio são garantidas por testes de unidade focados e eficientes. A estratégia adotada segue o princípio de testar comportamentos, dividindo a cobertura em duas frentes estruturais:

### 🛠️ Tecnologias Utilizadas
* **xUnit:** Framework principal para a estruturação, execução e asserção dos testes.
* **Moq:** Biblioteca de *Mocking* utilizada para isolar dependências externas e simular integrações durante os testes da camada de API.

#### 1. Testes de Domínio (Isolamento Puro)
Graças à utilização da **Clean Architecture**, as regras de negócio cruciais do sistema vivem na camada de Domínio, totalmente isoladas de frameworks, internet ou banco de dados.
* **Abordagem:** Testes de estado puro. Instanciando as entidades reais (`Order`, `Product`) e serviços de domínio (`DiscountService`) diretamente na memória.
* **Vantagem:** São testes ultrarrápidos e à prova de falhas de infraestrutura. Não utilizam *Mocks*, testando a matemática e a lógica orientada a objetos de forma direta.
* **Cenários Cobertos:** 
  * Aplicação matemática exata de todos os descontos promocionais de combos.
  * Validação dos valores de Subtotal, Desconto e Total.

#### 2. Testes de API e Controllers (Mocking)
A camada de apresentação (`OrdersController`) tem o papel de orquestrar a requisição do usuário, acionar o domínio e persistir os dados. Para testá-la sem afetar a infraestrutura, utilizamos "Dublês ou Fakes".
* **Abordagem:** Testes de interação e fluxo. Utilizando a biblioteca **Moq**, criamos versões falsas das interfaces de acesso a dados (`IOrderRepository`, `IProductRepository`).
* **Vantagem:** Garantir que a API responda corretamente sem nunca utilizar o Entity Framework Core ou um banco de dados real.
* **Cenários Cobertos:**
  * **Caminho Feliz:** Garantir que um pedido válido seja acionado ao repositório (`Times.Once`) e retorne status `201 Created`.
  * **Caminho Triste:** Garantir que requisições que quebrem as regras de negócio sejam interceptadas pelo *Controller*, bloqueando a ida ao banco de dados (`Times.Never`) e retornando um erro amigável com status `400 Bad Request`.
---
### 🚀 Como rodar os testes
Para executar os testes completos, validar os cenários e visualizar o relatório de sucesso, vá até a raiz do projeto e execute o comando:

```bash
 dotnet test
```
---
### 📩 O que ficou de fora?
* **Frontend em Blazor**: Frontend que consuma o Backend da API feito em Blazor ( Framework do C# ) 
