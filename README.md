## 📈 Rastreamento-Jaeger-docker
Exemplo de API rastreamento distribuído com Jaeger e OpenTelemetry em C# ASP.NET Core 10 com banco de dados MongoDB.

#### 📋 O que voçê vai ver nesse Projeto

| Tecnologia | Descrição |
|-----------|-----------|
| **Jaeger**  | Ferramenta para rastreamento distribuído em sistemas de microsserviços. |
| **OpenTelemetry**  | Framework open source da Cloud Native Computing Foundation (CNCF) que padroniza a forma de coletar, gerar e exportar dados de telemetria de aplicações. |
| **Tracing**  | É o caminho completo da requisição, formado por uma árvore de múltiplos spans interligados. |

As aplicações geram traces estruturados. O OpenTelemetry padroniza a coleta e pode enviar tudo para o Jaeger via protocolo OTLP.

#### 💬 Requisitos do Projeto
- Necessário **Docker** instalado.
  
#### 🔄 Executar a aplicação Docker 

VSCode Terminal [1]
- Criar Container 
```bash
docker-compose up --build 
```
VSCode Terminal [2]
- Fechar Container 
```bash
docker compose down -v --rmi all --remove-orphans
```

#### 🔄 Executar a aplicação Desenvolvimento local

VSCode Terminal [1.1]
```bash
dotnet build 
cd rastreamentoJaeger
dotnet run
```

- A aplicação iniciara no Swagger **http://localhost:8080/swagger/index.html**, copie a porta e altere para o o uso.  
- Valide a Conexão com o Banco: **http://localhost:8080/health** 

#### 🧪 Executar Endpoints
- GET Id **http://localhost:8080/api/produtos/{id}**
- GET All **http://localhost:8080/api/produtos/**
- POST **http://localhost:8080/api/produtos**
```bash
{
  "nome": "ABACAXI",
  "preco": 15.50
}
```
- PUT **http://localhost:8080/api/produtos/{id}**
```bash
{
  "id" : "6a57b01696541c007da56cd8",
  "nome": "ABACAXI",
  "preco": 16.00
}
```
- DELETE **http://localhost:8080/api/Produtos/{id}**

#### 🌐 Jaeger Interface Web (UI)
- Para acessar a interface do Jaeger **http://localhost:16686**
- Atualizar sempre a página para que seja recarregados e atualizados os dados **Operation** após requisições. Cada requisição gera **TraceId** e **SpanId**.

- Jaeger **Tags(s)** Utilizar Filtros
- GET All / GET Id
```bash
http.response.status_code=200
```
- POST 
```bash
http.response.status_code=201 
```
- DELETE / PUT  
```bash
http.response.status_code=204 
```
- Visualização de requisições incorretas 
```bash
error=true
```

