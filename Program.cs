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

            Console.WriteLine("Digite a hora de inicio:");
            string dateInicio = Console.ReadLine();

            Console.WriteLine("Digite a hora de termino:");
            string dateTermino = Console.ReadLine();

            Console.WriteLine("Digite o nome do cliente:");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o CPF do cliente:");
            string cpf = Console.ReadLine();

            Console.WriteLine("A reserva vai ser paga agora por PIX? (S/N)");
            res = Console.ReadLine();
            if (res == "S" || res == "s") pago = 1; else pago = 0;

            Console.WriteLine("");

            sql = "INSERT INTO RESERVAS (NOME_CLIENTE, CPF, PAGO, ID_QUADRA) "
            + "VALUES ('" + nomeCliente + "', '" + cpf + "', '" + pago + "', " + idQuad + ")";

            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0) {
                    sql = "INSERT INTO HORARIOS (HORA_INICIO, HORA_TERMINO, ID_QUADRA) "
                    + "VALUES ('" + dateInicio + "', '" + dateTermino + "', " + idQuad + ")";

                    cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    
                    try
                    {

                        i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Console.WriteLine("Reserva gerada com sucesso");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.Message);
                    }
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
                    bool data3 = reader.GetBoolean(3);
                    int data4 = reader.GetInt32(4);
                    Console.WriteLine();
                    Console.WriteLine(data1);
                    Console.WriteLine(data2);
                    Console.WriteLine(data3 == true ? "Foi pago antecipadamente" : "Nao foi pago antecipadamente");
                    Console.WriteLine(data4);
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
      public static void VerificarHorarios(string conString)
        {
            Console.Clear();
            Console.WriteLine("\nVerificar horário das quadras");
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
                    Console.WriteLine("Quadra nº " + data0 + ": " + data1);
                    Console.WriteLine("-------------#-------------");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

            Console.WriteLine("Digite o ID da quadra:");
            string idQuad = Console.ReadLine();

            sql = "SELECT * FROM HORARIOS WHERE ID_QUADRA = " + idQuad;

            con = new SqlConnection(conString);
            cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int data1 = reader.GetInt32(1);
                    int data2 = reader.GetInt32(2);
                    int data3 = reader.GetInt32(3);
                    Console.WriteLine();
                    Console.WriteLine("Quadra ID: " + data3);
                    Console.WriteLine("Horário de entrada: " + data1);
                    Console.WriteLine("Horário de saída: " + data2);
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
            VerificarHorarios= 5
        };

        static void Main(string[] args)
        {
            string conString = @"Data Source=DESKTOP-A26DHLV;Initial Catalog=BDQUADRAS;Integrated Security=True";

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
                    Console.WriteLine("5 - Verificar Horários");

                    
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
                            case Menu.VerificarHorarios:
                                Program.VerificarHorarios(conString);
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
