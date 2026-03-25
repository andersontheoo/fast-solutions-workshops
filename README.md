🚀 Fast Solutions Workshops API

API desenvolvida como parte do desafio técnico para estágio FullStack da FAST Soluções.

O objetivo do projeto é gerenciar workshops e a participação de colaboradores, permitindo análise e rastreamento de حضور nos eventos.

📌 Tecnologias utilizadas
C#
.NET (Web API)
ASP.NET Core
Swagger (documentação da API)
📂 Estrutura do Projeto

O projeto segue uma arquitetura simples e organizada:

Controllers → Responsáveis pelos endpoints da API
Models → Representação das entidades (Workshop, Colaborador)
Services/Repositories (se houver) → Regras de negócio e acesso a dados
⚙️ Funcionalidades
👨‍💼 Colaboradores
Criar colaborador
Listar colaboradores
Buscar por ID
Atualizar colaborador
Remover colaborador
📚 Workshops
Criar workshop
Listar workshops
Buscar por ID
Atualizar workshop
Remover workshop
🔗 Endpoints principais
Colaboradores
GET    /api/colaboradores
GET    /api/colaboradores/{id}
POST   /api/colaboradores
PUT    /api/colaboradores/{id}
DELETE /api/colaboradores/{id}
Workshops
GET    /api/workshops
GET    /api/workshops/{id}
POST   /api/workshops
PUT    /api/workshops/{id}
DELETE /api/workshops/{id}
📖 Documentação da API

A documentação interativa está disponível via Swagger:

/swagger
▶️ Como executar o projeto
Pré-requisitos
.NET SDK instalado
Passos
# Clone o repositório
git clone https://github.com/andersontheoo/fast-solutions-workshops.git

# Acesse a pasta frontend
cd fast-solutions-workshops/WorkshopTrackerAPI

# Execute o projeto
dotnet run

A API estará disponível em:

https://localhost:xxxx
🧠 Decisões de Projeto
Utilização de arquitetura simples para facilitar manutenção
Separação de responsabilidades entre controllers e modelos
API REST seguindo boas práticas de organização
no navegador pesquise https://localhost:xxxx/swagger




👨‍💻 Autor

Desenvolvido por Anderson

📌 Observações

Este projeto foi desenvolvido com foco em demonstrar conhecimentos em desenvolvimento backend, organização de código e construção de APIs REST.
