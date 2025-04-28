CREATE DATABASE ELOGICA_RH   
   
USE ELOGICA_RH   
   
CREATE TABLE Menu (   
    Id INT PRIMARY KEY IDENTITY(1,1),   
    Titulo VARCHAR(50) NOT NULL,   
    Descricao VARCHAR(250) NULL,   
    Url VARCHAR(20) NULL,   
    Icone VARCHAR(20) NULL,   
    Ordem INT NOT NULL,   
    MenuPaiId INT NULL,   
    FOREIGN KEY (MenuPaiId) REFERENCES Menu(Id)   
);   
   
CREATE TABLE Horarios (   
    Id INT PRIMARY KEY IDENTITY(1,1),   
    HorarioInicio DATETIME NOT NULL,   
    HorarioFim DATETIME NOT NULL,   
    IntervaloInicio DATETIME NULL,   
    IntervaloFim DATETIME NULL   
);   
   
CREATE TABLE Cargos(   
    Id INT PRIMARY KEY IDENTITY(1,1),   
    Titulo VARCHAR(50) NOT NULL,   
    Descricao VARCHAR(250),   
    SalarioBase FLOAT NOT NULL CHECK(SalarioBase >= 2000)   
);   
   
CREATE TABLE Setores (   
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,   
    Nome VARCHAR(50) NOT NULL,   
    Descricao VARCHAR(250) NOT NULL   
);   
   
CREATE TABLE CargosSetores (      
    CargosId INT NOT NULL,   
    SetoresId INT NOT NULL,   
    FOREIGN KEY (CargosId) REFERENCES Cargos(Id)   
        ON DELETE CASCADE ON UPDATE CASCADE,   
    FOREIGN KEY (SetoresId) REFERENCES Setores(Id)   
        ON DELETE CASCADE ON UPDATE CASCADE   
);   
   
CREATE TABLE Funcionarios (   
    Id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,   
    Nome VARCHAR(50) NOT NULL,   
    CPF VARCHAR(11) NOT NULL,   
    DataNascimento DATETIME NOT NULL,   
    Email VARCHAR(50) UNIQUE NOT NULL,   
    Telefone VARCHAR(11) NOT NULL,   
    Endereco VARCHAR(250) NOT NULL,   
    DataContratacao DATETIME NOT NULL,   
    Salario FLOAT NOT NULL CHECK (Salario>=2000),   
    Ativo BIT NOT NULL,   
    CargosId INT NOT NULL,   
    SetoresId INT NOT NULL,   
    HorariosId INT NOT NULL,   
    FOREIGN KEY (CargosId) REFERENCES Cargos(Id)   
        ON DELETE CASCADE ON UPDATE CASCADE,   
    FOREIGN KEY (SetoresId) REFERENCES Setores(Id)   
        ON DELETE CASCADE ON UPDATE CASCADE,   
    FOREIGN KEY (HorariosId) REFERENCES Horarios(Id)   
        ON DELETE CASCADE ON UPDATE CASCADE   
);   
   
CREATE TABLE Ferias (   
    Id INT PRIMARY KEY IDENTITY(1,1),   
    DataInicio DATETIME NOT NULL,   
    DataFim DATETIME NOT NULL,   
    FuncionarioId INT NOT NULL,   
    CONSTRAINT FuncionarioId FOREIGN KEY (FuncionarioId)   
        REFERENCES Funcionarios (Id)   
        ON DELETE CASCADE ON UPDATE CASCADE,   
    CONSTRAINT DatasFerias CHECK (DataFim > DataInicio)   
);

INSERT INTO Cargos(Titulo, Descricao, SalarioBase) VALUES     
('Gerente de Projetos', 'Respons vel por gerenciar equipes e projetos de TI', 12500.00),     
('Desenvolvedor S nior', 'Desenvolve sistemas complexos e lidera t cnicamente a equipe', 8500.00),     
('Analista de Sistemas', 'Analisa requisitos e prop e solu  es de software', 6500.00),     
('Desenvolvedor Pleno', 'Implementa funcionalidades e corrige bugs em sistemas', 5500.00),     
('Desenvolvedor J nior', 'Auxilia no desenvolvimento de software sob supervis o', 3200.00),     
('Estagi rio de TI', 'Apoia a equipe de desenvolvimento em tarefas b sicas', 2800.00),     
('DBA', 'Administra e otimiza bancos de dados', 7800.00),     
('Arquiteto de Software', 'Projeta a estrutura de sistemas complexos', 15000.00),     
('Scrum Master', 'Facilita processos  geis e remove impedimentos', 9500.00),     
('UX/UI Designer', 'Desenvolve interfaces e experi ncias de usu rio', 6200.00);     
   
