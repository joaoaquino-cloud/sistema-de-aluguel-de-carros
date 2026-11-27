using System;
using System.Collections.Generic;
using System.IO;
using LocadoraCarros;
class Program
{
    static void salvarDados(List<CarroAluguel> listaCarros, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);
        for (int i = 0; i < listaCarros.Count; i++)
        {
            writer.WriteLine($"{listaCarros[i].modelo};{listaCarros[i].placa};{listaCarros[i].ano};{listaCarros[i].valorDiaria};{listaCarros[i].alugado}");
        }
        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }

    static void carregarDados(List<CarroAluguel> listaCarros, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            for (int i = 0; i < linhasDoArquivo.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(linhasDoArquivo[i]))
                {
                    string[] campos = linhasDoArquivo[i].Split(';');
                    CarroAluguel c = new CarroAluguel();
                    c.modelo = campos[0];
                    c.placa = campos[1];
                    c.ano = int.Parse(campos[2]);
                    c.valorDiaria = double.Parse(campos[3]);
                    c.alugado = bool.Parse(campos[4]);
                    listaCarros.Add(c);
                }
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo CSV não encontrado :(");
        }
    }

    static void addCarro(List<CarroAluguel> listaCarros)
    {
        CarroAluguel novoCarro = new CarroAluguel();
        Console.WriteLine("=== CADASTRAR NOVO CARRO ===");
        Console.Write("Modelo do veículo: ");
        novoCarro.modelo = Console.ReadLine();
        Console.Write("Placa do veículo: ");
        novoCarro.placa = Console.ReadLine();
        Console.Write("Ano de fabricação: ");
        novoCarro.ano = int.Parse(Console.ReadLine());
        Console.Write("Valor da diária (R$): ");
        novoCarro.valorDiaria = double.Parse(Console.ReadLine());
        Console.Write("Está alugado no momento? (1-Sim / 2-Não): ");
        int opAlugado = int.Parse(Console.ReadLine());
        novoCarro.alugado = (opAlugado == 1);
        listaCarros.Add(novoCarro);
        Console.WriteLine("Carro cadastrado com sucesso!");
    }

    static void mostrarCarros(List<CarroAluguel> listaCarros)
    {
        Console.WriteLine("=== FROTA COMPLETA DE VEÍCULOS ===");
        for (int i = 0; i < listaCarros.Count; i++)
        {
            string status = listaCarros[i].alugado ? "ALUGADO" : "DISPONÍVEL";
            Console.WriteLine($"[{i + 1}] {listaCarros[i].modelo} | Placa: {listaCarros[i].placa} | Ano: {listaCarros[i].ano} | Diária: R$ {listaCarros[i].valorDiaria:F2} | Status: {status}");
        }
    }

    static int menu()
    {
        Console.WriteLine("=== SISTEMA DE ALUGUEL DE CARROS ===");
        Console.WriteLine("1 - Cadastrar Novo Carro");
        Console.WriteLine("2 - Mostrar Todos os Carros da Frota");
        Console.WriteLine("3 - Buscar Carro pela Placa");
        Console.WriteLine("4 - Verificar Carro de Maior e Menor Valor de Aluguel");
        Console.WriteLine("5 - Mostrar Apenas Carros Disponíveis");
        Console.WriteLine("6 - Mostrar Apenas Carros Alugados");
        Console.WriteLine("0 - Salvar e Sair");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine());
    }

    static bool buscarPorPlaca(List<CarroAluguel> listaCarros, string placaBusca)
    {
        bool encontrado = false;

        for (int i = 0; i < listaCarros.Count; i++)
        {
            if (listaCarros[i].placa.ToUpper() == placaBusca.ToUpper())
            {
                string status = listaCarros[i].alugado ? "ALUGADO" : "DISPONÍVEL";
                Console.WriteLine($"[{i + 1}] {listaCarros[i].modelo} | Placa: {listaCarros[i].placa} | Ano: {listaCarros[i].ano} | Diária: R$ {listaCarros[i].valorDiaria:F2} | Status: {status}");
                encontrado = true;
            }
        }

        return encontrado;
    }

    static void verificarMaiorMenorValor(List<CarroAluguel> listaCarros)
    {
        if (listaCarros.Count == 0)
        {
            Console.WriteLine("Nenhum carro cadastrado na frota.");
            return;
        }

        CarroAluguel carroMaior = listaCarros[0];
        CarroAluguel carroMenor = listaCarros[0];

        for (int i = 1; i < listaCarros.Count; i++)
        {
            if (listaCarros[i].valorDiaria > carroMaior.valorDiaria)
            {
                carroMaior = listaCarros[i];
            }
            if (listaCarros[i].valorDiaria < carroMenor.valorDiaria)
            {
                carroMenor = listaCarros[i];
            }
        }

        string statusMaior = carroMaior.alugado ? "ALUGADO" : "DISPONÍVEL";
        string statusMenor = carroMenor.alugado ? "ALUGADO" : "DISPONÍVEL";

        Console.WriteLine("=== MAIOR E MENOR VALOR DE DIÁRIA ===");
        Console.WriteLine($"MAIOR VALOR -> {carroMaior.modelo} | Placa: {carroMaior.placa} | Ano: {carroMaior.ano} | Diária: R$ {carroMaior.valorDiaria:F2} | Status: {statusMaior}");
        Console.WriteLine($"MENOR VALOR -> {carroMenor.modelo} | Placa: {carroMenor.placa} | Ano: {carroMenor.ano} | Diária: R$ {carroMenor.valorDiaria:F2} | Status: {statusMenor}");
    }

    static void mostrarCarrosDisponiveis(List<CarroAluguel> listaCarros)
    {
        Console.WriteLine("=== CARROS DISPONÍVEIS ===");
        for (int i = 0; i < listaCarros.Count; i++)
        {
            if (listaCarros[i].alugado == false)
            {
                Console.WriteLine($"[{i + 1}] {listaCarros[i].modelo} | Placa: {listaCarros[i].placa} | Ano: {listaCarros[i].ano} | Diária: R$ {listaCarros[i].valorDiaria:F2}");
            }
        }
    }

    static void mostrarCarrosAlugados(List<CarroAluguel> listaCarros)
    {
        Console.WriteLine("=== CARROS ALUGADOS ===");
        for (int i = 0; i < listaCarros.Count; i++)
        {
            if (listaCarros[i].alugado == true)
            {
                Console.WriteLine($"[{i + 1}] {listaCarros[i].modelo} | Placa: {listaCarros[i].placa} | Ano: {listaCarros[i].ano} | Diária: R$ {listaCarros[i].valorDiaria:F2}");
            }
        }
    }

    static void Main()
    {
        List<CarroAluguel> listaCarros = new List<CarroAluguel>();
        carregarDados(listaCarros, "carros.csv");

        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    addCarro(listaCarros);
                    break;
                case 2:
                    mostrarCarros(listaCarros);
                    break;
                case 3:
                    Console.Write("Digite a placa desejada: ");
                    string placa = Console.ReadLine();
                    if (!buscarPorPlaca(listaCarros, placa))
                    {
                        Console.WriteLine("Carro não encontrado com a placa informada.");
                    }
                    break;
                case 4:
                    verificarMaiorMenorValor(listaCarros);
                    break;
                case 5:
                    mostrarCarrosDisponiveis(listaCarros);
                    break;
                case 6:
                    mostrarCarrosAlugados(listaCarros);
                    break;
                case 0:
                    Console.WriteLine("Salvando dados e encerrando...");
                    salvarDados(listaCarros, "carros.csv");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (opcao != 0);
    }
}

