<h1 align="center"><b>Teste de conhecimento em C#</b></h1>
<p align="center">
  <a href="https://www.linkedin.com/in/larissa-tauana/">
    <img src="https://img.shields.io/badge/LinkedIn-blue.svg" alt="Linkedin">
  </a>
  <a href="https://github.com/LariTauana">
     img src="https://img.shields.io/badge/autora-LariTauana-red.svg" alt="LariTauana">
    </a>
</p>

<p>Criar uma listagem de fornecedores relacionado a uma empresa.
  <p></p>
    Requisitos: <li>O campo ‘Empresa’ será um cadastro a parte;</li>
                <li>Caso a empresa seja do Paraná, não permitir cadastrar um fornecedor pessoa física menor de idade;</li>
                <li>Caso o fornecedor seja pessoa física, também é necessário cadastrar o RG e a data de nascimento;</li>
                <li>A listagem de fornecedores deverá conter filtros por Nome, CPF/CNPJ e data de cadastro.</li>
</p>

<p> O Projeto consiste de back-end, feito em C#, utilizando uma Web API com APIRest, enquanto o front-end é feito em React, consumindo a API através do axios.</p>
<p>O <b>back-end</b> é construído em 3 camadas, a camada de interface de comunicação (Controllers), lógica e validações (Service) e acesso a dados (Repository).    Na solução, há também testes unitários que estão na pasta "UnitTets".</p>
<p> As exceções estão sendo filtradas através do Middleware IExceptionFilter: filtrando entre as validações e as exceções. Caso seja uma exceção (inesperada), retorna-se uma resposta genérica, e os detalhes serão registrados no logs.txt, através da classe Logger.</p>
<p>Nos models, temos as Entidades (tabelas no banco), Enums (DocumentType que é mapeado para string através do SupplierMapping, no repositório), ResponseModels (responsável por traduzir a entidade em objeto (json) esperado pelo front-end) e ValueObject (Document, pois é necessário o tipo de documento (CPF/CNPJ) e não somente o valor).</p>
<p>Tanto o repositório de fornecedor quanto o de empresa herdam do repositório genérico, implementado para evitar a repetição de código e facilitar na manutenção.</p>
<p>O Shared guarda as classes utilitárias.</p>

<p>O <b>Banco de dados</b> é construído por Migrations. Antes de rodar as migrations é necessário inserir o caminho do seu banco de dados na connection string no arquivo appsetings.json do Projeto Application.</p>

<p><b>Injeção de dependência</b>: Utilizei a injeção de dependência para desacoplar a comunicação entre as camadas (controllers, service, repository), ou seja, elas conhecem apenas as interfaces umas das outras. Essa injeção foi implementada por construtores registrando-as no Startup pelo meio do 'services.AddTransient', que instancia a classe quando a interface é chamada, e em seguida, desaloca essa instância.</p> 

<p><b>Testes Unitários</b>: Para os testes unitários, utilizei o XUnit e NSubstitute(para mockar classes que não deveriam ser testadas).</p>

<p>No <b>front-end</b> usei functional components com React hooks e está dividido em: pages, contendo duas páginas que se utilizam de componentes que estão na pasta components, e services que possui as validações, configura a API com axios e tem as funções responsáveis pelas requisições ao backend.
<p>Para subir o React, é necessário executar o comando "npm install" ou "yarn install" para baixar as dependências, e em seguida, rodar com "npm run start" ou "yarn start".</p> 

  <p></p>
Tecnologias utilizadas no back-end:
   <li>.NetCore</li>
   <li>EntityFrameworkCore</li>
   <li>SQLServer</li>
   <li>WebAPI</li>
  
  <p></p>
Tecnologias utilizadas no front-end:
   <li>React</li>
   <li>javascript, CSS e HTML
   <li>React Router DOM</li>
   <li>Material-ui-table(para a tabela)</li>
   <li>SweetAlert</li>
   <li>ReactLoading</li>
