## 📈 Rastreamento-Jaeger-Api10-docker
Exemplo de API rastreamento distribuído com Jaeger e OpenTelemetry em C# ASP.NET Core 10 com banco de dados MongoDB.

#### 📋 O que voçê vai ver nesse Projeto

| Tecnologia | Descrição |
|-----------|-----------|
| **Jaeger**  | Ferramenta para rastreamento distribuído em sistemas de microsserviços. |
| **OpenTelemetry**  | Framework open source da Cloud Native Computing Foundation (CNCF) que padroniza a forma de coletar, gerar e exportar dados de telemetria de aplicações. |
| **Tracing**  | É o caminho completo da requisição, formado por uma árvore de múltiplos spans interligados. |

#### 💬 Requisitos do Projeto
- Necessário **Docker** instalado.
- Necessário trocar todas as portas a cada execução, opção **"publishAllPorts": true** no arquivo de propriedades **launchSettings**.
  
#### 🔄 Executar a aplicação
- A aplicação iniciara no Swagger **https://localhost:XXXXX/swagger/index.html**, copie a porta e altere para o o uso.  
- Valide a Conexão com o Banco: **https://localhost:XXXXX/health** 

#### 🧪 Executar Endpoints

- GET All **https://localhost:XXXXX/api/produtos/**
- GET Id **https://localhost:XXXXX/api/produtos/{id}**
- POST **https://localhost:XXXXX/api/produtos**
```bash
{
  "nome": "ABACAXI",
  "preco": 15.50
}
```
- PUT **https://localhost:XXXXX/api/produtos/{id}**
```bash
{
  "id" : "6a57b01696541c007da56cd8",
  "nome": "ABACAXI",
  "preco": 16.00
}
```
- DELETE **https://localhost:XXXXX/api/Produtos/{id}**

#### 🌐 Jaeger Interface Web (UI)
- Para abrir o Jaeger acesse **http://localhost:16686**
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

