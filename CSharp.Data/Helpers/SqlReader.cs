using System.Data.SqlClient;

namespace CSharp.Data.Helpers
{
    /// <summary>
    /// Esta clase es una clase genérica que recibe como tipo de parámetro la entidad que deberá deserializar en el reader,
    /// utilizando reflexión para deserializar las propiedades de la entidad que se encuentran en el reader.
    /// Para usar esta clase, simplemente creas una instancia pasando el tipo de entidad que esperas como primer parámetro, y luego
    /// llamas al método Read() para obtener una lista con los resultados. Por ejemplo:
    /// 
    ///     using SqlDataReader reader = await SqlHelper.ExecuteReaderAsync(DbConnection(), StoreProcedure, CommandType.StoredProcedure);
    ///     var reader = SqlReader<MyEntity>().Read(reader);
    /// 
    /// reemplazando MyEntity por el tipo de entidad que deseas deserializar.
    /// </summary>
    /// <typeparam name="T">Entidad que se desea deserializar.</typeparam>
    public static class SqlReader<T> where T : class, new()
    {
        public static IEnumerable<T> Read(SqlDataReader reader)
        {
            IEnumerable<int> columns = Enumerable.Range(0, reader.FieldCount);

            while (reader.Read())
            {
                var entity = Activator.CreateInstance<T>();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (columns.Any(i => reader.GetName(i).Equals(property.Name)))
                    {
                        var ordinal = reader.GetOrdinal(property.Name);
                        if (!reader.IsDBNull(ordinal))
                        {
                            property.SetValue(entity, reader.GetValue(ordinal));
                        }
                    }
                }
                yield return entity;
            }
        }
    }
}
