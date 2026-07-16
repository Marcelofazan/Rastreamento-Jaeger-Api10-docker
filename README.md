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

#### 🔄 Executar a aplicação
- Necessário trocar todas as portas a cada execução, opção **"publishAllPorts": true** no arquivo de propriedades **launchSettings**.
- A aplicação iniciara no Swagger **https://localhost:55172/swagger/index.html**, copie a porta e altere para o o uso.  
- Valide a Conexão com o Banco: **https://localhost:55172/health** 
- Para abrir o Jaeger acesse **http://localhost:16686**

#### 🧪 Executar Endpoints

- GET All **https://localhost:55172/api/produtos/**
- GET Id **https://localhost:55172/api/produtos/6a57b01696541c007da56cd8**
- POST **https://localhost:55172/api/produtos**
```bash
{
  "nome": "ABACAXI",
  "preco": 15.50
}
```
- PUT **https://localhost:55172/api/produtos/6a57b01696541c007da56cd8**
```bash
{
  "id" : "6a57b01696541c007da56cd8",
  "nome": "ABACAXI",
  "preco": 16.00
}
```
- DELETE **https://localhost:55172/api/Produtos/6a57b01696541c007da56cd8**

#### 🌐 Jaeger Interface Web (UI)
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