INSERT INTO Setores (Nome, Descricao) VALUES    
('Administrativo', 'Setor respons vel pela gest o administrativa da empresa'),   
('Financeiro', 'Setor respons vel pela gest o financeira e cont bil'),   
('Recursos Humanos', 'Setor respons vel pela gest o de pessoas e departamento pessoal'),   
('TI', 'Setor respons vel pela infraestrutura de tecnologia e sistemas'),   
('Marketing', 'Setor respons vel pela comunica  o, publicidade e rela  es p blicas'),   
('Produ  o', 'Setor respons vel pela fabrica  o dos produtos');   
   
INSERT INTO Horarios (HorarioInicio, IntervaloInicio, IntervaloFim, HorarioFim) VALUES   
(8, 12, 13, 17),   
(8, 12, 14, 18), (8,  
13, 14, 17), (9,  
12, 13, 18), (9,  
12, 14, 19), (9,  
13, 14, 18),   
(10, 12, 13, 19), (10,  
12, 14, 20), (10, 13,  
14, 19);   
   
INSERT INTO Funcionarios (Nome, CPF, DataNascimento, Email, Telefone,   
Endereco, DataContratacao, Salario, Ativo, CargosId, SetoresId, HorariosId) VALUES   
('Ana Clara Souza', '12345678901', '1990-03-22T00:00:00',   
'ana.souza1@empresa.com', '11987654321', 'Av. Paulista, 1000 - S o Paulo/SP',  
'2015-04-10T09:00:00', 8500.00, 1, 2, 4, 1),   
('Bruno Henrique Lima', '23456789012', '1985-06-12T00:00:00',   
'bruno.lima@empresa.com', '11976543210', 'Rua das Laranjeiras, 245 - Rio de   
Janeiro/RJ', '2017-07-05T09:00:00', 6500.00, 1, 3, 1, 2),   
('Camila Ribeiro', '34567890123', '1992-10-30T00:00:00',   
'camila.ribeiro@empresa.com', '21987654321', 'Rua XV de Novembro, 134 -  Curitiba/PR', '2018-01-15T09:00:00', 3200.00, 1, 5, 6, 3),   
('Diego Ferreira', '45678901234', '1988-12-11T00:00:00',   
'diego.ferreira@empresa.com', '11991234567', 'Rua Independ ncia, 45 - Porto   
Alegre/RS', '2016-09-01T09:00:00', 5500.00, 1, 4, 4, 1),   
('Eduarda Costa', '56789012345', '1995-07-19T00:00:00',   
'eduarda.costa@empresa.com', '11999887766', 'Av. das Am ricas, 2222 - Rio de  
Janeiro/RJ', '2019-11-11T09:00:00', 2800.00, 1, 6, 1, 5),   
('Felipe Martins', '67890123456', '1983-04-03T00:00:00',   
'felipe.martins@empresa.com', '11988776655', 'Rua Sete de Setembro, 700 -   
Salvador/BA', '2014-06-22T09:00:00', 7800.00, 1, 7, 4, 4),   
('Gabriela Rocha', '78901234567', '1991-01-27T00:00:00',   
'gabriela.rocha@empresa.com', '11977665544', 'Rua dos Andradas, 560 - Porto   
Alegre/RS', '2020-02-20T09:00:00', 8500.00, 1, 2, 3, 2),   
('Henrique Oliveira', '89012345678', '1987-05-09T00:00:00',   
'henrique.oliveira@empresa.com', '11966554433', 'Av. Brasil, 321 - Recife/PE',  
'2013-12-10T09:00:00', 6200.00, 1, 10, 5, 3),   
('Isabela Fernandes', '90123456789', '1994-11-14T00:00:00',   
'isabela.fernandes@empresa.com', '11955443322', 'Rua das Palmeiras, 89 -   
Belo Horizonte/MG', '2021-01-05T09:00:00', 9500.00, 1, 9, 3, 6),   
('Jo o Pedro Lima', '01234567890', '1986-08-21T00:00:00',   
'joao.pedro@empresa.com', '11944332211', 'Rua Direita, 222 - Campinas/SP',  
'2012-03-18T09:00:00', 15000.00, 1, 8, 4, 7),   
('Karla Beatriz Mendes', '10293847561', '1993-03-10T00:00:00',   
'karla.mendes@empresa.com', '11933221100', 'Rua Aurora, 330 - S o  
Paulo/SP', '2020-05-11T09:00:00', 6200.00, 1, 10, 5, 8),   
('Leonardo Nunes', '11223344556', '1989-07-04T00:00:00',   
'leonardo.nunes@empresa.com', '11999889900', 'Av. da Liberdade, 550 -   
Campinas/SP', '2015-09-23T09:00:00', 7800.00, 1, 7, 1, 6),   
('Marina Castro', '66778899001', '1990-09-15T00:00:00',   
'marina.castro@empresa.com', '21999887766', 'Rua S o Jo o, 78 - S o  
Paulo/SP', '2018-10-10T09:00:00', 8500.00, 1, 2, 4, 1),   
('Nat lia Vieira', '55667788990', '1992-12-02T00:00:00',   
'natalia.vieira@empresa.com', '11987651234', 'Rua Par , 45 - Salvador/BA',   
'2017-01-30T09:00:00', 3200.00, 1, 5, 6, 9),   
('Ot vio Ramos', '44556677889', '1984-02-18T00:00:00',   
'otavio.ramos@empresa.com', '11988776543', 'Rua Goi s, 90 - Goi nia/GO',  '2013-08-12T09:00:00', 9500.00, 1, 9, 2, 7),   
('Paula Martins', '99887766554', '1995-05-25T00:00:00',   
'paula.martins@empresa.com', '11977665432', 'Av. Ipiranga, 101 - S o   
Paulo/SP', '2021-03-05T09:00:00', 5500.00, 1, 4, 5, 2),   
('Rafael Souza', '88776655443', '1987-11-29T00:00:00',   
'rafael.souza@empresa.com', '11966554321', 'Rua Pernambuco, 17 - Recife/PE',  
'2016-07-20T09:00:00', 12500.00, 1, 1, 1, 5),   
('Sabrina Alves', '77665544332', '1991-06-30T00:00:00',   
'sabrina.alves@empresa.com', '11955443210', 'Rua Brasil, 48 - Curitiba/PR',   
'2019-09-01T09:00:00', 2800.00, 1, 6, 2, 4),   
('Thiago Moura', '66554433221', '1982-04-17T00:00:00',   
'thiago.moura@empresa.com', '11944332109', 'Av. Independ ncia, 1100 -   
Fortaleza/CE', '2011-12-15T09:00:00', 15000.00, 1, 8, 4, 3),   
('Vanessa Duarte', '55443322110', '1989-08-06T00:00:00',   'vanessa.duarte@empresa.com', '11933221098', 'Rua Amazonas, 132 - Belo  Horizonte/MG', '2014-11-25T09:00:00', 6200.00, 1, 10, 3, 6),   
('Wesley Ara jo', '44332211009', '1990-01-01T00:00:00',   
'wesley.araujo@empresa.com', '11922110987', 'Rua Maranh o, 77 - S o  
Lu s/MA', '2017-02-14T09:00:00', 7800.00, 1, 7, 5, 1),   
('Yasmin Oliveira', '33221100998', '1993-11-18T00:00:00',   
'yasmin.oliveira@empresa.com', '11911099876', 'Av. Kennedy, 202 -   
Campinas/SP', '2020-06-18T09:00:00', 5500.00, 1, 4, 6, 7),   
('Z lia Campos', '22110099887', '1988-03-27T00:00:00',   
'zelia.campos@empresa.com', '11900988765', 'Rua das Hortas, 18 -  Florian polis/SC', '2012-09-19T09:00:00', 8500.00, 1, 2, 4, 5),   
('Andr  Luiz Mendes', '11009988776', '1981-07-08T00:00:00',   
'andre.mendes@empresa.com', '11899876543', 'Av. Tiradentes, 320 - S o   
Paulo/SP', '2010-01-08T09:00:00', 12500.00, 1, 1, 1, 3),   
('Bianca Rocha', '10998877665', '1994-04-24T00:00:00',   
'bianca.rocha@empresa.com', '11988765432', 'Rua Bahia, 12 - Fortaleza/CE',  
'2022-04-01T09:00:00', 3200.00, 1, 5, 3, 2),   
('Caio C sar Pinto', '99887766553', '1986-02-10T00:00:00',   
'caio.pinto@empresa.com', '11977654321', 'Rua Rio Branco, 40 - Vit ria/ES',  
'2014-10-12T09:00:00', 9500.00, 1, 9, 2, 9),   
('Daniela Lopes', '88776655442', '1990-10-11T00:00:00',   
'daniela.lopes@empresa.com', '11966543210', 'Rua Santo Ant nio, 95 - S o   
Paulo/SP', '2016-06-01T09:00:00', 6500.00, 1, 3, 1, 4),   
('Eduardo Matos', '77665544331', '1983-09-09T00:00:00',   
'eduardo.matos@empresa.com', '11955432109', 'Av. Bento Gon alves, 700 -  Porto Alegre/RS', '2013-03-20T09:00:00', 15000.00, 1, 8, 4, 8),   
('Fernanda Braga', '66554433220', '1992-06-05T00:00:00',   
'fernanda.braga@empresa.com', '11944321098', 'Rua Cear , 88 - Teresina/PI',   
'2021-07-07T09:00:00', 2800.00, 1, 6, 6, 3),   
('Gustavo Ramos', '55443322109', '1985-01-23T00:00:00',   'gustavo.ramos@empresa.com', '11933210987', 'Rua das Gaivotas, 101 -  
Natal/RN', '2011-08-10T09:00:00', 7800.00, 1, 7, 5, 2);   

-- F rias de 30 dias consecutivos
INSERT INTO Ferias (DataInicio, DataFim, FuncionarioId) VALUES
('2025-05-23T00:00:00', '2025-06-21T23:59:59', 1),
('2025-05-23T00:00:00', '2025-06-21T23:59:59', 2),
('2025-06-07T00:00:00', '2025-07-06T23:59:59', 3),
('2025-06-07T00:00:00', '2025-07-06T23:59:59', 4),
('2025-06-22T00:00:00', '2025-07-21T23:59:59', 5),
('2025-06-22T00:00:00', '2025-07-21T23:59:59', 6),
('2025-07-07T00:00:00', '2025-08-05T23:59:59', 7),
('2025-07-07T00:00:00', '2025-08-05T23:59:59', 8),
('2025-07-22T00:00:00', '2025-08-20T23:59:59', 9),
('2025-07-22T00:00:00', '2025-08-20T23:59:59', 10);

-- F rias fracionadas: 15 + 15
INSERT INTO Ferias (DataInicio, DataFim, FuncionarioId) VALUES
('2025-08-06T00:00:00', '2025-08-20T23:59:59', 11),
('2025-11-01T00:00:00', '2025-11-15T23:59:59', 11),
('2025-08-06T00:00:00', '2025-08-20T23:59:59', 12),
('2025-11-01T00:00:00', '2025-11-15T23:59:59', 12),
('2025-08-21T00:00:00', '2025-09-04T23:59:59', 13),
('2025-11-16T00:00:00', '2025-11-30T23:59:59', 13),
('2025-08-21T00:00:00', '2025-09-04T23:59:59', 14),
('2025-11-16T00:00:00', '2025-11-30T23:59:59', 14),
('2025-09-05T00:00:00', '2025-09-19T23:59:59', 15),
('2025-12-01T00:00:00', '2025-12-15T23:59:59', 15),
('2025-09-05T00:00:00', '2025-09-19T23:59:59', 16),
('2025-12-01T00:00:00', '2025-12-15T23:59:59', 16),
('2025-09-20T00:00:00', '2025-10-04T23:59:59', 17),
('2025-12-16T00:00:00', '2025-12-30T23:59:59', 17),
('2025-09-20T00:00:00', '2025-10-04T23:59:59', 18),
('2025-12-16T00:00:00', '2025-12-30T23:59:59', 18),
('2025-10-05T00:00:00', '2025-10-19T23:59:59', 19),
('2025-12-01T00:00:00', '2025-12-15T23:59:59', 19),
('2025-10-05T00:00:00', '2025-10-19T23:59:59', 20),
('2025-12-01T00:00:00', '2025-12-15T23:59:59', 20);

-- F rias fracionadas: 10 + 10 + 10
INSERT INTO Ferias (DataInicio, DataFim, FuncionarioId) VALUES
('2025-10-20T00:00:00', '2025-10-29T23:59:59', 21),
('2025-11-01T00:00:00', '2025-11-10T23:59:59', 21),
('2025-12-01T00:00:00', '2025-12-10T23:59:59', 21),
('2025-10-20T00:00:00', '2025-10-29T23:59:59', 22),
('2025-11-01T00:00:00', '2025-11-10T23:59:59', 22),
('2025-12-01T00:00:00', '2025-12-10T23:59:59', 22),
('2025-10-30T00:00:00', '2025-11-08T23:59:59', 23),
('2025-11-11T00:00:00', '2025-11-20T23:59:59', 23),
('2025-12-11T00:00:00', '2025-12-20T23:59:59', 23),
('2025-10-30T00:00:00', '2025-11-08T23:59:59', 24),
('2025-11-11T00:00:00', '2025-11-20T23:59:59', 24),
('2025-12-11T00:00:00', '2025-12-20T23:59:59', 24),
('2025-11-09T00:00:00', '2025-11-18T23:59:59', 25),
('2025-11-21T00:00:00', '2025-11-30T23:59:59', 25),
('2025-12-21T00:00:00', '2025-12-30T23:59:59', 25),
('2025-11-09T00:00:00', '2025-11-18T23:59:59', 26),
('2025-11-21T00:00:00', '2025-11-30T23:59:59', 26),
('2025-12-21T00:00:00', '2025-12-30T23:59:59', 26),
('2025-11-19T00:00:00', '2025-11-28T23:59:59', 27),
('2025-12-01T00:00:00', '2025-12-10T23:59:59', 27),
('2025-12-16T00:00:00', '2025-12-25T23:59:59', 27),
('2025-11-19T00:00:00', '2025-11-28T23:59:59', 28),
('2025-12-01T00:00:00', '2025-12-10T23:59:59', 28),
('2025-12-16T00:00:00', '2025-12-25T23:59:59', 28),
('2025-11-29T00:00:00', '2025-12-08T23:59:59', 29),
('2025-12-11T00:00:00', '2025-12-20T23:59:59', 29),
('2025-12-26T00:00:00', '2025-12-31T23:59:59', 29),
('2025-11-29T00:00:00', '2025-12-08T23:59:59', 30),
('2025-12-11T00:00:00', '2025-12-20T23:59:59', 30),
('2025-12-26T00:00:00', '2025-12-31T23:59:59', 30);

INSERT INTO Menu (Titulo, Descricao, Url, Icone, Ordem, MenuPaiId)
VALUES
('Página Inicial', 'Item que da acesso a página inicial do sistema', null, 'bi bi-house-door', 1, null),
('Parâmetros', 'Item que da acesso a página de parâmetros do sistema', null, 'bi bi-gear', 2, null),
('Funcionários', 'Item que da acesso a página de funcionários do sistema', null, 'bi bi-people', 3, null),
('Cargos', 'Item que da acesso ao item de menu Cargos, filho de parâmetros', '/cargos', null, 1, 2),
('Horários', 'Item que da acesso ao item de menu Horários, filho de parâmetros', '/horarios', null, 2, 2),
('Itens de menu', 'Item que da acesso ao item de menu, filho de parâmetros', '/itensmenu', null, 3, 2),
('Setores', 'Item que da acesso ao item de menu setores, filho de parâmetros', '/setores', null, 4, 2),
('Férias', 'Item que da acesso ao item de menu férias, filho de funcionários', '/ferias', null, 1, 3),
('Cadastros', 'Item que da acesso ao item de menu cadastros, filho de funcionários', '/cadastros', null, 2, 3);
   
INSERT INTO CargosSetores (CargosId, SetoresId) VALUES  
(1, 4), (1, 1),  
(2, 4), (2, 6), (2, 1),  
(3, 4), (3, 2), (3, 1),  
(4, 4), (4, 6), (4, 2),  
(5, 4), (5, 1), (5, 3),  
(6, 4), (6, 1), (6, 3),  
(7, 4), (7, 2), (7, 1),  
(8, 4), (8, 6), (8, 1),  
(9, 4), (9, 1), (9, 5),  
(10, 5), (10, 4), (10, 3),  
(1, 3);


