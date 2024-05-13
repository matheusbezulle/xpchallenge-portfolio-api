# API de Portfólio
Este projeto é uma API .NET que permite cadastrar administradores no sistema, gerenciar portfólios de investimentos personalizados, e que notifica os administradores sobre produtos financeiros dos portfólios com vencimentos próximos.

## Como executar a aplicação

1. **Pré-requisitos**: 
Certifique-se de ter o **.NET 8** e o **Docker** instalados em sua máquina.

2. **Clone o repositório**: 
Use o comando `git clone https://github.com/matheusbezulle/xpchallenge-portfolio-api.git` para clonar o repositório.

3. **Navegue até o diretório do projeto**: 
Use o comando `cd xpchallenge-portfolio-api`.

4. **Construa o projeto**: 
Execute o docker-compose na raiz do projeto com o comando `docker-compose-build` para buildar o projeto.

5. **Execute o projeto**: 
Use o comando `docker-compose up` para executar o microsserviço e seu respectivo banco de dados mongodb.

5. **Acesse o swagger**: 
A aplicação sobe por padrão na porta 8001. Para acessa-la digite no seu navegador `localhost:8001/swagger`

6. **Acesse o mongodb**:
Caso queira acessar o mongodb, ele sobe por padrão na porta 27018. É possível se conectar através da connstring `mongodb://localhost:27018/db_portfolio?replicaSet=rs0`

## Como utilizar a aplicação

### Hangfire
A rotina de envio de emails diários de produtos próximos ao vencimento é realizada utilizando o Hangfire, com base nos administradores cadastrados. É possível acessar o dashboard do hangfire para consultar a execução da tarefa de enviar emails, através do endpoint `localhost:8001/hangfire`

### Documentação
É possível consultar a documentação da API e realizar chamadas através do swagger. Segue a documentação mais detalhada dos objetos de request nos diversos endpoints:

#### 🟢 POST /Administrador

Método responsável por cadastrar um administrador.

**Parâmetros do corpo da solicitação**:

- `Email` (string): Endereço de email do administrador. Este campo é obrigatório e deve ser um endereço de email válido.


#### 🔴 DELETE /Administrador/{email}

Método responsável por excluir um administrador.

**Parâmetros da URL**:

- `email` (string): Endereço de email do administrador. Este campo é obrigatório.

---

#### 🔵 GET /Portfolio/{idPortfolio}

Método responsável por obter os dados de determinado portfólio.

**Parâmetros da URL**:

- `idPortfolio` (string): Identificador único do portfólio. Este campo é obrigatório.


#### 🟢 POST /Portfolio

Método responsável por cadastrar um novo portfólio.

**Parâmetros do corpo da solicitação**:

- `Nome` (string): Nome do portfólio. Este campo é obrigatório.
- `IdPerfil` (int): Identificador do perfil do portfólio. Este campo é obrigatório e deve ser preenchido com valores de 1-Conservador, 2-Moderado e 3-Agressivo.


#### 🔴 DELETE /Portfolio/{idPortfolio}

Método responsável por excluir um portfólio.

**Parâmetros da URL**:

- `idPortfolio` (string): Identificador único do portfólio. Este campo é obrigatório.

---

#### 🟢 POST /ProdutoFinanceiro

Método responsável por incluir novos produtos financeiros em um determinado portfólio.

**Parâmetros do corpo da solicitação**:

- `IdPortfolio` (string): Identificador único do portfólio. Este campo é obrigatório.
- `Nome` (string): Nome do produto financeiro. Este campo é obrigatório.
- `IdCategoria` (int): Identificador da categoria do produto financeiro. Este campo é obrigatório e deve ser preenchido com valores de 1 a 6.
- `Peso` (int): Peso do produto financeiro*. Este campo é obrigatório e deve ser maior que zero.
- `DataVencimento` (DateTime): Data de vencimento do produto financeiro. Este campo é obrigatório e deve ser preenchido no formato dd/MM/yyyy.

*O peso do produto financeiro significa proporcionalmente o quanto da carteira esse produto representa. O peso definido será sempre proporcinal aos pesos definidos em todos os produtos de um determinado portfólio. Por exemplo: um portfólio com 3 produtos, com pesos 4, 6 e 10. Respectivamente, os pesos simbolizam 20%, 30% e 50% da composição da carteira.

#### 🟡 PUT /ProdutoFinanceiro/Peso

Método responsável por alterar o peso de um produto financeiro em determinado portfólio.

**Parâmetros do corpo da solicitação**:

- `IdPortfolio` (string): Identificador único do portfólio. Este campo é obrigatório.
- `Nome` (string): Nome do produto financeiro. Este campo é obrigatório.
- `Peso` (int): Novo peso do produto financeiro. Este campo é obrigatório.


#### 🔴 DELETE /ProdutoFinanceiro

Método responsável por remover um produto financeiro de determinado portfólio.

**Parâmetros do corpo da solicitação**:

- `IdPortfolio` (string): Identificador único do portfólio. Este campo é obrigatório.
- `Nome` (string): Nome do produto financeiro. Este campo é obrigatório.


Para mais detalhes, consulte a documentação da API.

## Evoluções

- Autenticação: garantir a segurança no consumo da aplicação.
- Ambientação: criar ambientes de dev, hlg e prd.
- Esteira CI/CD: realizar o deploy contínuo em uma esteira automatizada.
- Cloud: algum gerenciador de containers, como AKS.
- Observabilidade: aprimorar logs, métricas e traces.
- Monitoria: ferramentas para monitorar aplicação, como Grafana.

## Contribuição

Matheus Bezulle dos Anjos, (11) 97067-4857
