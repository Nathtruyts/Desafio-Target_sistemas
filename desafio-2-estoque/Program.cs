using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Produto
{
    public int CodigoProduto { get; set; }
    public string DescricaoProduto { get; set; } = string.Empty;
    public int Estoque { get; set; }
}

class EstoqueJson
{
    public List<Produto> Estoque { get; set; } = new List<Produto>();
}

class Program
{
    private const string JSON_FILENAME = "estoque.json";
    static EstoqueJson dadosEstoque = new EstoqueJson();

    static void Main()
    {
        var filePath = FindJsonFile();
        if (filePath == null)
            return;

        try
        {
            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            dadosEstoque = JsonSerializer.Deserialize<EstoqueJson>(json, options) ?? new EstoqueJson();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao ler ou desserializar '{JSON_FILENAME}': {ex.Message}");
            return;
        }

        Console.WriteLine("Processando movimentações...\n");

        MovimentarEstoque(101, 20, "entrada", "Reposição de estoque");
        MovimentarEstoque(102, 10, "saida", "Venda de mercadoria");
    }

    // Localiza o arquivo JSON em vários locais comuns para evitar erros de caminho
    static string? FindJsonFile()
    {
        var candidates = new[] {
            Path.Combine(AppContext.BaseDirectory, JSON_FILENAME),
            Path.Combine(Directory.GetCurrentDirectory(), JSON_FILENAME),
            Path.Combine(Directory.GetCurrentDirectory(), "..", JSON_FILENAME)
        };

        foreach (var candidate in candidates)
        {
            if (File.Exists(candidate))
            {
                Console.WriteLine($"Arquivo encontrado: {Path.GetFullPath(candidate)}\n");
                return Path.GetFullPath(candidate);
            }
        }

        Console.Error.WriteLine($"Erro: '{JSON_FILENAME}' não encontrado nos locais esperados.");
        foreach (var c in candidates)
            Console.Error.WriteLine($"  - {Path.GetFullPath(c)}");
        return null;
    }

    static void MovimentarEstoque(int codigoProduto, int quantidade, string tipo, string descricao)
    {
        var produto = dadosEstoque.Estoque.FirstOrDefault(p => p.CodigoProduto == codigoProduto);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        long idMovimentacao = DateTime.Now.Ticks;

        if (tipo == "entrada")
        {
            produto.Estoque += quantidade;
        }
        else if (tipo == "saida")
        {
            if (produto.Estoque < quantidade)
            {
                Console.WriteLine("Erro: Estoque insuficiente.");
                return;
            }

            produto.Estoque -= quantidade;
        }

        Console.WriteLine("Movimentação realizada:");
        Console.WriteLine($"ID: {idMovimentacao}");
        Console.WriteLine($"Descrição: {descricao}");
        Console.WriteLine($"Produto: {produto.DescricaoProduto}");
        Console.WriteLine($"Estoque Final: {produto.Estoque}");
        Console.WriteLine();
    }
}
