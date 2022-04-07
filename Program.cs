using System;
using System.Data;
using System.Data.SqlClient;

namespace QuadrasApp
{
    internal class Program
    {
        public static void CadastrarQuadra(string conString)
        {
            int coberta, bancos, arquibancada;
            string res;
            Console.WriteLine("Cadastrar Quadra");
            Console.WriteLine("Digite o nome da quadra");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o tipo de quadra");
            string tipo = Console.ReadLine();

            Console.WriteLine("A quadra é coberta? (S/N)");
            res = Console.ReadLine();
            if (res == "S" || res == "s") coberta = 1; else coberta = 0;

            Console.WriteLine(coberta);
            Console.WriteLine("A quadra tem bancos? (S/N)");
            res = Console.ReadLine();
            if (res == "S" || res == "s") bancos = 1; else bancos = 0;

            Console.WriteLine("A quadra tem arquibancada? (S/N)");
            res = Console.ReadLine();
            if (res == "S" || res == "s") arquibancada = 1; else arquibancada = 0;

            string sql = "INSERT INTO QUADRAS (NOME, TIPO, COBERTA, BANCOS, ARQUIBANCADA, BLOQUEADA, EM_USO) "
            + "VALUES ('" + nome + "', '" + tipo + "', '" + coberta + "', '" + bancos + "', '" + arquibancada + "', 1, 1 )";

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Quadra Cadastrada com sucesso");
            } catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            } finally
            {
                con.Close();
            }
        }

        public static void VisualizarQuadras(string conString)
        {
            string sql = "SELECT * FROM QUADRAS";

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string data1 = reader.GetString(1);
                    string data2 = reader.GetString(2);
                    bool data3 = reader.GetBoolean(3);
                    bool data4 = reader.GetBoolean(4);
                    bool data5 = reader.GetBoolean(5);
                    bool data6 = reader.GetBoolean(6);
                    bool data7 = reader.GetBoolean(7);
                    Console.WriteLine(data1);
                    Console.WriteLine(data2);
                    Console.WriteLine(data3 == true ? "É Coberta" : "Nao é coberta" );
                    Console.WriteLine(data4 == true ? "Tem bancos" : "Nao tem bancos" );
                    Console.WriteLine(data5 == true ? "Tem arquibancada" : "Nao tem arquibancada" );
                    Console.WriteLine(data6 == true ? "Liberada" : "Bloqueada" );
                    Console.WriteLine(data7 == true ? "Disponivel" : "Em Uso" );
                    Console.WriteLine("-------------#-------------");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        enum Menu 
        { 
            Sair = 0,
            Cadastrar = 1,
            Visualizar = 2
        };

        static void Main(string[] args)
        {
            string conString = @"Data Source=DESKTOP-D1D6V76\SQLEXPRESS;Initial Catalog=BDQUADRAS;Integrated Security=True";

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                bool saida = false;
                while(saida == false)
                {
                    Console.WriteLine("Bem Vindo!");
                    Console.WriteLine("O que deseja fazer?\n");
                    Console.WriteLine("1 - Cadastrar Quadra;");
                    Console.WriteLine("2 - Visualizar Quadra;");
                    
                    Console.WriteLine("0- Sair.");

                    string escolhaStr = Console.ReadLine();
                    int escolhaInt = int.Parse(escolhaStr);

                    if (escolhaInt >= 0 && escolhaInt < 8)
                    {
                        Menu escolha = (Menu)escolhaInt;

                        switch (escolha)
                        {
                            case Menu.Sair:
                                saida = true;
                                break;
                            case Menu.Cadastrar:
                                Program.CadastrarQuadra(conString);
                                break;
                            case Menu.Visualizar:
                                Program.VisualizarQuadras(conString);
                                break;
                        }
                    }
                    else
                    {
                        saida = true;
                    }
                    Console.Clear();   
                }
            }
        }
    }
}
