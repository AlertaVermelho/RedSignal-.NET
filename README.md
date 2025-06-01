# 🚨 Projeto RedSignal

**Plataforma de Monitoramento de Enchentes e Deslizamentos**  
Projeto desenvolvido para a disciplina de **Advanced Business Development with .NET**, como parte do **Global Solution 2025/3° Semestre** da FIAP.

---

## 🎯 Contexto

O RedSignal nasce como uma resposta tecnológica ao desafio de eventos extremos da natureza, proposto pela FIAP no projeto Global Solution. O objetivo é mitigar os impactos de enchentes e deslizamentos em áreas urbanas, oferecendo uma solução integrada que combina inteligência artificial, APIs REST, comunicação entre sistemas e interface administrativa para visualização de dados críticos.

---

## 🔧 Tecnologias Utilizadas

- ASP.NET Core (.NET 6)
- Razor Pages
- Entity Framework Core
- Oracle Database
- Swagger (Swashbuckle)
- Autenticação com Cookies
- Integração via API REST com backend Java
- FastAPI (Python) para serviços de IA

---

## 🛠️ Como Rodar o Projeto

### 1. Pré-requisitos
- .NET 6 SDK
- Oracle Database rodando localmente
- Visual Studio 2022 ou VS Code
- Docker (opcional para deploy com banco Oracle)
- Git

### 2. Clonar o repositório
```bash
git clone https://github.com/BeatrizFerreira01/RedSignal-.NET.git
```

### 3. Navegar até o projeto
```bash
cd RedSignal-.NET
```

### 4. Configurar o `appsettings.json`
Adicione sua connection string Oracle:
```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=xxxxxx;Password=xxxxxx;Data Source=localhost:1521/XEPDB1;"
  },
  "AdminCredentials": {
    "Username": "admin",
    "Password": "admin123"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 5. Rodar as Migrations e atualizar o banco
```bash
dotnet ef database update
```

### 6. Executar o projeto
```bash
dotnet run
```

Acesse o navegador em:  
🔗 `https://localhost:5135`

---

## 📘 Acesso ao Swagger

A documentação interativa da API é gerada automaticamente pelo Swagger. Após executar o projeto, acesse:

🔗 [`https://localhost:5135/swagger`](https://localhost:5135/swagger)

Lá você poderá testar os endpoints da API diretamente pelo navegador.

---

## 🔒 Login Administrativo

- Usuário: `admin`  
- Senha: `admin123`

---

## 📋 Funcionalidades

| Função | Descrição |
|:------:|:---------|
| API REST | Gerencia Locais Monitorados para notificação de usuários |
| Swagger | Documentação automática dos endpoints |
| Razor Pages | Interface web para administração de locais monitorados |
| Integração com Java | Recebe requisições da API Java para checagem de locais afetados |
| Autenticação | Login simples com cookies para acesso ao painel administrativo |

---

## 🖥️ Estrutura de Pastas

| Pasta | Finalidade |
|:------|:-----------|
| `Controllers` | Contém os endpoints REST da API |
| `Pages/Admin/` | Interface administrativa Razor Pages |
| `Models/` | Entidades da aplicação |
| `Services/` | Regras de negócio isoladas |
| `Data/` | Contexto do banco de dados (EF Core) |
| `wwwroot/` | Arquivos estáticos da aplicação |

---

## 📦 Exemplo de Endpoint

**POST** `/api/v1/users/{userId}/monitored-locations`

```json
{
  "nomeLocal": "Casa",
  "latitude": -23.5678,
  "longitude": -46.6789,
  "raioNotificacaoKm": 2.0
}
```

---

## 🧠 Inteligência Artificial (Python)

- **/ia/classify_text**: classifica texto de alertas por tipo e severidade  
- **/ia/cluster_alerts**: agrupa alertas geograficamente em hotspots

---

## 🤝 Integração com Java

- A API Java consulta `GET /api/v1/internal/monitored-locations/all-active` da API C#
- Compara geograficamente os locais monitorados com novos hotspots
- Envia notificações push para usuários impactados

---

## 👨‍💻 Desenvolvedores

- RM559177 - Amanda Mesquita Cirino da Silva  
- RM555698 - Beatriz Ferreira Cruz  
- RM556071 - Journey Tiago Lopes Ferreira

---

## 🎥 Pitch & Demonstração

- [🔗 Link para o vídeo Pitch (YouTube)](https://youtube.com/...)
- [🔗 Link para a Demonstração Completa (YouTube)](https://youtube.com/...)

---

## 📝 Observações Finais

Este projeto foi desenvolvido integrando múltiplas disciplinas e tecnologias com foco em:

- Segurança e usabilidade
- Arquitetura baseada em microsserviços
- Escalabilidade e integração entre APIs
- Aplicação real com dados geográficos e IA

---
