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
- **Entity Framework Core**: ORM para operações com banco de dados.
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

## Como Usar
1. **Acessar a API**:
   - Use uma ferramenta como Postman ou cURL para interagir com a API.
   - A documentação Swagger está disponível em `/swagger` quando a aplicação estiver rodando (por exemplo, `https://localhost:5001/swagger`).

2. **Exemplo de Requisição**:
   Para obter uma lista paginada de funcionários:
...

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

## Autenticação
Atualmente, o backend não implementa autenticação. Todos os endpoints são acessíveis publicamente. Para uso em produção, considere adicionar autenticação baseada em JWT ou integrar com um provedor de identidade.

## Padrão de Nomenclatura de Métodos
- **Métodos de Controladores**: Use nomenclatura RESTful (por exemplo, `GetFuncionarios`, `CreateFuncionario`, `UpdateFuncionario`).
- **Métodos de Serviços**: Use nomes descritivos que reflitam a ação (por exemplo, `ValidateSalarioBase`, `ScheduleVacation`).
- **Métodos de Repositórios**: Use padrões de acesso a dados (por exemplo, `GetByIdAsync`, `AddAsync`, `UpdateAsync`).

## Padrão de Commits
Os commits seguem a especificação **Conventional Commits**:
...
