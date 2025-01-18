using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Hits.Services.Dapper;

/// <summary>
///     Class to perform SQL transactions using Dapper as a micro ORM
/// </summary>
public static class Transact
{
    /// <summary>
    ///     Query data using a stored procedure, returning results in a list of T
    /// </summary>
    /// <typeparam name="T">Results of query in a list of T</typeparam>
    /// <typeparam name="TU">Object type for parameters</typeparam>
    /// <param name="storedProcedure">Fully qualified stored procedure name</param>
    /// <param name="parameters">Set of query parameters for transaction, referenced for returned output</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static List<T> QueryData<T, TU>(string storedProcedure, ref TU parameters, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
    }

    /// <summary>
    ///     Query data, returning results in a list of T asynchronously
    /// </summary>
    /// <typeparam name="T">Results of query in a list of T</typeparam>
    /// <typeparam name="U">Object type for parameters</typeparam>
    /// <param name="storedProcedure">Fully qualified stored procedure name</param>
    /// <param name="parameters">Set of query parameters for transaction</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static async Task<List<T>> QueryDataAsync<T, U>(string storedProcedure, U parameters, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return (List<T>)await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    ///     Query data, returning results in a list of T
    /// </summary>
    /// <typeparam name="T">Results of query in a list of T</typeparam>
    /// <param name="query">SQL query</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static List<T> QueryData<T>(string query, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return (List<T>)connection.Query<T>(query);
    }

    /// <summary>
    ///     Query data, returning results in a list of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static List<T> QueryData<T>(string query, IDbTransaction transaction) => transaction.Connection == null
        ? throw new ArgumentNullException("Transaction Connection")
        : (List<T>)transaction.Connection.Query<T>(query, transaction: transaction, commandType: CommandType.Text);

    /// <summary>
    ///     Query db with no returned values
    /// </summary>
    /// <param name="query"></param>
    /// <param name="connSql"></param>
    public static void Query(string query, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        _ = connection.Query(query);
    }

    /// <summary>
    ///     Query db with no returned values asynchronously
    /// </summary>
    /// <param name="query"></param>
    /// <param name="connSql"></param>
    public static async Task QueryAsync(string query, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        _ = await connection.QueryAsync(query);
    }

    /// <summary>
    ///     Query db with no returned values asynchronously. This method uses a supplied IDbConnection,mainly for use in a
    ///     transaction
    /// </summary>
    /// <param name="query"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task QueryAsync(string query, IDbTransaction transaction)
    {
        if (transaction.Connection == null) throw new ArgumentNullException("Transaction.Connection");
        var conn = transaction.Connection;
        _ = await transaction.Connection.QueryAsync(query, transaction: transaction, commandType: CommandType.Text);
    }

    /// <summary>
    ///     Query data, returning results in a list of T asynchronously
    /// </summary>
    /// <typeparam name="T">Results of query in a list of T</typeparam>
    /// <param name="query">SQL query</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static async Task<List<T>> QueryDataAsync<T>(string query, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return (List<T>)await connection.QueryAsync<T>(query);
    }

    /// <summary>
    ///     Executes a stored procedure, returning results in a list of T
    /// </summary>
    /// <typeparam name="T">Results of query in type T</typeparam>
    /// <typeparam name="TU">Object type for parameters</typeparam>
    /// <param name="storedProcedure">Fully qualified stored procedure name</param>
    /// <param name="parameters">Set of query parameters for transaction, referenced for returned output</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static T? ExecuteScalar<T, TU>(string storedProcedure, ref TU parameters, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return connection.ExecuteScalar<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    ///     Executes a stored procedure that returns a scalar value as T
    /// </summary>
    /// <typeparam name="T">Results of query in type T</typeparam>
    /// <typeparam name="TU">Object type for parameters</typeparam>
    /// <param name="storedProcedure">Fully qualified stored procedure name</param>
    /// <param name="parameters">Set of query parameters for transaction</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static async Task<T?> ExecuteScalarAsync<T, TU>(string storedProcedure, TU parameters, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return await connection.ExecuteScalarAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    ///     Queries a scalar value as T
    /// </summary>
    /// <typeparam name="T">Results of query in type T</typeparam>
    /// <param name="query">SQL query</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static T? ExecuteScalar<T>(string query, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return connection.ExecuteScalar<T>(query);
    }

    /// <summary>
    ///     Queries a scalar value, returned result of T asynchronously
    /// </summary>
    /// <typeparam name="T">Results of query in type T</typeparam>
    /// <param name="query">SQL query</param>
    /// <param name="connSql">Connection string</param>
    /// <returns></returns>
    public static async Task<T?> ExecuteScalarAsync<T>(string query, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return await connection.ExecuteScalarAsync<T>(query);
    }

    /// <summary>
    ///     Executes a stored procedure with no returned results
    /// </summary>
    /// <typeparam name="T">Object type for parameters</typeparam>
    /// <param name="storedProcedure">Fully qualified stored procedure name</param>
    /// <param name="parameters">Set of query parameters for transaction, referenced for returned output</param>
    /// <param name="connSql">Connection string</param>
    public static void ExecuteProcedure<T>(string storedProcedure, ref T parameters, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        _ = connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    ///     Executes a stored procedure with no returned results  asynchronously
    /// </summary>
    /// <typeparam name="T">Object type for parameters</typeparam>
    /// <param name="storedProcedure">Fully qualified stored procedure name</param>
    /// <param name="parameters">Set of query parameters for transaction</param>
    /// <param name="connSql">Connection string</param>
    public static async Task<int> ExecuteProcedureAsync<T>(string storedProcedure, T parameters, string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    ///     Executes a stored procedure with no returned results  asynchronously. This method uses a supplied IDbConnection,
    ///     mainly for use in a transaction
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storedProcedure"></param>
    /// <param name="parameters"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task<int> ExecuteProcedureAsync<T>(string storedProcedure, T parameters,
        IDbTransaction transaction)
    {
        if (transaction.Connection == null) throw new ArgumentNullException("Transaction Connection");
        var conn = transaction.Connection;
        return await conn.ExecuteAsync(storedProcedure, parameters, transaction,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    ///     Return the database name from connection string
    /// </summary>
    /// <param name="connSql">Connection string</param>
    /// <returns>Then name of the database</returns>
    public static string CurrentDatabaseName(string connSql)
    {
        using IDbConnection connection = new SqlConnection(connSql);
        return connection.Database;
    }
}