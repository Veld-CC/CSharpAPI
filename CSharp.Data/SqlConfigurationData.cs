using CSharp.Models;
using System.Data.SqlClient;

namespace GSM.Data
{
    public class SqlConfigurationData
    {
        private List<Database> _connectionsStrings { get; set; }

        public string ConnectionString { get; set; }
        public string Database2 { get; set; }
        public string Database3 { get; set; }
        

        public SqlConfigurationData(List<Database> _)
        {
            _connectionsStrings = _;

            // Se establece la cadena de conexion por defecto
            ConnectionString = _connectionsStrings.First(x => x.Name.Equals("AdventureConnection")).ConnectionString;
            Database2 = _connectionsStrings.First(x => x.Name.Equals("Database2")).ConnectionString;
            Database3 = _connectionsStrings.First(x => x.Name.Equals("Database3")).ConnectionString;

        }

        /// <summary>
        /// Obtiene una cadena de conexion en especifico
        /// </summary>
        /// <param name="name">Nombre de la cadena de conexión</param>
        /// <returns>Regresa el valor de la cadena de conexión</returns>
        private string GetConnectionString(string connectionName) =>
			_connectionsStrings.First(x => x.Name.Equals(connectionName)).ConnectionString;

		/// <summary>
		/// Conexión a la base de datos de acuerdo a una cadena de conexión establecida
		/// </summary>
		/// <returns>Retorna una instancia de la clase SqlConnection</returns>
		public SqlConnection DbConnection() => new(ConnectionString);

		/// <summary>
		/// Conexión a la base de datos de acuerdo a una cadena de conexión establecida
		/// </summary>
		/// <param name="connectionName">Nombre de la cadena de conexión</param>
		/// <returns>Retorna una instancia de la clase SqlConnection</returns>
		public SqlConnection DbConnection(string connectionName)
        {
            string conn = GetConnectionString(connectionName);
            return new SqlConnection(conn);
		}
	}
}
