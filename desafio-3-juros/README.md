# Desafio 3 - Juros — resumo rápido

O programa:
- solicita valor da dívida (aceita ponto ou vírgula: 54.00 ou 54,00) e data de vencimento (dd/MM/yyyy);
- calcula dias em atraso a partir de hoje (`DateTime.Today`);
- aplica juros diários de 2,5% sobre o valor original;
- imprime resultado: dias atraso, valor original, juros e total.

Trechos principais (onde olhar):
- entrada: `ParseValor()` normaliza vírgula→ponto e `DateTime.ParseExact` para data;
- cálculo: `(DateTime.Today - dataVencimento).Days` para diferença exata; `valor * 0.025 * diasAtraso` para juros simples;
- saída: `Console.WriteLine` com formatação `F2` (2 casas decimais).

Rodar:

```powershell
cd "c:\Users\natht\OneDrive\Documentos\target sistemas\desafio-3-juros"
dotnet run
```
