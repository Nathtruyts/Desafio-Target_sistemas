using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== CÁLCULO DE JUROS POR ATRASO ===");

        Console.Write("Digite o valor da dívida (ex: 54.00 ou 54,00): R$ ");
        decimal valor = ParseValor(Console.ReadLine());

        Console.Write("Digite a data de vencimento (formato: dd/MM/yyyy): ");
        string dataTexto = Console.ReadLine();
        DateTime dataVencimento = DateTime.ParseExact(dataTexto, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        CalcularJuros(valor, dataVencimento);
    }

    static decimal ParseValor(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0m;

        input = input.Replace(",", ".");
        if (decimal.TryParse(input, CultureInfo.InvariantCulture, out decimal resultado))
            return resultado;

        Console.Error.WriteLine("Erro: valor inválido. Usando 0.");
        return 0m;
    }

    static void CalcularJuros(decimal valor, DateTime dataVencimento)
    {
        DateTime dataAtual = DateTime.Today;

        int diasAtraso = (dataAtual - dataVencimento).Days;

        if (diasAtraso <= 0)
        {
            Console.WriteLine($"\nPagamento em dia.");
            Console.WriteLine($"Data de vencimento: {dataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Valor original: R$ {valor:F2}");
            return;
        }

        decimal multaDiaria = 0.025m;
        decimal juros = valor * multaDiaria * diasAtraso;
        decimal totalComJuros = valor + juros;

        Console.WriteLine("\n--- RESULTADO ---");
        Console.WriteLine($"Data de hoje: {dataAtual:dd/MM/yyyy}");
        Console.WriteLine($"Data de vencimento: {dataVencimento:dd/MM/yyyy}");
        Console.WriteLine($"Dias em atraso: {diasAtraso}");
        Console.WriteLine($"Valor original: R$ {valor:F2}");
        Console.WriteLine($"Juros (2,5% ao dia): R$ {juros:F2}");
        Console.WriteLine($"Total com juros: R$ {totalComJuros:F2}");
    }
}
