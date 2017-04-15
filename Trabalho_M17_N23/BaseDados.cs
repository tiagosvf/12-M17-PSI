using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Trabalho_M17_N23
{
    public class BaseDados
    {
        private static BaseDados instance;
        public static BaseDados Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaseDados();
                return instance;
            }
        }
        private string strLigacao;
        private SqlConnection ligacaoBD;
        public BaseDados()
        {
            //ligação à bd
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }
        ~BaseDados()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #region Funções genéricas
        //devolve consulta
        public DataTable devolveConsulta(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }
        public DataTable devolveConsulta(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }


        public DataTable devolveConsulta(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Transaction = transacao;
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        //executar comando
        public bool executaComando(string sql)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Debug.WriteLine(erro.Message);
                return false;
            }
            return true;
        }
        public bool executaComando(string sql, List<SqlParameter> parametros)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                //throw erro;
                return false;
            }
            return true;
        }
        public bool executaComando(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.Transaction = transacao;
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return false;
            }
            return true;
        }
        #endregion
        #region utilizadores
        public bool registarUtilizador(string email, string nome_proprio, string apelido, string morada, string codigo_postal, string password, int newsletter)
        {
            string sql = "INSERT INTO utilizadores(email,nome_proprio,apelido,morada,codigo_postal,password,estado,perfil, newsletter) ";
            sql += "VALUES (@email,@nome_proprio,@apelido,@morada,@codigo_postal,HASHBYTES('SHA2_512',@password),@estado,@perfil,@newsletter)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@nome_proprio",SqlDbType=SqlDbType.VarChar,Value=nome_proprio},
                new SqlParameter() {ParameterName="@apelido",SqlDbType=SqlDbType.VarChar,Value=apelido},
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=morada },
                new SqlParameter() {ParameterName="@codigo_postal",SqlDbType=SqlDbType.VarChar,Value=codigo_postal},
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password },
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=0 },
                new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.Int,Value=1 },
                  new SqlParameter() {ParameterName="@newsletter",SqlDbType=SqlDbType.Int,Value=newsletter }
            };
            return executaComando(sql, parametros);
        }
        public bool registarUtilizador(string email, string nome_proprio, string apelido, string morada, string codigo_postal, string password, int perfil, int newsletter)
        {

            string sql = "INSERT INTO utilizadores(email,nome_proprio,apelido,morada,codigo_postal,password,estado,perfil, newsletter) ";
            sql += "VALUES (@email,@nome_proprio,@apelido,@morada,@codigo_postal,HASHBYTES('SHA2_512',@password),@estado,@perfil,@newsletter)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                  new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@nome_proprio",SqlDbType=SqlDbType.VarChar,Value=nome_proprio},
                new SqlParameter() {ParameterName="@apelido",SqlDbType=SqlDbType.VarChar,Value=apelido},
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=morada },
                new SqlParameter() {ParameterName="@codigo_postal",SqlDbType=SqlDbType.VarChar,Value=codigo_postal},
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password },
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=0 },
                new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.Int,Value=perfil },
                 new SqlParameter() {ParameterName="@newsletter",SqlDbType=SqlDbType.Int,Value=newsletter }
            };
            return executaComando(sql, parametros);
        }
        public bool registarUtilizador(string email, string nome_proprio, string apelido, string morada, string codigo_postal, string password, int estado, int perfil, int newsletter)
        {
            string sql = "INSERT INTO utilizadores(email,nome_proprio,apelido,morada,codigo_postal,password,estado,perfil,newsletter) ";
            sql += "VALUES (@email,@nome_proprio,@apelido,@morada,@codigo_postal,HASHBYTES('SHA2_512',@password),@estado,@perfil,@newsletter)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                  new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@nome_proprio",SqlDbType=SqlDbType.VarChar,Value=nome_proprio},
                new SqlParameter() {ParameterName="@apelido",SqlDbType=SqlDbType.VarChar,Value=apelido},
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=morada },
                new SqlParameter() {ParameterName="@codigo_postal",SqlDbType=SqlDbType.VarChar,Value=codigo_postal},
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password },
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=estado },
                new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.Int,Value=perfil },
                 new SqlParameter() {ParameterName="@newsletter",SqlDbType=SqlDbType.Int,Value=newsletter }
            };
            return executaComando(sql, parametros);
        }
        public void atualizarUtilizador(int id, string nome_proprio, string apelido,int estado,int perfil, string morada, string codigo_postal)
        {
            string sql = @"UPDATE utilizadores SET nome_proprio=@nome_proprio,estado=@estado, perfil=@perfil, apelido=@apelido,morada=@morada,codigo_postal=@codigo_postal
                            WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id},
                new SqlParameter() {ParameterName="@nome_proprio",SqlDbType=SqlDbType.VarChar,Value=nome_proprio},
                new SqlParameter() {ParameterName="@apelido",SqlDbType=SqlDbType.VarChar,Value=apelido},
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=morada },
                new SqlParameter() {ParameterName="@codigo_postal",SqlDbType=SqlDbType.VarChar,Value=codigo_postal},
                   new SqlParameter() {ParameterName="@perfil",SqlDbType=SqlDbType.Int,Value=perfil},
                      new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=estado},
            };
            executaComando(sql, parametros);
        } ////////////////////////////////////////////////////CONTINUAR DAQUI///////////////////////////////////////////////
        public DataTable devolveDadosUtilizador(int id)
        {
            string sql = "SELECT * FROM utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        public int estadoUtilizador(int id)
        {
            string sql = "SELECT estado FROM utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return int.Parse(dados.Rows[0][0].ToString());
        }
        public void ativarDesativarUtilizador(int id)
        {
            int estado = estadoUtilizador(id);
            if (estado == 0) estado = 1;
            else estado = 0;
            string sql = "UPDATE utilizadores SET estado = @estado WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=estado },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            executaComando(sql, parametros);
        }
        public DataTable verificarLogin(string email, string password)
        {
            string sql = "SELECT * FROM Utilizadores WHERE email=@email AND ";
            sql += "password=HASHBYTES('SHA2_512',@password) AND estado=0";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password }
            };
            DataTable utilizador = devolveConsulta(sql, parametros);
            if (utilizador == null || utilizador.Rows.Count == 0)
                return null;
            string id = utilizador.Rows[0]["id"].ToString();
            sql = "UPDATE Utilizadores SET [online]=1 WHERE id=" + id;
            executaComando(sql);
            return utilizador;
        }

        public DataTable listaUtilizadoresDisponiveis()
        {
            string sql = "SELECT id, nome_proprio + ' '+ apelido AS Nome, email, estado, codigo_postal, morada, perfil FROM utilizadores where perfil=1";
            return devolveConsulta(sql);
        }
        public DataTable listaTodosUtilizadores()
        {
            string sql = "SELECT id, nome_proprio + ' '+ apelido AS Nome, email, estado, codigo_postal, morada, perfil,newsletter FROM utilizadores";
            return devolveConsulta(sql);
        }
        public bool removerUtilizador(int id)
        {
            string sql = "DELETE FROM Utilizadores WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value= id},
            };
            return executaComando(sql, parametros);
        }
        public void recuperarPassword(string email, string guid)
        {
            string sql = "UPDATE utilizadores set lnkRecuperar=@lnk WHERE email=@email";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            executaComando(sql, parametros);
        }

        public void editarDadosCliente(int id, string nome, string apelido, string codigo_postal, string morada)
        {
            
            string sql = "UPDATE utilizadores set nome=@nome, apelido=@apelido, codigo_postal=@codigo_postal, morada=@morada WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=nome },
                new SqlParameter() {ParameterName="@apelido",SqlDbType=SqlDbType.VarChar,Value=apelido },
                new SqlParameter() {ParameterName="@codigo_postal",SqlDbType=SqlDbType.VarChar,Value=codigo_postal },
                  new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=morada },
                    new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
            };
            executaComando(sql, parametros);
        }
        public void atualizarPassword(string guid, string password)
        {
            string sql = "UPDATE utilizadores set password=HASHBYTES('SHA2_512',@password),estado=1,lnkRecuperar=null WHERE lnkRecuperar=@lnk";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password},
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            executaComando(sql, parametros);
        }
        public DataTable devolveDadosUtilizador(string email)
        {
            string sql = "SELECT * FROM utilizadores WHERE email=@email";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email }
            };
            DataTable dados = devolveConsulta(sql, parametros);
            return dados;
        }
        #endregion

        #region produtos
        public DataTable pesquisaProdutosPeloNome(String strain)
        {

            string sql = "SELECT id,strain,preco,avaliacao,sativa_indica FROM Produtos WHERE strain like @strain OR sativa_indica like @strain AND stock>0";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@strain",SqlDbType=SqlDbType.Text,Value="%"+strain+"%"}
            };
            return devolveConsulta(sql, parametros);
        }
        public void aumentarQuantidadeCarrinho(int id_produto, int id_utilizador, int quantidade)
        {
        

            string sql = " UPDATE Carrinhos set quantidade=quantidade+@quantidade WHERE produto=@produto AND utilizador=@utilizador";
            List<SqlParameter>  parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@produto",SqlDbType=SqlDbType.Int,Value=id_produto},
                  new SqlParameter() {ParameterName="@utilizador",SqlDbType=SqlDbType.Int,Value=id_utilizador},
                    new SqlParameter() {ParameterName="@quantidade",SqlDbType=SqlDbType.Int,Value=quantidade}
            };
            executaComando(sql, parametros);
        }

        public void diminuirStock(int id_produto,int quantidade)
        {


            string sql = " UPDATE Produtos set stock=stock-@quantidade WHERE id=@produto";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@produto",SqlDbType=SqlDbType.Int,Value=id_produto},
                  new SqlParameter() {ParameterName="@quantidade",SqlDbType=SqlDbType.Int,Value=quantidade},
          
            };
            executaComando(sql, parametros);
        }

        public void removerDoCarrinho(int id_produto, int id_utilizador)
        {
            string sql = "Delete FROm CARRINHOS WHERE produto=@produto AND utilizador=@utilizador";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@produto",SqlDbType=SqlDbType.Int,Value=id_produto},
                  new SqlParameter() {ParameterName="@utilizador",SqlDbType=SqlDbType.Int,Value=id_utilizador},
                    
            };
            executaComando(sql, parametros);
        }
        public void adicionarAoCarrinho(int id_produto, int id_utilizador, int quantidade)
        {
            string sql = "Insert INTO CARRINHOS (produto, utilizador, quantidade) Values(@produto, @utilizador, @quantidade)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@produto",SqlDbType=SqlDbType.Int,Value=id_produto},
                  new SqlParameter() {ParameterName="@utilizador",SqlDbType=SqlDbType.Int,Value=id_utilizador},
                    new SqlParameter() {ParameterName="@quantidade",SqlDbType=SqlDbType.Int,Value=quantidade}
            };
            executaComando(sql, parametros);
        }
        public DataTable listaProdutos()
        {
            string sql = "SELECT * FROM Produtos";
            return devolveConsulta(sql);
        }
        public DataTable listaProdutosDisponiveis()
        {
            string sql = "SELECT * FROM Produtos WHERE stock>0";
            return devolveConsulta(sql);
        }
        public DataTable devolveDadosProdutos(int id)
        {
            string sql = "SELECT * FROM Produtos WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            return devolveConsulta(sql, parametros);
        }
        public DataTable devolveDadosProdutosCarrinho(int id)
        {
            string sql = "SELECT Produtos.id, Produtos.Strain, Produtos.Preco, Carrinhos.Quantidade FROM Produtos INNER JOIN Carrinhos ON produtos.id = carrinhos.produto WHERE Carrinhos.utilizador = @id ";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            return devolveConsulta(sql, parametros);
        }
        public DataTable listaProdutosComPrecoInferior(int  id)
        {
            string sql = "SELECT * FROM Produtos where preco<=(select preco from produtos where id=@id)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}
            };
            return devolveConsulta(sql, parametros);
        }
        public int adicionarProduto(string strain, string sativa_ou_indica, int percentagem_sativa, bool feminizada, bool automatica, int stock, int percentagem_thc, decimal preco)
        {
            string sql = "INSERT INTO Produtos (strain,sativa_indica,percentagem_sativa,feminizada,automatica, stock, percentagem_thc, preco) VALUES ";
            sql += "(@strain,@sativa_indica,@percentagem_sativa,@feminizada,@automatica,@stock,@percentagem_thc,@preco);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@strain",SqlDbType=SqlDbType.VarChar,Value= strain},
                new SqlParameter() {ParameterName="@sativa_indica",SqlDbType=SqlDbType.VarChar,Value= sativa_ou_indica},
                new SqlParameter() {ParameterName="@percentagem_sativa",SqlDbType=SqlDbType.Int,Value= percentagem_sativa},
                new SqlParameter() {ParameterName="@percentagem_thc",SqlDbType=SqlDbType.Int,Value= percentagem_thc},
                new SqlParameter() {ParameterName="@feminizada",SqlDbType=SqlDbType.Bit,Value= feminizada},
                 new SqlParameter() {ParameterName="@automatica",SqlDbType=SqlDbType.Bit,Value= automatica},
                   new SqlParameter() {ParameterName="@stock",SqlDbType=SqlDbType.Int,Value= stock},
                     new SqlParameter() {ParameterName="@preco",SqlDbType=SqlDbType.Decimal,Value= preco}
            };
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id;
        }

        public void removerProduto(int id)
        {
            string sql = "DELETE FROM Produtos WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id}
            };
            executaComando(sql, parametros);
        }
        public void atualizaProduto(int id, string strain, string sativa_ou_indica, int percentagem_sativa, bool feminizada, bool automatica, int stock, int percentagem_thc, decimal preco)
        {
            string sql = "UPDATE Produtos SET strain=@strain,sativa_indica=@sativa_indica,percentagem_sativa=@percentagem_sativa,feminizada=@feminizada, automatica=@automatica, stock=@stock, percentagem_thc=@percentagem_thc,preco=@preco";
            sql += " WHERE id=@id;";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                   new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value= id},
                    new SqlParameter() {ParameterName="@strain",SqlDbType=SqlDbType.VarChar,Value= strain},
                new SqlParameter() {ParameterName="@sativa_indica",SqlDbType=SqlDbType.VarChar,Value= sativa_ou_indica},
                new SqlParameter() {ParameterName="@percentagem_sativa",SqlDbType=SqlDbType.Int,Value= percentagem_sativa},
                new SqlParameter() {ParameterName="@percentagem_thc",SqlDbType=SqlDbType.Int,Value= percentagem_thc},
                new SqlParameter() {ParameterName="@feminizada",SqlDbType=SqlDbType.Bit,Value= feminizada},
                 new SqlParameter() {ParameterName="@automatica",SqlDbType=SqlDbType.Bit,Value= automatica},
                   new SqlParameter() {ParameterName="@stock",SqlDbType=SqlDbType.Int,Value= stock},
                     new SqlParameter() {ParameterName="@preco",SqlDbType=SqlDbType.Decimal,Value= preco}
            };
            executaComando(sql, parametros);
        }
        #endregion
        #region vendas


        public int adicionarVenda(int id, string nome_proprio, string apelido, string morada, string codigo_postal)
        {
            string sql = "INSERT INTO Vendas(utilizador,nome_proprio,apelido,morada,codigo_postal,estado) ";
            sql += "VALUES (@id,@nome_proprio,@apelido,@morada,@codigo_postal,'Recebida');SELECT CAST(SCOPE_IDENTITY() AS INT);";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
                new SqlParameter() {ParameterName="@nome_proprio",SqlDbType=SqlDbType.VarChar,Value=nome_proprio},
                new SqlParameter() {ParameterName="@apelido",SqlDbType=SqlDbType.VarChar,Value=apelido},
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=morada },
                new SqlParameter() {ParameterName="@codigo_postal",SqlDbType=SqlDbType.VarChar,Value=codigo_postal}
            };
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id_venda = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id_venda;

        }

        public void adicionarProdutosVendas(int id_venda, int id_produto, int quantidade)
        {
            string sql = "INSERT INTO Produtos_vendas(venda,produto,quantidade) ";
            sql += "VALUES (@venda,@produto,@quantidade);";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@venda",SqlDbType=SqlDbType.Int,Value=id_venda },
                 new SqlParameter() {ParameterName="@produto",SqlDbType=SqlDbType.Int,Value=id_produto },
                 new SqlParameter() {ParameterName="@quantidade",SqlDbType=SqlDbType.Int,Value=quantidade },

            };
            executaComando(sql, parametros);


        }

        public DataTable devolveProdutosVenda(int id_venda)
        {
            string sql = "SELECT Produtos.id, Produtos.Strain, Produtos.Preco, Produtos_vendas.Quantidade FROM Produtos INNER JOIN Produtos_vendas ON produtos.id = produtos_vendas.produto WHERE produtos_vendas.venda = @id ";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id_venda }
            };
            return devolveConsulta(sql, parametros);
        }
        public DataTable listaTodasAsVendas()
        {
            string sql = "SELECT id,utilizador, nome_proprio + ' ' + apelido AS [Nome do destinatário], codigo_postal AS [Código-postal], morada AS Morada, estado AS Estado, total AS [Valor total] FROM Vendas ";
          
            return devolveConsulta(sql);
        }

        public DataTable listaVendasUtilizador(int id_utilizador)
        {
            string sql = "SELECT id, nome_proprio + ' ' + apelido AS [Nome do destinatário], codigo_postal AS [Código-postal], morada AS Morada, estado AS Estado, total AS [Valor total] FROM Vendas WHERE utilizador=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id_utilizador }
            };
            return devolveConsulta(sql, parametros);
        }
        public DataTable listaDadosVenda(int id_venda)
        {
            string sql = "SELECT * FROM Vendas WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id_venda }
            };
            return devolveConsulta(sql, parametros);
        }


        public void adicionarPrecoVenda(int id_venda, float total)
        {
            string sql = "Update vendas set total=@total WHERE id=@venda";
           

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@venda",SqlDbType=SqlDbType.Int,Value=id_venda },
                   new SqlParameter() {ParameterName="@total",SqlDbType=SqlDbType.Float,Value=total },


            };
            executaComando(sql, parametros);


        }

        public void removerCarrinho(int id_utilizador)
        {
            string sql = "DELETE From Carrinhos WHERE utilizador=@utilizador";


            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName="@utilizador",SqlDbType=SqlDbType.Int,Value=id_utilizador },
                


            };
            executaComando(sql, parametros);


        }



        #endregion
        #region comentarios
        public bool adicionarComentario(int produto, string comentario, int avaliacao, int utilizador)
        {
            DataTable avaliacoes = BaseDados.Instance.devolveConsulta("SELECT avaliacao FROM comentarios WHERE produto="+produto);
            int total = avaliacao;
            decimal avaliacao_nova = 0;
            for (int i = 0; i < avaliacoes.Rows.Count; i++)
            {
                total += int.Parse(avaliacoes.Rows[i][0].ToString());
            }
            avaliacao_nova = total / (avaliacoes.Rows.Count+1);

            string sqlx = "UPDATE Produtos set avaliacao=@avaliacao";

            List<SqlParameter> parametrosx = new List<SqlParameter>()
            {
              
                    new SqlParameter() {ParameterName="@avaliacao",SqlDbType=SqlDbType.Decimal,Value=avaliacao_nova },
            };
            executaComando(sqlx, parametrosx);

            string sql = "INSERT INTO Comentarios(produto,comentario,avaliacao,utilizador) ";
            sql += "VALUES (@produto,@comentario,@avaliacao,@utilizador)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@comentario",SqlDbType=SqlDbType.VarChar,Value=comentario },                
                new SqlParameter() {ParameterName="@avaliacao",SqlDbType=SqlDbType.Int,Value=avaliacao},
                new SqlParameter() {ParameterName="@produto",SqlDbType=SqlDbType.Int,Value=produto},
                 new SqlParameter() {ParameterName="@utilizador",SqlDbType=SqlDbType.Int,Value=utilizador}
            };
            return executaComando(sql, parametros);
        }

        public DataTable listaComentariosUtilizador(int utilizador)
        {
           
            string sql = "SELECT * from Comentarios Where utilizador=@utilizador";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
             
                 new SqlParameter() {ParameterName="@utilizador",SqlDbType=SqlDbType.Int,Value=utilizador}
            };
            return devolveConsulta(sql, parametros);
        }

        public DataTable verificaCompraProduto(int utilizador, int produto)
        {
            string sql = "SELECT vendas.utilizador, produtos_vendas.produto FROM Vendas INNER JOIN Produtos_vendas ON vendas.id=produtos_vendas.venda WHERE produtos_vendas.produto=@produto AND vendas.utilizador=@utilizador";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {

                 new SqlParameter() {ParameterName="@utilizador",SqlDbType=SqlDbType.Int,Value=utilizador},


                 new SqlParameter() {ParameterName="@produto",SqlDbType=SqlDbType.Int,Value=produto}
            };
            return devolveConsulta(sql, parametros);
        }

        #endregion

        #region newsletter
       

        public DataTable listaEmailsNewsletter()
        {

            string sql = "SELECT email FROM Utilizadores where newsletter=1";
            
            return devolveConsulta(sql);
        }

       
        #endregion
    }
}