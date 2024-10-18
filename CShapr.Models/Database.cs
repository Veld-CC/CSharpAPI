namespace CSharp.Models
{
	/// <summary>
	/// Clase para representar los nodos de la seccion ConecctionString
	/// </summary>
	public class Database
	{
		/// <summary>
		/// Nombre de la cadena de conexión
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Valor de la cadena de conexión
		/// </summary>
		public string ConnectionString { get; set; } = string.Empty;
	}
}
