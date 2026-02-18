# Monitoramento de Atualizações – APIs Portal Único Siscomex 🚀

## Descrição

Este projeto realiza o **monitoramento automático das APIs do Portal Único Siscomex** 📦, verificando semanalmente alterações na documentação Swagger (OpenAPI).  
Ele atualiza arquivos locais 💾 e envia notificações por e-mail ✉️ sempre que mudanças são detectadas, garantindo rastreabilidade 🔍, antecipando impactos técnicos ⚙️ e reduzindo riscos em produção ⚠️.

---

## Objetivo

- Detectar alterações estruturais na documentação das APIs.  
- Antecipar possíveis impactos técnicos.  
- Reduzir risco de falhas em produção.  
- Garantir rastreabilidade das verificações realizadas.

---

## Contexto

Em dezembro de 2025, uma atualização nas APIs do Portal Único Siscomex exigiu ajustes técnicos na integração do E-DX.  
Diante disso, foi desenvolvido este programa interno para **identificar alterações antes que impactem o ambiente produtivo**.

---

## Funcionalidades

O monitoramento realiza:

1. Acessa os arquivos JSON da documentação Swagger das APIs monitoradas.  
2. Faz download do conteúdo atual.  
3. Compara com a versão armazenada localmente da semana anterior.  
4. Caso seja detectada qualquer alteração:  
   - Atualiza o arquivo local.  
   - Dispara notificação automática por e-mail.

Mudanças detectáveis incluem:  
- Inclusão de novos endpoints  
- Alterações em schemas  
- Mudanças de contratos  
- Atualizações de versão da API

---

## APIs Monitoradas

- **CATP** – Catálogo de Produtos  
- **DUE** – Declaração Única de Exportação  
- **Autenticação** – API de autenticação

---

## Tecnologias

- **C#** (.NET 9)  
- **HttpClient** – para requisições HTTP  
- **MD5** – para verificação de alterações  
- **SMTP** – para envio de notificações por e-mail  
