Projeto de Estudo de criação de uma API em .NET para atender um sistema de registros de marcadores cartográficos.

Este projeto pode ser usados para diversos fins. A idea central é que os usuários comuns realizam as marcações e tipificam os registros de acordo com os tipos cadastrados por administradores. Além disso, os usuários podem adicionar comentários nos marcadores e também avaliar positivamente ou negativamente as marcações. Um exemplo:

Uma prefeitura que deseja identificar por meio do auxilio de moradores quais são pontos da infraestrutura rodoviária apresenta problemas. 

Para isso, a prefeitura, ao assumir o papel de usuário administrador, pode realizar o cadastro do tipo de ocorrência que deseja identificar, como por exemplo: buraco na via, alagamento, cruzamento perigoso, faixas mal pintadas e etc. Os cidadãos, ao assumirem o papel de usuário normal, podem realizar as marcações cartográficas e tipifica-las de acordo com o que foi cadastrado pela prefeitura. Além disso, os cidadãos podem avaliar os registros de outros cidadãos adicionando comentários e engajamentos positivos e negativos. Dessa forma, a prefeitura terá ao seu dispor uma base de dados capaz de apresentar pontos de interesse da população que apresentam o maior indicie de reclamações.


Tecnologias, conceitos e metodologias utilizadas:

- .NET 7.0;
- Entity Framework com migrations, aplicando o conceito de Code-First;
- AutoMapper
- API Restfull utilizando JSON;
- Microsoft SQL Server;
- Autenticação de usuários via token JWT e configuração de roles;
- Uso do SOLID, buscando separar as responsabilidades no sistema;
- Uso de validators.
