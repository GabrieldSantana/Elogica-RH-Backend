# Backend do Sistema Elógica RH

## Introdução
Este projeto é o backend de um sistema de gerenciamento de Recursos Humanos (RH), projetado para lidar com a gestão de funcionários, atribuição de cargos e setores, agendamento de férias e controle de horários de trabalho. Ele fornece uma API RESTful para gerenciar operações relacionadas ao RH, com regras de validação para garantir a integridade dos dados e conformidade com as políticas da empresa.

O backend implementa paginação para todos os endpoints de listagem, inclui validações para entidades como funcionários, cargos e férias, e se integra a um banco de dados SQL Server para armazenamento persistente.

## Objetivo
O backend é o núcleo do sistema de RH, permitindo:
- Gerenciamento de funcionários (operações CRUD, dados pessoais, cargos e salários).
- Controle de cargos e setores com atribuição de funcionários.
- Gestão de férias com detecção de conflitos de datas.
- Gerenciamento de horários com regras estritas de tempo.

## Tecnologias
- **.NET Core**: Framework para construção da API REST.
- **C#**: Linguagem de programação principal.
- **Dapper**: Micro-ORM leve para consultas SQL otimizadas.
- **AutoMapper**: Biblioteca para mapeamento entre objetos (ex.: DTOs e entidades).
- **SQL Server**: Banco de dados para armazenamento de dados de RH.
- **Swagger**: Documentação e testes da API.

## Instalação
1. **Clonar o Repositório**:
   ```bash
   git clone https://github.com/GabrieldSantana/Elogica-RH-Backend.git
   cd Elogica-RH-Backend
   ```

2. **Instalar Dependências**:
   Certifique-se de ter o SDK do .NET instalado. Em seguida, restaure as dependências:
   ```bash
   dotnet restore
   ```

3. **Configurar o Banco de Dados**:
   - Configure uma instância do SQL Server (local ou remota).
   - Atualize a string de conexão no arquivo `appsettings.json` (veja a seção "Configurando o appsettings.json" abaixo).
   - Execute as migrações para configurar o banco de dados:
     ```bash
     dotnet ef database update
     ```

4. **Executar a Aplicação**:
   ```bash
   dotnet run --project Src/Application/Application.csproj
   ```
   A API estará disponível em `https://localhost:5001` (ou na porta especificada no `appsettings.json`).

## Configurando o appsettings.json
O arquivo `appsettings.json` contém configurações, incluindo a string de conexão com o SQL Server. Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HRSystemDB;User Id=sa;Password=SuaSenhaForte;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

- Substitua `Server`, `Database`, `User Id` e `Password` pelas credenciais do seu SQL Server.
- Certifique-se de que `TrustServerCertificate=True` se estiver usando um certificado local ou autoassinado.

## Como Usar
1. **Acessar a API**:
   - Use uma ferramenta como Postman ou cURL para interagir com a API.
   - A documentação Swagger está disponível em `/swagger` quando a aplicação estiver rodando (por exemplo, `https://localhost:5001/swagger`).

2. **Exemplo de Requisição**:
   Para obter uma lista paginada de funcionários:
   ```bash
   curl -X GET "https://localhost:5001/api/Funcionarios?page=1&pageSize=10" -H "accept: application/json"
   ```
   Resposta:
   ```json
   {
     "data": [
       {
         "id": 1,
         "nome": "Ana Clara Souza",
         "cpf": "12345678901",
         "dataNascimento": "1990-03-22",
         "dataContratacao": "2015-04-10",
         "ativo": true,
         "cargosId": 2
       }
     ],
     "total": 1,
     "page": 1,
     "pageSize": 10
   }
   ```

## Banco de Dados
O backend utiliza um banco de dados SQL Server com o seguinte esquema (conforme mostrado no diagrama fornecido):

- **Funcionarios**: Armazena dados de funcionários (ID, Nome, CPF, DataNascimento, Email, Telefone, Endereco, DataContratacao, Salario, Ativo, CargosId).
- **Cargos**: Armazena dados de cargos (ID, Titulo, Descricao, SalarioBase).
- **Setores**: Armazena dados de setores (ID, Nome, Descricao).
- **Ferias**: Armazena dados de férias (ID, DataInicio, DataFim, FuncionariosId).
- **Horarios**: Armazena dados de horários (ID, HorarioInicio, HorarioFim, IntervaloInicio, IntervaloFim).
- **Menu**: Armazena itens de menu para navegação (ID, Titulo, Descricao, Url, Icone, Ordem, MenuPaiId).

### Regras de Validação
- **Funcionarios**:
  - `DataNascimento`: Deve ter pelo menos 16 anos.
  - `DataContratacao`: Não pode ser uma data futura se `Ativo` for verdadeiro.
- **Cargos**:
  - `SalarioBase`: Deve ser no mínimo R$ 2.000,00.
- **Setores**:
  - `Nome`: Não pode conter números.
- **Ferias**:
  - Funcionários podem ter de 1 a 3 períodos de férias por ano, totalizando 30 dias.
  - Férias devem ser agendadas com pelo menos 1 mês de antecedência.
  - Funcionários só podem tirar férias 1 ano após a data de contratação.
