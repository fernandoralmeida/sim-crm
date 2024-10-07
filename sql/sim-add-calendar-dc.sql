IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Empresa] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJ] varchar(18) NOT NULL,
    [Data_Abertura] datetime2 NULL,
    [Nome_Empresarial] varchar(max) NULL,
    [Nome_Fantasia] varchar(max) NULL,
    [CNAE_Principal] varchar(max) NULL,
    [Atividade_Principal] varchar(max) NULL,
    [Atividade_Secundarias] varchar(max) NULL,
    [CEP] varchar(10) NULL,
    [Logradouro] varchar(max) NULL,
    [Numero] varchar(max) NULL,
    [Complemento] varchar(max) NULL,
    [Bairro] varchar(max) NULL,
    [Municipio] varchar(max) NULL,
    [UF] varchar(2) NULL,
    [Email] varchar(max) NULL,
    [Telefone] varchar(max) NULL,
    [Situacao_Cadastral] varchar(max) NULL,
    CONSTRAINT [PK_Empresa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Evento] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL,
    [Tipo] varchar(max) NULL,
    [Nome] varchar(max) NULL,
    [Formato] varchar(255) NULL,
    [Data] datetime2 NULL,
    [Descricao] varchar(max) NULL,
    [Owner] varchar(max) NULL,
    [Parceiro] nvarchar(max) NULL,
    [Lotacao] int NOT NULL,
    [Situacao] int NULL,
    CONSTRAINT [PK_Evento] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pessoa] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) COLLATE Latin1_General_CI_AI NOT NULL,
    [Nome_Social] varchar(max) NULL,
    [Data_Nascimento] datetime2 NOT NULL,
    [CPF] varchar(14) NOT NULL,
    [RG] varchar(max) NULL,
    [RG_Emissor] varchar(max) NULL,
    [RG_Emissor_UF] varchar(2) NULL,
    [Genero] nvarchar(max) NULL,
    [Deficiencia] nvarchar(max) NULL,
    [CEP] varchar(max) NULL,
    [Logradouro] varchar(max) COLLATE Latin1_General_CI_AI NULL,
    [Numero] varchar(max) NULL,
    [Complemento] varchar(max) NULL,
    [Bairro] varchar(max) COLLATE Latin1_General_CI_AI NULL,
    [Cidade] varchar(max) NULL,
    [UF] varchar(2) NULL,
    [Tel_Movel] varchar(max) NULL,
    [Tel_Fixo] varchar(max) NULL,
    [Email] varchar(max) NULL,
    [Data_Cadastro] datetime2 NULL,
    [Ultima_Alteracao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Pessoa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Planer] (
    [Id] uniqueidentifier NOT NULL,
    [Segunda] varchar(max) NULL,
    [Terca] varchar(max) NULL,
    [Quarta] varchar(max) NULL,
    [Quinta] varchar(max) NULL,
    [Sexta] varchar(max) NULL,
    [Sabado] varchar(max) NULL,
    [ProximaSemana] varchar(max) NULL,
    [Prioridades] varchar(max) NULL,
    [Anotacao] varchar(max) NULL,
    [DataInicial] datetime2 NULL,
    [DataFinal] datetime2 NULL,
    [Owner_AppUser_Id] nvarchar(max) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Planer] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Protocolos] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] varchar(max) NULL,
    [Modulo] varchar(max) NULL,
    [AppUserId] nvarchar(max) NULL,
    [Data] datetime2 NULL,
    CONSTRAINT [PK_Protocolos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Secretaria] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [Acronimo] varchar(max) NULL,
    [Hierarquia] int NULL,
    [Dominio] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Secretaria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [StatusAtendimento] (
    [Id] uniqueidentifier NOT NULL,
    [UnserName] varchar(256) NULL,
    [Online] bit NOT NULL,
    CONSTRAINT [PK_StatusAtendimento] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Atendimento] (
    [Id] uniqueidentifier NOT NULL,
    [Protocolo] varchar(256) NOT NULL,
    [Data] datetime2 NULL,
    [DataF] datetime2 NULL,
    [Setor] varchar(max) NULL,
    [Canal] varchar(max) NULL,
    [Servicos] varchar(max) NULL,
    [Descricao] varchar(max) NULL,
    [Status] varchar(max) NULL,
    [Ultima_Alteracao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    [Anonimo] bit NULL,
    [Owner_AppUser_Id] nvarchar(max) NULL,
    [PessoaId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    CONSTRAINT [PK_Atendimento] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Atendimento_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Atendimento_Pessoa_PessoaId] FOREIGN KEY ([PessoaId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE TABLE [Inscricao] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] int NOT NULL,
    [AplicationUser_Id] nvarchar(max) NULL,
    [Data_Inscricao] datetime2 NULL,
    [Presente] bit NOT NULL,
    [ParticipanteId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    [EventoId] uniqueidentifier NULL,
    CONSTRAINT [PK_Inscricao] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Inscricao_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Inscricao_Evento_EventoId] FOREIGN KEY ([EventoId]) REFERENCES [Evento] ([Id]),
    CONSTRAINT [FK_Inscricao_Pessoa_ParticipanteId] FOREIGN KEY ([ParticipanteId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE TABLE [Vinculos] (
    [Id] uniqueidentifier NOT NULL,
    [Vinculo] int NOT NULL,
    [PessoaId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    CONSTRAINT [PK_Vinculos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vinculos_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Vinculos_Pessoa_PessoaId] FOREIGN KEY ([PessoaId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE TABLE [Canal] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Canal] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Canal_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Parceiros] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Parceiros] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Parceiros_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Servico] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Servico] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Servico_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Tipos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [Tipo] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Tipos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tipos_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE INDEX [IX_Atendimento_EmpresaId] ON [Atendimento] ([EmpresaId]);
GO

CREATE INDEX [IX_Atendimento_PessoaId] ON [Atendimento] ([PessoaId]);
GO

CREATE UNIQUE INDEX [IX_Atendimento_Protocolo] ON [Atendimento] ([Protocolo]);
GO

CREATE INDEX [IX_Canal_DominioId] ON [Canal] ([DominioId]);
GO

CREATE UNIQUE INDEX [IX_Empresa_CNPJ] ON [Empresa] ([CNPJ]);
GO

CREATE INDEX [IX_Inscricao_EmpresaId] ON [Inscricao] ([EmpresaId]);
GO

CREATE INDEX [IX_Inscricao_EventoId] ON [Inscricao] ([EventoId]);
GO

CREATE INDEX [IX_Inscricao_ParticipanteId] ON [Inscricao] ([ParticipanteId]);
GO

CREATE INDEX [IX_Parceiros_DominioId] ON [Parceiros] ([DominioId]);
GO

CREATE UNIQUE INDEX [IX_Pessoa_CPF] ON [Pessoa] ([CPF]);
GO

CREATE INDEX [IX_Servico_DominioId] ON [Servico] ([DominioId]);
GO

CREATE INDEX [IX_Tipos_DominioId] ON [Tipos] ([DominioId]);
GO

CREATE INDEX [IX_Vinculos_EmpresaId] ON [Vinculos] ([EmpresaId]);
GO

CREATE INDEX [IX_Vinculos_PessoaId] ON [Vinculos] ([PessoaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240206200625_db-sim-application-crm', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Atendimento] ADD [DominioId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_Atendimento_DominioId] ON [Atendimento] ([DominioId]);
GO

ALTER TABLE [Atendimento] ADD CONSTRAINT [FK_Atendimento_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240208112805_add-atendimento-domain', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Evento] ADD [DominioId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_Evento_DominioId] ON [Evento] ([DominioId]);
GO

ALTER TABLE [Evento] ADD CONSTRAINT [FK_Evento_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240208131349_add-evento-domain', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Reminders] (
    [Id] uniqueidentifier NOT NULL,
    [Titulo] varchar(255) NULL,
    [Local] varchar(255) NULL,
    [Visivel] int NOT NULL,
    [Data] datetime2 NOT NULL,
    [Descricao] varchar(max) NULL,
    [Owner] varchar(255) NULL,
    [Status] bit NULL,
    CONSTRAINT [PK_Reminders] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240926001907_add-calendar', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Reminders] ADD [Data_Cadastro] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

CREATE TABLE [Empresa] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJ] varchar(18) NOT NULL,
    [Data_Abertura] datetime2 NULL,
    [Nome_Empresarial] varchar(max) NULL,
    [Nome_Fantasia] varchar(max) NULL,
    [CNAE_Principal] varchar(max) NULL,
    [Atividade_Principal] varchar(max) NULL,
    [Atividade_Secundarias] varchar(max) NULL,
    [CEP] varchar(10) NULL,
    [Logradouro] varchar(max) NULL,
    [Numero] varchar(max) NULL,
    [Complemento] varchar(max) NULL,
    [Bairro] varchar(max) NULL,
    [Municipio] varchar(max) NULL,
    [UF] varchar(2) NULL,
    [Email] varchar(max) NULL,
    [Telefone] varchar(max) NULL,
    [Situacao_Cadastral] varchar(max) NULL,
    CONSTRAINT [PK_Empresa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pessoa] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) COLLATE Latin1_General_CI_AI NOT NULL,
    [Nome_Social] varchar(max) NULL,
    [Data_Nascimento] datetime2 NOT NULL,
    [CPF] varchar(14) NOT NULL,
    [RG] varchar(max) NULL,
    [RG_Emissor] varchar(max) NULL,
    [RG_Emissor_UF] varchar(2) NULL,
    [Genero] nvarchar(max) NULL,
    [Deficiencia] nvarchar(max) NULL,
    [CEP] varchar(max) NULL,
    [Logradouro] varchar(max) COLLATE Latin1_General_CI_AI NULL,
    [Numero] varchar(max) NULL,
    [Complemento] varchar(max) NULL,
    [Bairro] varchar(max) COLLATE Latin1_General_CI_AI NULL,
    [Cidade] varchar(max) NULL,
    [UF] varchar(2) NULL,
    [Tel_Movel] varchar(max) NULL,
    [Tel_Fixo] varchar(max) NULL,
    [Email] varchar(max) NULL,
    [Data_Cadastro] datetime2 NULL,
    [Ultima_Alteracao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Pessoa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Planer] (
    [Id] uniqueidentifier NOT NULL,
    [Segunda] varchar(max) NULL,
    [Terca] varchar(max) NULL,
    [Quarta] varchar(max) NULL,
    [Quinta] varchar(max) NULL,
    [Sexta] varchar(max) NULL,
    [Sabado] varchar(max) NULL,
    [ProximaSemana] varchar(max) NULL,
    [Prioridades] varchar(max) NULL,
    [Anotacao] varchar(max) NULL,
    [DataInicial] datetime2 NULL,
    [DataFinal] datetime2 NULL,
    [Owner_AppUser_Id] nvarchar(max) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Planer] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Protocolos] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] varchar(max) NULL,
    [Modulo] varchar(max) NULL,
    [AppUserId] nvarchar(max) NULL,
    [Data] datetime2 NULL,
    CONSTRAINT [PK_Protocolos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RaeSebrae] (
    [Id] uniqueidentifier NOT NULL,
    [RAE] nvarchar(max) NULL,
    CONSTRAINT [PK_RaeSebrae] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Secretaria] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [Acronimo] varchar(max) NULL,
    [Hierarquia] int NULL,
    [Dominio] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Secretaria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [StatusAtendimento] (
    [Id] uniqueidentifier NOT NULL,
    [UnserName] varchar(256) NULL,
    [Online] bit NOT NULL,
    CONSTRAINT [PK_StatusAtendimento] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Simples] (
    [Id] uniqueidentifier NOT NULL,
    [Documento] varchar(255) NULL,
    [Exercicio] varchar(255) NULL,
    [Chave] nvarchar(max) NULL,
    [EmpresaId] uniqueidentifier NULL,
    CONSTRAINT [PK_Simples] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Simples_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id])
);
GO

CREATE TABLE [BPPContratos] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] int NOT NULL,
    [Data] datetime2 NULL,
    [Valor] decimal(18,2) NOT NULL,
    [Situacao] int NOT NULL,
    [DataSituacao] datetime2 NULL,
    [ClienteId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    [Pagamento] int NOT NULL,
    [Descricao] varchar(255) NULL,
    [AppUser] varchar(255) NULL,
    [UltimaAlteracao] datetime2 NULL,
    CONSTRAINT [PK_BPPContratos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BPPContratos_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_BPPContratos_Pessoa_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE TABLE [Vinculos] (
    [Id] uniqueidentifier NOT NULL,
    [Vinculo] int NOT NULL,
    [PessoaId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    CONSTRAINT [PK_Vinculos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vinculos_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Vinculos_Pessoa_PessoaId] FOREIGN KEY ([PessoaId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE TABLE [Atendimento] (
    [Id] uniqueidentifier NOT NULL,
    [Protocolo] varchar(256) NOT NULL,
    [Data] datetime2 NULL,
    [DataF] datetime2 NULL,
    [Setor] varchar(max) NULL,
    [Canal] varchar(max) NULL,
    [Servicos] varchar(max) NULL,
    [Descricao] varchar(max) NULL,
    [Status] varchar(max) NULL,
    [Ultima_Alteracao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    [Anonimo] bit NULL,
    [Owner_AppUser_Id] nvarchar(max) NULL,
    [PessoaId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    [DominioId] uniqueidentifier NULL,
    [SebraeId] uniqueidentifier NULL,
    CONSTRAINT [PK_Atendimento] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Atendimento_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Atendimento_Pessoa_PessoaId] FOREIGN KEY ([PessoaId]) REFERENCES [Pessoa] ([Id]),
    CONSTRAINT [FK_Atendimento_RaeSebrae_SebraeId] FOREIGN KEY ([SebraeId]) REFERENCES [RaeSebrae] ([Id]),
    CONSTRAINT [FK_Atendimento_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Canal] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Canal] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Canal_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Evento] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL,
    [Tipo] varchar(max) NULL,
    [Nome] varchar(max) NULL,
    [Formato] varchar(255) NULL,
    [Data] datetime2 NULL,
    [Descricao] varchar(max) NULL,
    [Owner] varchar(max) NULL,
    [Parceiro] nvarchar(max) NULL,
    [Lotacao] int NOT NULL,
    [Situacao] int NULL,
    [DominioId] uniqueidentifier NULL,
    CONSTRAINT [PK_Evento] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Evento_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Parceiros] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Parceiros] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Parceiros_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Servico] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Servico] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Servico_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Tipos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [Tipo] varchar(max) NULL,
    [DominioId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Tipos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tipos_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [BPPRenegociacoes] (
    [Id] uniqueidentifier NOT NULL,
    [ContratoId] uniqueidentifier NULL,
    [Data] datetime2 NULL,
    [Situacao] int NOT NULL,
    CONSTRAINT [PK_BPPRenegociacoes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BPPRenegociacoes_BPPContratos_ContratoId] FOREIGN KEY ([ContratoId]) REFERENCES [BPPContratos] ([Id])
);
GO

CREATE TABLE [Inscricao] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] int NOT NULL,
    [AplicationUser_Id] nvarchar(max) NULL,
    [Data_Inscricao] datetime2 NULL,
    [Presente] bit NOT NULL,
    [ParticipanteId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    [EventoId] uniqueidentifier NULL,
    CONSTRAINT [PK_Inscricao] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Inscricao_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Inscricao_Evento_EventoId] FOREIGN KEY ([EventoId]) REFERENCES [Evento] ([Id]),
    CONSTRAINT [FK_Inscricao_Pessoa_ParticipanteId] FOREIGN KEY ([ParticipanteId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE INDEX [IX_Atendimento_DominioId] ON [Atendimento] ([DominioId]);
GO

CREATE INDEX [IX_Atendimento_EmpresaId] ON [Atendimento] ([EmpresaId]);
GO

CREATE INDEX [IX_Atendimento_PessoaId] ON [Atendimento] ([PessoaId]);
GO

CREATE UNIQUE INDEX [IX_Atendimento_Protocolo] ON [Atendimento] ([Protocolo]);
GO

CREATE INDEX [IX_Atendimento_SebraeId] ON [Atendimento] ([SebraeId]);
GO

CREATE INDEX [IX_BPPContratos_ClienteId] ON [BPPContratos] ([ClienteId]);
GO

CREATE INDEX [IX_BPPContratos_EmpresaId] ON [BPPContratos] ([EmpresaId]);
GO

CREATE INDEX [IX_BPPRenegociacoes_ContratoId] ON [BPPRenegociacoes] ([ContratoId]);
GO

CREATE INDEX [IX_Canal_DominioId] ON [Canal] ([DominioId]);
GO

CREATE UNIQUE INDEX [IX_Empresa_CNPJ] ON [Empresa] ([CNPJ]);
GO

CREATE INDEX [IX_Evento_DominioId] ON [Evento] ([DominioId]);
GO

CREATE INDEX [IX_Inscricao_EmpresaId] ON [Inscricao] ([EmpresaId]);
GO

CREATE INDEX [IX_Inscricao_EventoId] ON [Inscricao] ([EventoId]);
GO

CREATE INDEX [IX_Inscricao_ParticipanteId] ON [Inscricao] ([ParticipanteId]);
GO

CREATE INDEX [IX_Parceiros_DominioId] ON [Parceiros] ([DominioId]);
GO

CREATE UNIQUE INDEX [IX_Pessoa_CPF] ON [Pessoa] ([CPF]);
GO

CREATE INDEX [IX_Servico_DominioId] ON [Servico] ([DominioId]);
GO

CREATE INDEX [IX_Simples_EmpresaId] ON [Simples] ([EmpresaId]);
GO

CREATE INDEX [IX_Tipos_DominioId] ON [Tipos] ([DominioId]);
GO

CREATE INDEX [IX_Vinculos_EmpresaId] ON [Vinculos] ([EmpresaId]);
GO

CREATE INDEX [IX_Vinculos_PessoaId] ON [Vinculos] ([PessoaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241006230251_add-calendar-dc', N'6.0.8');
GO

COMMIT;
GO

