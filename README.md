# Projeto SIM-CRM
Sistema para gerenciar atendimentos e central de relacionamento ao cliente.

# Seções
<br>- Agenda de Eventos
<br>- Central de Atendimentos
<br>- Clientes - PF e PJ
<br>- Planner
<br>- Painel Atendentes
<br>- Dashboard Analítico

# CRM SIM
É um projeto aberto destinado inicialmente para pequenas prefeituras que queiram ter uma sistema informatizado de atendimento ao munícipe.

Interface do Usuário:
<br>- Asp.Net Razor Pages;
  
Backend:
<br>- <linlk src="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">.Net 6.0;</link>

Database:
<br>- <link src="https://www.microsoft.com/pt-br/download/details.aspx?id=101064">Microsoft® SQL Server® 2019 Express</link>

Packeges:
<br>- AutoMapper.Extensions.Microsoft.DependencyInjection 11.0.0;
<br>- Microsoft.AspNetCore.Authentication 2.2.0;
<br>- Microsoft.AspNetCore.Identity.EntityFrameworkCore 6.0.8;
<br>- Microsoft.AspNetCore.Identity.UI 6.0.8
<br>- Microsoft.AspNetCore.Mvc.NewtonsoftJson 6.0.8
<br>- Microsoft.EntityFrameworkCore.SqlServer 6.0.8
<br>- Microsoft.EntityFrameworkCore.Tools 6.0.8
<br>- Microsoft.EntityFrameworkCore.Design 6.0.8
<br>- Microsoft.Extensions.DependencyInjection.Abstractions 6.0.0;
<br>- EPPlus 7.0.6
<br>- Newtonsoft.Json 13.0.1

Obs: O Sim-crm é recomendado somente para uso em intranet.

# Instalação do SQL Server Express 2019:
1 Download:
Faça o download do instalador do SQL Server Express 2019 no site oficial da Microsoft.

2 Execução do Instalador:
Execute o instalador baixado e siga as instruções do assistente de instalação.

3 Configuração da Instância:
Durante a instalação, você será solicitado a configurar a instância do SQL Server. Selecione a opção desejada (geralmente a instância padrão é suficiente para ambientes locais).

4 Configuração da Autenticação:
Escolha o modo de autenticação que melhor atenda às suas necessidades. Recomenda-se usar a autenticação do Windows para ambientes locais.

5 Configuração de Recursos:
Selecione os recursos do SQL Server que você deseja instalar. Para uma instalação mínima, você pode selecionar apenas o "Database Engine Services".

6 Conclusão da Instalação:
Complete o assistente de instalação e aguarde até que a instalação seja concluída.

7 Configuração do SQL Server:
Conectando ao SQL Server:
Use o SQL Server Management Studio (SSMS) ou outra ferramenta para se conectar ao SQL Server. Você pode usar a autenticação do Windows ou a autenticação do SQL Server, dependendo da configuração escolhida durante a instalação.

8 Criando um Banco de Dados:
Crie um novo banco de dados para sua aplicação (scripts incluso no diretorio sql).

# Configuração do IIS:
1 Ativando Recursos do Windows:
Certifique-se de que os recursos necessários para o IIS estejam ativados no Windows. Você pode fazer isso no "Painel de Controle" -> "Programas" -> "Ativar ou desativar recursos do Windows". Marque "Serviços de Informações da Internet (IIS)".

2 Instalação do ASP.NET Core Hosting Bundle:
Faça o download e instale o ASP.NET Core Hosting Bundle no site oficial da Microsoft:
ASP.NET Core Hosting Bundle

# Configuração do Site no IIS:

1 Abra o "Gerenciador de IIS".
Clique com o botão direito em "Sites" e selecione "Adicionar Site".
Preencha as informações do site, apontando o caminho físico para a pasta do seu aplicativo ASP.NET Core.
Certifique-se de selecionar a versão correta do ASP.NET.
Clique em "OK" para criar o site.
Configuração do Pool de Aplicativos:

2 No "Gerenciador de IIS", clique com o botão direito no seu site recém-criado e escolha "Configurações Avançadas".
Selecione a versão correta do ASP.NET Core no campo "Versão do .NET CLR".
Configure o "Pool de Aplicativos" para usar a identidade correta (pode ser a identidade do aplicativo ou uma conta específica).
Certifique que "Pool de Aplicativos" tem acesso ao banco de dados.

3 Configuração do Firewall:
Certifique-se de que o firewall permita conexões no porto do IIS (por padrão, 80).

# Testando a Aplicação:
1 Abra um navegador e acesse sua aplicação via http://localhost ou o endereço configurado no IIS.

2 Agora, sua aplicação ASP.NET Core deve estar sendo executada no IIS, e o SQL Server Express está pronto para ser usado pela aplicação. Certifique-se de ajustar as configurações conforme necessário para atender aos requisitos específicos do seu aplicativo.




