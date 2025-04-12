
public class Produto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public string Categoria { get; set; }
    public string Marca { get; set; }
    public string Codigo { get; set; }

    public Produto(string nome, decimal preco, string categoria, string marca, string codigo, int quantidade)
    {
        Nome = nome;
        Preco = preco;
        Categoria = categoria;
        Marca = marca;
        Codigo = codigo;
        Quantidade = quantidade;
    }

    public void AdicionarQuantidade(int quantidade)
    {
        if (quantidade > 0)
        {
            Quantidade += quantidade;
        }
    }

    public bool RemoverQuantidade(int quantidade)
    {
        if (quantidade > 0 && quantidade <= Quantidade)
        {
            Quantidade -= quantidade;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Código: {Codigo}, Nome: {Nome}, Marca: {Marca}, Categoria: {Categoria}, Preço: {Preco:C}, Quantidade em Estoque: {Quantidade}";
    }
}

public class Estoque
{
    private List<Produto> produtos = new List<Produto>();

    public void AdicionarProduto(Produto produto)
    {
        produtos.Add(produto);
        Console.WriteLine("Produto adicionado com sucesso.");
    }

    public void RemoverProduto(string codigo)
    {
        Produto produto = produtos.Find(p => p.Codigo == codigo);
        if (produto != null)
        {
            produtos.Remove(produto);
            Console.WriteLine("Produto removido com sucesso.");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }

    public void AtualizarQuantidade(string codigo, int quantidade, bool entrada = true)
    {
        Produto produto = produtos.Find(p => p.Codigo == codigo);
        if (produto != null)
        {
            if (entrada)
            {
                produto.AdicionarQuantidade(quantidade);
                Console.WriteLine("Entrada de estoque realizada com sucesso.");
            }
            else
            {
                if (produto.RemoverQuantidade(quantidade))
                {
                    Console.WriteLine("Saída de estoque realizada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Quantidade insuficiente em estoque.");
                }
            }
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }

    public Produto ConsultarProduto(string codigo)
    {
        return produtos.Find(p => p.Codigo == codigo);
    }

    public void ListarProdutos()
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto no estoque.");
        }
        else
        {
            Console.WriteLine("Produtos no estoque:");
            foreach (var produto in produtos)
            {
                Console.WriteLine(produto);
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Estoque estoque = new Estoque();
        MostrarMenu(estoque);
    }

    public static void MostrarMenu(Estoque estoque)
    {
        while (true)
        {
            Console.WriteLine("\n===> Controle de Estoque Kabum <===");
            Console.WriteLine("[1] Novo");
            Console.WriteLine("[2] Listar Produtos");
            Console.WriteLine("[3] Remover Produtos");
            Console.WriteLine("[4] Entrada Estoque");
            Console.WriteLine("[5] Saída Estoque");
            Console.WriteLine("[0] Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    AdicionarProduto(estoque);
                    break;
                case "2":
                    estoque.ListarProdutos();
                    break;
                case "3":
                    RemoverProduto(estoque);
                    break;
                case "4":
                    EntradaEstoque(estoque);
                    break;
                case "5":
                    SaidaEstoque(estoque);
                    break;
                case "0":
                    Console.WriteLine("Saindo do sistema...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    public static void AdicionarProduto(Estoque estoque)
    {
        Console.Write("Digite o nome do produto: ");
        string nome = Console.ReadLine();

        Console.Write("Digite o preço do produto: ");
        decimal preco = decimal.Parse(Console.ReadLine());

        Console.Write("Digite a categoria do produto: ");
        string categoria = Console.ReadLine();

        Console.Write("Digite a marca do produto: ");
        string marca = Console.ReadLine();

        Console.Write("Digite o código do produto: ");
        string codigo = Console.ReadLine();

        Console.Write("Digite a quantidade inicial do produto: ");
        int quantidade = int.Parse(Console.ReadLine());

        Produto produto = new Produto(nome, preco, categoria, marca, codigo, quantidade);
        estoque.AdicionarProduto(produto);
    }

    public static void RemoverProduto(Estoque estoque)
    {
        Console.Write("Digite o código do produto que deseja remover: ");
        string codigo = Console.ReadLine();

        estoque.RemoverProduto(codigo);
    }

    public static void EntradaEstoque(Estoque estoque)
    {
        Console.Write("Digite o código do produto que deseja dar entrada no estoque: ");
        string codigo = Console.ReadLine();

        Console.Write("Digite a quantidade a adicionar: ");
        int quantidade = int.Parse(Console.ReadLine());

        estoque.AtualizarQuantidade(codigo, quantidade, true);
    }

    public static void SaidaEstoque(Estoque estoque)
    {
        Console.Write("Digite o código do produto que deseja dar saída no estoque: ");
        string codigo = Console.ReadLine();

        Console.Write("Digite a quantidade a remover: ");
        int quantidade = int.Parse(Console.ReadLine());

        estoque.AtualizarQuantidade(codigo, quantidade, false);
    }
}
