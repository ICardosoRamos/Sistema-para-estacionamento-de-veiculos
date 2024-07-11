# entrevista_benner
Sistema para estacionamento de veículos

Observações:
Como este projeto ao meu ver precisaria de 1 semana para ser feito por completo(cheio de validações tanto no back quanto no front, e de preferencia com uma interface bem mais bonita, ousada, com animações e tmb notificações bem visuais estilo Toast), acabou que faltou bastante coisa, mas o basico da implementação solicitada na análise foi feita e imagino que isso seja suficiente para medir meu nível, acredito que como um junior que nao tinha tanta experiencia com c# em específico eu tenha conseguido fazer um bom trabalho e tão pouco tempo, afinal foram apenas dois dias de desenvolvimento, tendo que aprender mt coisa enquanto tentava desenvolver. Agora sem mais delongas...

Instalação:

para rodar o backend é necessário entrar pelo terminal integgrado do visual studio(eu indico usar o vscode que é mais simples para este projeto).

no terminal integrado primeiro entre na pasta Estacionamento.API e depois execute os comandos:
"dotnet restore", para criar as pastas obj e bin, OBS: no meu acredito que por conta de extensões ele detectou o projeto automaticamente e criou as pastas automaticamente.
"dotnet build" para verificar erros.
e por ultimo "dotnet watch run", é necessário ter o dotnet disponível versão 8.0 e ter o certificado já confiado no pc, este link mostra os detalhes:
https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0&tabs=visual-studio%2Clinux-ubuntu#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos

os preços parametrizados já vem configurados, é possível editá-los somente pelo swagger do backend.

para ligar o frontend em react, é necessário outro terminal integrado ao mesmo tempo, depois entrar na pasta frontend, ter o node instalado, no terminal digitar npm install na primeira vez e depois npm run dev.



Analise Técnica:

Desenvolver um aplicativo simples para controle de estacionamento onde o usuário poderá registrar a entrada e saída dos veículos.

Os valores praticados pelo estacionamento devem ser parametrizados em uma tabela de preços com controle vigência. Exemplo: Valores válidos para o período de 01/01/2024 até 31/12/2024.

Utilizar a data de entrada do veículo como referência para buscar a tabela de preços.

A tabela de preço deve contemplar o valor da hora inicial e valor para as horas adicionais.

Será cobrado metade do valor da hora inicial quando o tempo total de permanência no estacionamento for igual ou inferior a 30 minutos.

O valor da hora adicional possui uma tolerância de 10 minutos para cada 1 hora. Exemplo: 30 minutos valor R$ 1,00 | 1 hora valor R$ 2,00 | 1 hora 10 minutos valor R$ 2,00 | 1 hora e 15 minutos valor R$ 3,00 | 2 horas e 5 minutos valor R$ 3,00 | 2 horas e 15 minutos valor R$ 4,00.

Utilizar a placa do veículo como chave de busca.

O sistema deve possuir uma interface desktop ou web para registrar as entradas, saídas e parametrizações.

Utilizar uma estrutura de armazenamento local como Arquivo, SQLite, Access, MySql, DB4o, etc.

O sistema deve ser implementado em C#.
A interface pode ser Desktop ou Web.
Se possível utilizar conceitos de mercado como Entity framework, LINQ, MVC, design
patterns, TDD.

