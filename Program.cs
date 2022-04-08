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

            Console.WriteLine("A quadra tem bancos? (S/N)");
            res = Console.ReadLine();
            if (res == "S" || res == "s") bancos = 1; else bancos = 0;

            Console.WriteLine("A quadra tem arquibancada? (S/N)");
            res = Console.ReadLine();
            if (res == "S" || res == "s") arquibancada = 1; else arquibancada = 0;

            string sql = "INSERT INTO QUADRAS (NOME, TIPO, COBERTA, BANCOS, ARQUIBANCADA, BLOQUEADA, EM_USO) "
            + "VALUES ('" + nome + "', '" + tipo + "', '" + coberta + "', '" + bancos + "', '" + arquibancada + "', 0, 0 )";

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Quadra Cadastrada com sucesso");
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
                    int data0 = reader.GetInt32(0);
                    string data1 = reader.GetString(1);
                    string data2 = reader.GetString(2);
                    bool data3 = reader.GetBoolean(3);
                    bool data4 = reader.GetBoolean(4);
                    bool data5 = reader.GetBoolean(5);
                    bool data6 = reader.GetBoolean(6);
                    bool data7 = reader.GetBoolean(7);
                    Console.WriteLine();
                    Console.WriteLine(data0);
                    Console.WriteLine(data1);
                    Console.WriteLine(data2);
                    Console.WriteLine(data3 == true ? "É Coberta" : "Nao é coberta");
                    Console.WriteLine(data4 == true ? "Tem bancos" : "Nao tem bancos");
                    Console.WriteLine(data5 == true ? "Tem arquibancada" : "Nao tem arquibancada");
                    Console.WriteLine(data6 == true ? "Bloqueada" : "Liberada");
                    Console.WriteLine(data7 == true ? "Em Uso" : "Disponivel");
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
        
        public static void CriarReserva(string conString)
        {
            Console.Clear();
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
                    int data0 = reader.GetInt32(0);
                    string data1 = reader.GetString(1);
                    Console.WriteLine();
                    Console.WriteLine("Quadra nº "+data0+": "+data1);
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

            int pago;
            string res;
            Console.WriteLine("\nCriar Reserva de Quadra");
            Console.WriteLine("Digite o ID da quadra:");
            string idQuad = Console.ReadLine();

            Console.WriteLine("Digite a data e hora de inicio:");
            string dateInicio = Console.ReadLine();

            Console.WriteLine("Digite a data e hora de termino:");
            string dateTermino = Console.ReadLine();

            Console.WriteLine("Digite o nome do cliente:");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o CPF do cliente:");
            string cpf = Console.ReadLine();

            Console.WriteLine("A reserva vai ser paga agora por PIX?");
            res = Console.ReadLine();
            if (res == "S" || res == "s") pago = 1; else pago = 0;

            Console.WriteLine("");

            sql = "INSERT INTO RESERVAS (DATA_INICIO, DATA_TERMINO, NOME_CLIENTE, CPF, PAGO, ID_QUADRA) "
            + "VALUES ('" + dateInicio + "', '" + dateTermino + "', '" + nomeCliente + "', '" + cpf + "', '" + pago + "', " + idQuad + ")";

            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Reserva gerada com sucesso");
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
        
        public static void VisualizarReserva(string conString)
        {
            string sql = "SELECT * FROM RESERVAS";

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
                    string data3 = reader.GetString(3);
                    string data4 = reader.GetString(4);
                    bool data5 = reader.GetBoolean(5);
                    int data6 = reader.GetInt32(6);
                    Console.WriteLine();
                    Console.WriteLine(data1);
                    Console.WriteLine(data2);
                    Console.WriteLine(data3);
                    Console.WriteLine(data4);
                    Console.WriteLine(data5 == true ? "Foi pago antecipadamente" : "Nao foi pago antecipadamente");
                    Console.WriteLine(data6);
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
            CadastrarQuadra = 1,
            VisualizarQuadra = 2,
            CriarReserva = 3,
            VisualizarReservas = 4,
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
                    Console.WriteLine("3 - Reservar Quadra;");
                    Console.WriteLine("4 - Visualizar Reservas");
                    
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
                            case Menu.CadastrarQuadra:
                                Program.CadastrarQuadra(conString);
                                Console.ReadLine();
                                break;
                            case Menu.VisualizarQuadra:
                                Program.VisualizarQuadras(conString);
                                Console.ReadLine();
                                break;
                            case Menu.CriarReserva:
                                Program.CriarReserva(conString);
                                Console.ReadLine();
                                break;
                            case Menu.VisualizarReservas:
                                Program.VisualizarReserva(conString);
                                Console.ReadLine();
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
