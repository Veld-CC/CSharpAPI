using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Data.Helpers
{
    public class SqlHelper
    {
        /// <summary>
        /// Set the connection, command, and then execute the command with non query.
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the SQL statement. Otherwise -1</returns>
        public async static Task<int> ExecuteNonQueryAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] parameters)
        {
            using SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            // There're three command types: StoredProcedure, Text, TableDirect.
            // The TableDirect type is only for OLE DB.
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Set the connection, command, and then execute the command with non query.
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the SQL statement. Otherwise -1</returns>
        public async static Task<int> ExecuteNonQueryAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, object? parameters = null)
        {
            using SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            // There're three command types: StoredProcedure, Text, TableDirect.
            // The TableDirect type is only for OLE DB.
            cmd.CommandType = commandType;
            parameters?.GetType().GetProperties().ToList().ForEach(property =>
                cmd.Parameters.AddWithValue(property.Name, property.GetValue(parameters)));

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Set the connection, command, and then execute the command with non query (with transaction).
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the SQL statement. Otherwise -1</returns>
        public async static Task<int> ExecuteNonQueryTransactionAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] parameters)
        {
            using SqlConnection conn = connection;
            conn.Open();

            // Start a local transaction.
            SqlTransaction transaction = connection.BeginTransaction();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            using SqlCommand cmd = new(commandText, conn, transaction);

            // There're three command types: StoredProcedure, Text, TableDirect.
            // The TableDirect type is only for OLE DB.
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            try
            {
                var result = await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw new Exception(ex2.Message);
                }
            }
        }

        /// <summary>
        /// Set the connection, command, and then execute the command with non query (with transaction).
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the SQL statement. Otherwise -1</returns>
        public async static Task<int> ExecuteNonQueryTransactionAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, object? parameters = null)
        {
            using SqlConnection conn = connection;
            conn.Open();

            // Start a local transaction.
            SqlTransaction transaction = connection.BeginTransaction();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            using SqlCommand cmd = new(commandText, conn, transaction);

            // There're three command types: StoredProcedure, Text, TableDirect.
            // The TableDirect type is only for OLE DB.
            cmd.CommandType = commandType;
            parameters?.GetType().GetProperties().ToList().ForEach(property =>
                cmd.Parameters.AddWithValue(property.Name, property.GetValue(parameters)));

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            try
            {
                var result = await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw new Exception(ex2.Message);
                }
            }
        }

        /// <summary>
        /// Set the connection, command, and then execute the command and only return one value. 
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns></returns>        
        public async static Task<object> ExecuteScalarAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] parameters)
        {
            using SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            await conn.OpenAsync();
            return await cmd.ExecuteScalarAsync();
        }

        /// <summary>
        /// Set the connection, command, and then execute the command and only return one value. 
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns></returns>        
        public async static Task<object> ExecuteScalarAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, object? parameters = null)
        {
            using SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            cmd.CommandType = commandType;
            parameters?.GetType().GetProperties().ToList().ForEach(property =>
                cmd.Parameters.AddWithValue(property.Name, property.GetValue(parameters)));

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            await conn.OpenAsync();
            return await cmd.ExecuteScalarAsync();
        }

        /// <summary>
        /// Set the connection, command, and then execute the command with query and return the reader. 
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns></returns>        
        public async static Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] parameters)
        {
            SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            await conn.OpenAsync();
            // When using CommandBehavior.CloseConnection, the connection will be closed when the   
            // IDataReader is closed.  
            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Set the connection, command, and then execute the command with query and return the reader. 
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns></returns>        
        public async static Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, object? parameters = null)
        {
            SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            cmd.CommandType = commandType;
            parameters?.GetType().GetProperties().ToList().ForEach(property =>
                cmd.Parameters.AddWithValue(property.Name, property.GetValue(parameters)));

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            await conn.OpenAsync();
            // When using CommandBehavior.CloseConnection, the connection will be closed when the   
            // IDataReader is closed.  
            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Set the connection, command, and then execute the command with query and return the reader. 
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns></returns>        
        public static SqlDataReader ExecuteReader(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[] parameters)
        {
            SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            conn.Open();
            // When using CommandBehavior.CloseConnection, the connection will be closed when the   
            // IDataReader is closed.  
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Set the connection, command, and then execute the command with query and return the reader. 
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="commandText">Query to execute</param>
        /// <param name="commandType">String command interpretation</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns></returns>        
        public static SqlDataReader ExecuteReader(SqlConnection connection, string commandText, CommandType commandType = CommandType.StoredProcedure, object? parameters = null)
        {
            SqlConnection conn = connection;
            using SqlCommand cmd = new(commandText, conn);
            cmd.CommandType = commandType;
            parameters?.GetType().GetProperties().ToList().ForEach(property =>
                cmd.Parameters.AddWithValue(property.Name, property.GetValue(parameters)));

            // Setting command timeout to 10 minutes
            cmd.CommandTimeout = 600;

            conn.Open();

            // When using CommandBehavior.CloseConnection, the connection will be closed when the   
            // IDataReader is closed.  
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public async static Task<bool> BulkInsertAsync(SqlConnection connection, string TableName, List<string> TableColumns, DataTable dataTable)
        {
            using SqlConnection conn = connection;
            await conn.OpenAsync();

            // Start a local transaction.
            SqlTransaction transaction = (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                using SqlBulkCopy objbulk = new(conn, SqlBulkCopyOptions.Default, transaction)
                {
                    DestinationTableName = TableName
                };

                foreach (string column in TableColumns)
                    objbulk.ColumnMappings.Add(column, column);

                await objbulk.WriteToServerAsync(dataTable);
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Attempt to roll back the transaction.
                try
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw new Exception(ex2.Message);
                }
            }
        }
    }
}
