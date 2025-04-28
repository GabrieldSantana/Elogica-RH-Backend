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
As instruções a seguir são destinadas a usuários do **Microsoft Visual Studio**. Certifique-se de que o Visual Studio está instalado com o ".NET Core" ou ".NET Desktop Development" habilitado.

1. **Clonar o Repositório**:
   - Abra o Visual Studio.
   - No menu superior, vá em **File > Open > Project/Solution** ou use o atalho `Ctrl+Shift+O`.
   - Navegue até a pasta onde você deseja clonar o repositório e crie uma nova pasta chamada `Elogica-RH-Backend`.
   - Abra o **Team Explorer** (menu **View > Team Explorer** ou `Ctrl+\, Ctrl+M`).
   - No Team Explorer, clique em **Clone Repository**, insira a URL do repositório:
     ```
     https://github.com/GabrieldSantana/Elogica-RH-Backend.git
     ```
   - Escolha o caminho local (a pasta criada anteriormente) e clique em **Clone**.

2. **Abrir o Projeto**:
   - Após clonar, o Visual Studio detectará automaticamente os arquivos do projeto.
   - No **Solution Explorer** (menu **View > Solution Explorer** ou `Ctrl+Alt+L`), localize o arquivo `WebApi.sln` na pasta `Elogica-RH-Backend` e clique duas vezes para abrir a solução.

3. **Instalar Dependências**:
   - No **Solution Explorer**, clique com o botão direito na solução (`Solution 'WebApi'`) e selecione **Restore NuGet Packages**.
   - O Visual Studio restaurará automaticamente todas as dependências listadas nos arquivos `.csproj`. Certifique-se de que o Visual Studio está conectado à internet para baixar os pacotes NuGet.

4. **Configurar o Banco de Dados**:
   - **Passo 1: Configurar uma Instância do SQL Server**  
     Configure uma instância do SQL Server (local ou remota). Você pode usar o SQL Server Management Studio (SSMS) ou outra ferramenta compatível para gerenciar o banco.

   - **Passo 2: Executar o Script de Criação do Banco de Dados**  
     No **Solution Explorer**, localize a pasta `db/` no projeto. Esta pasta contém o script `script_db.sql`, que inclui todos os comandos necessários para criar o banco de dados `ELOGICA_RH`, suas tabelas e inserir dados iniciais.  
     - Abra o SSMS e conecte-se à sua instância do SQL Server.  
     - Abra o arquivo [script_db.sql](db/script_db.sql) (clique aqui para acessar).  
     - Execute o script no SSMS para criar e popular o banco de dados.

   - **Passo 3: Atualizar a String de Conexão**  
     No **Solution Explorer**, localize o arquivo `appsettings.json` no projeto `WebApi`. Clique duas vezes para abri-lo e atualize a string de conexão para apontar para o banco `ELOGICA_RH` (veja a seção "Configurando o appsettings.json" abaixo).

5. **Executar a Aplicação**:
   - No **Solution Explorer**, clique com o botão direito no projeto `WebApi` (em `Src/WebApi`) e selecione **Set as Startup Project**.
   - Pressione `F5` ou clique no botão **Start** (com o ícone de play verde) na barra de ferramentas para executar a aplicação.
   - A API estará disponível em `https://localhost:5001` (ou na porta especificada no `appsettings.json`).

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
   Para obter um cargo por id:
   ```bash
   GET "https://localhost:5001/api/cargos/2"
   ```
   Resposta:
   ```json
    {
      "success": true,
      "data": {
        "id": 2,
        "titulo": "Desenvolvedor Sênior",
        "descricao": "Desenvolve sistemas complexos e lidera técnicamente a equipe",
        "salarioBase": 8500,
        "setores": []
      }
    }
   ```

## Banco de Dados
O backend utiliza um banco de dados SQL Server com o seguinte esquema:

- **Funcionarios**: Armazena dados de funcionários (ID, Nome, CPF, DataNascimento, Email, Telefone, Endereco, DataContratacao, Salario, Ativo, CargosId).
- **Cargos**: Armazena dados de cargos (ID, Titulo, Descricao, SalarioBase).
- **Setores**: Armazena dados de setores (ID, Nome, Descricao).
- **Ferias**: Armazena dados de férias (ID, DataInicio, DataFim, FuncionariosId).
- **Horarios**: Armazena dados de horários (ID, HorarioInicio, HorarioFim, IntervaloInicio, IntervaloFim).
- **Menu**: Armazena itens de menu para navegação (ID, Titulo, Descricao, Url, Icone, Ordem, MenuPaiId).
- **CargosSetores**: Armazena ligação entre as chaves de Cargos e Setores (CargosId, SetoresId).
  
Para saber mais sobre o banco de dados, consulte a documentação [Documentacao_DB_Elogica_RH.pdf](db/Documentacao_DB_Elogica_RH.pdf) (clique aqui para acessar).

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
| GET    | `/ferias`               | Obtém as férias de trabalho dos funcionários. |
| GET    | `/ferias/{id}`          | Obtém férias de trabalho dos funcionários baseado no id. |
| GET    | `/ferias/{pagina}/{quantidade}`          | Obtém férias paginados. |
| POST   | `/ferias`           | Cria uma nova férias.          |
| PUT    | `/horario/{id}`      | Atualiza uma férias.           |
| DELETE | `/horario/{id}`      | Deleta uma férias.             |
| GET    | `/horarios`               | Obtém os horários de trabalho dos funcionários. |
| GET    | `/horarios/{id}`          | Obtém horário de trabalho dos funcionários baseado no id. |
| GET    | `/horarios/{pagina}/{quantidade}`          | Obtém horário paginados. |
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
