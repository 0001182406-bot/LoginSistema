using System;

public class Usuario
{
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public string Senha { get; set; } = "";
    public DateTime DataNascimento { get; set; }

    public bool VerificarLogin(string senha, DateTime dataNascimento)
    {
        return Senha == senha && DataNascimento.Date == dataNascimento.Date;
    }
}

public class TelaLogin
{
    private Usuario usuario;
    private int tentativasRestantes = 2;
    
    public TelaLogin(Usuario usuario)
    {
        this.usuario = usuario;
    }
    
    public bool Login(string senha, DateTime dataNascimento)
    {
        if (tentativasRestantes > 0)
        {
            if (usuario.VerificarLogin(senha, dataNascimento))
            {
                Console.WriteLine($"Login bem-sucedido! Bem-vindo, {usuario.Nome}!");
                tentativasRestantes = 2;
                return true;
            }
            else
            {
                tentativasRestantes--;
                Console.WriteLine($"Login inválido! Tentativas restantes: {tentativasRestantes}");
                
                if (tentativasRestantes == 0)
                {
                    Console.WriteLine("\nNúmero máximo de tentativas atingido!");
                    RecuperacaoDeAcesso();
                }
                return false;
            }
        }
        else
        {
            Console.WriteLine("\nNúmero máximo de tentativas atingido!");
            RecuperacaoDeAcesso();
            return false;
        }
    }
    
    public void RecuperacaoDeAcesso()
    {
        Console.WriteLine("\n=== RECUPERAÇÃO DE SENHA ===");
        Console.WriteLine($"Enviando e-mail para: {usuario.Email}");
        Console.WriteLine("E-mail simulado enviado com sucesso!");
        Console.WriteLine("Link: http://seusistema.com/recuperar-senha");
        Console.WriteLine("Tentativas resetadas para 2.\n");
        tentativasRestantes = 2;
    }
    
    public int GetTentativas()
    {
        return tentativasRestantes;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SISTEMA DE LOGIN ===");
        
        Usuario usuario = new Usuario
        {
            Nome = "Pablo",
            Email = "Pablo@email.com",
            Senha = "654321",
            DataNascimento = new DateTime(2000, 1, 1)
        };
        
        TelaLogin tela = new TelaLogin(usuario);
        
        Console.WriteLine("\n💡-- TESTE O SISTEMA:");
        Console.WriteLine($"- Senha correta: 654321");
        Console.WriteLine($"- Data correta: 08/08/2007");
        Console.WriteLine($"- Você tem {tela.GetTentativas()} tentativa(s) inicialmente");
        Console.WriteLine($"- 2 erros, a recuperação será ativada automaticamente");
        
        while (true)
        {
            Console.WriteLine($"\n-Tentativas disponíveis: {tela.GetTentativas()}");
            Console.Write("-Digite a senha: ");
            string senha = Console.ReadLine() ?? "";
            
            Console.Write("-Digite a data (dd/mm/aaaa): ");
            
            if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                bool resultado = tela.Login(senha, data);
                Console.WriteLine(resultado ? " ACESSO PERMITIDO!" : " ACESSO NEGADO!");
            }
            else
            {
                Console.WriteLine(" Data inválida!");
            }

            
            Console.Write("\nContinuar? (S/N): ");
            if ((Console.ReadLine() ?? "").ToUpper() != "S") break;
        }
        
        Console.WriteLine("\n--- FIM --");
    }
}