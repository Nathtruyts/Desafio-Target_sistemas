# Desafio 2 - Estoque — resumo rápido

O programa:
- lê `estoque.json` na mesma pasta;
- aplica movimentações de entrada/saída (adiciona ou subtrai do estoque);
- valida existência do produto e estoque suficiente;
- imprime resumo da movimentação com ID, descrição e estoque final.

Trechos principais:
- modelos: `Produto`, `EstoqueJson` — mapeamento do JSON;
- leitura: `File.ReadAllText("estoque.json")` + `JsonSerializer.Deserialize<EstoqueJson>`;
- processamento: função `MovimentarEstoque(codigo, quantidade, tipo, descricao)` com regras para `entrada` e `saida`;
- saída: `Console.WriteLine` com ID gerado por `DateTime.Now.Ticks` e o `Estoque Final`.

Rodar:

```powershell
cd "c:\Users\natht\OneDrive\Documentos\target sistemas\desafio-2-estoque"
dotnet run
```