- **Horarios**:
  - Horários de trabalho são das 08:00 às 20:00, com intervalo de 1 a 2 horas entre 12:00 e 14:00.
- **Menu**:
  - Itens de menu pai exigem um `Icone`.
  - Itens de menu filho exigem `Url` e `MenuPaiId`.
  - `Ordem` deve ser único dentro do mesmo grupo de menu.

## Autenticação
Atualmente, o backend não implementa autenticação. Todos os endpoints são acessíveis publicamente. Para uso em produção, considere adicionar autenticação baseada em JWT ou integrar com um provedor de identidade.

## Rotas Disponíveis
| Método | Caminho                       | Descrição                          |
|--------|-------------------------------|------------------------------------|
| GET    | `/funcionarios`               | Obtém funcionários. |
| GET    | `/funcionarios/{id}`          | Obtém funcionário baseado no id. |
| GET    | `/funcionarios/{pagina}/{quantidade}`          | Obtém funcionários paginados. |
| POST   | `/funcionarios`           | Cria um novo funcionário.          |
| PUT    | `/funcionarios/{id}`      | Atualiza um funcionário.           |
| PUT    | `/funcionarios/desativa/{id}`      | Desativar um funcionário.             |
| GET    | `/cargos`               | Obtém cargos. |
| GET    | `/cargos/{id}`          | Obtém cargo baseado no id. |
| GET    | `/cargos/{pagina}/{quantidade}`          | Obtém cargos paginados. |
| POST   | `/cargos`           | Cria um novo cargo.          |
| PUT    | `/cargos/{id}`      | Atualiza um cargo.           |
| DELETE | `/cargos/{id}`      | Deleta um cargo.             |
| GET    | `/setores`               | Obtém setores. |
| GET    | `/setores/{id}`          | Obtém setor baseado no id. |
| GET    | `/setores/{pagina}/{quantidade}`          | Obtém setores paginados. |
| POST   | `/setores`           | Cria um novo setor.          |
| PUT    | `/setores/{id}`      | Atualiza um setor.           |
| DELETE | `/setores/{id}`      | Deleta um setor.             |
| GET    | `/cargossetores`               | Obtém cargos setores. |
| GET    | `/cargossetores/{id}`          | Obtém cargo setor baseado no id. |
| GET    | `/cargossetores/{pagina}/{quantidade}`          | Obtém cargos setores paginados. |
| POST   | `/cargossetores`           | Cria um novo cargo setor.          |
| PUT    | `/cargossetores/{id}`      | Atualiza um cargo setor.           |
| DELETE | `/cargossetores/{id}`      | Deleta um cargo setor.             |
| GET    | `/ferias`               | Obtém os ferias de trabalho dos funcionários. |
| GET    | `/ferias/{id}`          | Obtém ferias de trabalho dos funcionários baseado no id. |
| GET    | `/ferias/{pagina}/{quantidade}`          | Obtém ferias de trabalho dos funcionários baseado no id. |
| POST   | `/ferias`           | Cria um novo ferias.          |
| PUT    | `/horario/{id}`      | Atualiza um ferias.           |
| DELETE | `/horario/{id}`      | Deleta um ferias.             |
| GET    | `/horarios`               | Obtém os horários de trabalho dos funcionários. |
| GET    | `/horarios/{id}`          | Obtém horário de trabalho dos funcionários baseado no id. |
| GET    | `/horarios/{pagina}/{quantidade}`          | Obtém horário de trabalho dos funcionários baseado no id. |
| POST   | `/horarios`           | Cria um novo horário.          |
| PUT    | `/horario/{id}`      | Atualiza um horário.           |
| DELETE | `/horario/{id}`      | Deleta um horário.             |

## Padrão de Nomenclatura de Métodos

- **Métodos de Controladores, Serviços e Repositórios** (Exemplos):

- `BuscarFuncionarioAsync`,
- `BuscarFuncionarioPorIdAsync`,
- `BuscarFuncionarioPaginadoAsync`,
- `AdicionarFuncionarioAsync`,
- `AtualizarFuncionarioAsync`,

- `ExcluirFuncionarioAsync`


## Padrão de Commits
Os commits seguem a especificação **Conventional Commits**:
- Formato: `<tipo>: <descrição>`
- Exemplo: `feat: adicionar método paginação de funcionários`
- Tipos: `feat`, `fix`, `docs`, `refactor`, `test`, `chore`.

## Colaboradores
Agradecemos aos seguintes colaboradores pelo seu empenho e trabalho neste projeto:
- [Clara Oliveira](https://github.com/mclaraoliveira).
- [Conrado Capistrano](https://github.com/ConradoCapistrano).
- [Davisson Falcão](https://github.com/DavissonJr).
- [Elton Luiz](https://github.com/eltonluiz178).
- [Gabriel de Santana](https://github.com/gabrieldsantana).
- [Lucas Serafim](https://github.com/LucasSerafim147).
- [Thiago Felipe](https://github.com/thiagotfsilva).
- [Vanessa Rodrigues](https://github.com/Vanvrs).
