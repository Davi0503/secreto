using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace testeConsole
{
    class Program
    {

        private static string connectionString = "Data Source=SQL5042.site4now.net;Initial Catalog=DB_A4C3B1_BlockTimeSenai;User Id=DB_A4C3B1_BlockTimeSenai_admin;Password=Da12Tava12;";

        static void Main(string[] args)
        {
            string userName = GetConfig("wmic computersystem get username");
            string cpu = GetConfig("wmic cpu get name");
            string capacidadeMemoria = GetConfig("wmic memorychip get capacity");
            string HDtipo = GetConfig("wmic diskdrive get mediatype");
            string HDtamanho = GetConfig("wmic diskdrive get size");
            string HDstatus = GetConfig("wmic diskdrive get status");
            string SerialBios = GetConfig("wmic bios get serialnumber");
            string ProductName = GetConfig("wmic csproduct get name");
            string ProductVendor = GetConfig("wmic csproduct get vendor");
            

            string query = @"INSERT INTO CONFIGURACOESCOMPUTADOR VALUES(@userName, @cpu, @capacidadeMemoria, @HDtipo, @HDtamanho, @HDstatus, @SerialBios, @ProductName, @ProductVendor);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@cpu", cpu);
                cmd.Parameters.AddWithValue("@capacidadeMemoria",capacidadeMemoria);
                cmd.Parameters.AddWithValue("@HDtipo", HDtipo);
                cmd.Parameters.AddWithValue("@HDtamanho", HDtamanho);
                cmd.Parameters.AddWithValue("@HDstatus", HDstatus);
                cmd.Parameters.AddWithValue("@SerialBios", SerialBios);
                cmd.Parameters.AddWithValue("@ProductName", ProductName);
                cmd.Parameters.AddWithValue("@ProductVendor", ProductVendor);
                cmd.ExecuteNonQuery();
            } 
        }

        private static string GetConfig(string component)
        {
            Process processo = new Process();
            processo.StartInfo.FileName = "cmd.exe";
            processo.StartInfo.CreateNoWindow = true;
            processo.StartInfo.RedirectStandardInput = true;
            processo.StartInfo.RedirectStandardOutput = true;
            processo.StartInfo.UseShellExecute = false;
            processo.Start();
            processo.StandardInput.WriteLine(component);
            processo.StandardInput.Flush();
            processo.StandardInput.Close();
            processo.WaitForExit();
            var name = processo.StandardOutput.ReadToEnd().Split("\n").ToList();
            var remove = name[5].Length - 4;
            return name[5].Remove(remove, 4);
        }
    }
}
