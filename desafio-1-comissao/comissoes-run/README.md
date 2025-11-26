# Comissoes (.NET) — resumo rápido

O programa:
- lê `vendas.json` na pasta pai;
- calcula comissões ( <100 = 0; 100–499 = 1%; >=500 = 5% );
- soma por vendedor e imprime arredondado em `pt-BR`.

Trechos principais
- modelos: `Venda`, `Root` — mapeamento do JSON;
- leitura: `File.Exists` + `File.ReadAllText`;
- desserialização: `JsonSerializer.Deserialize<Root>(..., PropertyNameCaseInsensitive = true)`;
- cálculo: loop que acumula em `Dictionary<string,double>`;
- saída: `Math.Round(...,2)` + `CultureInfo("pt-BR").ToString("C")`.

Rodar:

```powershell
cd "c:\Users\natht\OneDrive\Documentos\target sistemas\desafio-1-comissao\comissoes-run"
dotnet run
```

Nota: o programa procura `vendas.json` em vários locais (bin, pasta do projeto e pasta pai) e imprime qual arquivo foi usado, para evitar erros em file path.
