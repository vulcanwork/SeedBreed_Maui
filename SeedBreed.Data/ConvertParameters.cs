using Dapper;
using System.Data;

namespace Hits.Services.Dapper;

/// <summary>
///     Converts model into parameters then executes transaction
/// </summary>
public static class ConvertParameters
{
    /// <summary>
    ///     Returns a string of just the name of the stored procedure, minus the schema
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    private static string StoredProcedure(string sp) => sp.IndexOf('.') > 0 ? sp[(sp.IndexOf('.') + 1)..] : sp;

    /// <summary>
    ///     Returns a list of specified class of T from a stored procedure
    /// </summary>
    /// <typeparam name="T">Class of parameters that the data will be returned in</typeparam>
    /// <param name="model">
    ///     Model containing parameter values for the stored procedure, supply null if no parameters are
    ///     required
    /// </param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <returns></returns>
    public static List<T> GetData<T>(T? model, string storedProcedure, string connSql)
    {
        var p = model is null
            ? new DynamicParameters()
            : GetClassParameters<T>(StoredProcedure(storedProcedure), model, connSql);
        return Transact.QueryData<T, DynamicParameters>(storedProcedure, ref p, connSql);
    }

    /// <summary>
    ///     Returns a list of specified class of T from a stored procedure asynchronously
    /// </summary>
    /// <typeparam name="T">Class of parameters that the data will be returned in</typeparam>
    /// <param name="model">
    ///     Model containing parameter values for the stored procedure, supply null if no parameters are
    ///     required
    /// </param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <returns></returns>
    public static async Task<List<T>> GetDataAsync<T>(T? model, string storedProcedure, string connSql)
    {
        var p = model is null
            ? new DynamicParameters()
            : GetClassParameters<T>(StoredProcedure(storedProcedure), model, connSql);
        return await Transact.QueryDataAsync<T, DynamicParameters>(storedProcedure, p, connSql);
    }

    /// <summary>
    ///     Returns a list of specified class of T from a stored procedure that will retrieve parameters values from class of U
    ///     asynchronously
    /// </summary>
    /// <typeparam name="T">Class of parameters that the data will be returned in</typeparam>
    /// <typeparam name="TU">Class that contain the value for the parameters</typeparam>
    /// <param name="model">
    ///     Model containing parameter values for the stored procedure, supply null if no parameters are
    ///     required
    /// </param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <returns></returns>
    public static async Task<List<T>> GetDataAsync<T, TU>(TU? model, string storedProcedure, string connSql)
    {
        var p = model is null
            ? new DynamicParameters()
            : GetClassParameters<TU>(StoredProcedure(storedProcedure), model, connSql);
        return await Transact.QueryDataAsync<T, DynamicParameters>(storedProcedure, p, connSql);
    }

    /// <summary>
    ///     Returns a scalar value of T from a stored procedure
    /// </summary>
    /// <typeparam name="T">Return value type</typeparam>
    /// <param name="model">
    ///     Model containing parameter values for the stored procedure, supply null if no parameters are
    ///     required
    /// </param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <returns></returns>
    public static T? GetScalarValue<T>(T? model, string storedProcedure, string connSql)
    {
        var p = model is null
            ? new DynamicParameters()
            : GetClassParameters<T>(StoredProcedure(storedProcedure), model, connSql);
        return Transact.ExecuteScalar<T, DynamicParameters>(storedProcedure, ref p, connSql);
    }

    /// <summary>
    ///     Returns a scalar values of T from a stored procedure that requires parameters, asynchronously
    /// </summary>
    /// <typeparam name="T">Return value type</typeparam>
    /// <typeparam name="T">Model containing parameter values for the stored procedure</typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <param name="model"></param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <returns></returns>
    public static async Task<T?> GetScalarValueAsync<T, TU>(TU? model, string storedProcedure, string connSql)
    {
        var p = model is null
            ? new DynamicParameters()
            : GetClassParameters(storedProcedure, model, connSql);
        return await Transact.ExecuteScalarAsync<T, DynamicParameters>(storedProcedure, p, connSql);
    }

    /// <summary>
    ///     Writes data based on a stored procedure and a specified class(model) of T. Class parameters names and stored
    ///     procedure parameters names must match, other than the starting @
    /// </summary>
    /// <typeparam name="T">Model containing parameter values for the stored procedure</typeparam>
    /// <param name="model">Class containing parameter names and values</param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    public static void WriteData<T>(T model, string storedProcedure, string connSql)
    {
        var p = GetClassParameters(StoredProcedure(storedProcedure), model, connSql);
        Transact.ExecuteProcedure(storedProcedure, ref p, connSql);
    }

    /// <summary>
    ///     Writes data based on a stored procedure and a specified class(model) of T. Class parameters names and stored
    ///     procedure parameters names must match, other than the starting @
    /// </summary>
    /// <typeparam name="T">Model containing parameter values for the stored procedure</typeparam>
    /// <param name="model">Class containing parameter names and values</param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <param name="extraParameters">Parameters not in table model. Format key:@name as string, value:as string</param>
    public static void WriteData<T>(T model, string storedProcedure, string connSql,
        Dictionary<string, string> extraParameters)
    {
        var p = GetClassParameters(storedProcedure, model, connSql);
        foreach (var parameter in extraParameters)
        {
            p.Add(parameter.Key, parameter.Value);
        }

        Transact.ExecuteProcedure(storedProcedure, ref p, connSql);
    }

    /// <summary>
    ///     Writes data based on a stored procedure and a specified class(model) of T. Class parameters names and stored
    ///     procedure parameters names must match, other than the starting @
    /// </summary>
    /// <typeparam name="T">Model containing parameter values for the stored procedure</typeparam>
    /// <param name="model">Class containing parameter names and values</param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <param name="extraParameters">Parameters not in table model. Format key:@name as string, value:as string</param>
    public static async Task<int> WriteDataAsync<T>(T model, string storedProcedure, string connSql,
        Dictionary<string, string> extraParameters)
    {
        var p = GetClassParameters(StoredProcedure(storedProcedure), model, connSql);
        foreach (var parameter in extraParameters)
        {
            p.Add(parameter.Key, parameter.Value);
        }

        return await Transact.ExecuteProcedureAsync(storedProcedure, p, connSql);
    }

    /// <summary>
    ///     Writes data based on a stored procedure and a specified class(model) of T. Class parameters names and stored
    ///     procedure parameters names must match, other than the starting @.  This method uses a supplied IDbConnection,
    ///     mainly for use in a transaction.
    /// </summary>
    /// <typeparam name="T">Model containing parameter values for the stored procedure</typeparam>
    /// <param name="model">Class containing parameter names and values</param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    /// <param name="transaction">IDbConnection</param>
    /// <param name="extraParameters">Parameters not in table model. Format key:@name as string, value:as string</param>
    public static async Task<int> WriteDataAsync<T>(T model, string storedProcedure, IDbTransaction transaction,
        Dictionary<string, string> extraParameters)
    {
        var p = GetClassParameters(StoredProcedure(storedProcedure), model, transaction);
        foreach (var parameter in extraParameters)
        {
            p.Add(parameter.Key, parameter.Value);
        }

        return await Transact.ExecuteProcedureAsync(storedProcedure, p, transaction);
    }
    /// <summary>
    ///     Writes data based on a stored procedure and a specified class(model) of T. Class parameters names and stored
    ///     procedure parameters names must match, other than the starting @, asynchronously
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model">Model containing parameter values for the stored procedure</param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="connSql">Connection string for the database</param>
    public static async Task<int> WriteDataAsync<T>(T model, string storedProcedure, string connSql)
    {
        var p = GetClassParameters(StoredProcedure(storedProcedure), model, connSql);
        return await Transact.ExecuteProcedureAsync(storedProcedure, p, connSql);
    }

    /// <summary>
    ///     Writes data based on a stored procedure and a specified class(model) of T. Class parameters names and stored
    ///     procedure parameters names must match, other than the starting @, asynchronously. This method uses a supplied
    ///     IDbConnection,
    ///     mainly for use in a transaction
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model">Model containing parameter values for the stored procedure</param>
    /// <param name="storedProcedure">Stored procedure's fully qualified name: "schema.sp_name"</param>
    /// <param name="transaction">IDbConnection</param>
    public static async Task<int> WriteDataAsync<T>(T model, string storedProcedure, IDbTransaction transaction)
    {
        var p = GetClassParameters(StoredProcedure(storedProcedure), model, transaction);
        return await Transact.ExecuteProcedureAsync(storedProcedure, p, transaction);
    }

    /// <summary>
    ///     Builds a set of dynamic parameters based on the model's properties and the required parameters of the stored
    ///     procedure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storedProcedure"></param>
    /// <param name="props"></param>
    /// <param name="connSql"></param>
    /// <returns></returns>
    public static DynamicParameters GetClassParameters<T>(string storedProcedure, T props, string connSql)
    {
        /*Query SQL for stored procedure parameters*/
        var query =
            "DECLARE @sp nvarchar(50) SELECT p.name AS Parameter FROM sys.procedures sp JOIN sys.parameters p " +
            $"ON sp.object_id = p.object_id JOIN sys.types t ON p.system_type_id = t.system_type_id WHERE sp.name = '{StoredProcedure(storedProcedure)}' ";
        var parameters = Transact.QueryData<string>(query, connSql);
        /*Get all the properties from the supplied class*/
        var t = typeof(T);
        var properties = t.GetProperties();
        /*Create dynamic parameters and match the class properties to the needed stored procedure parameters*/
        var p = new DynamicParameters();

        foreach (var property in properties)
        {
            var propertyName = $"@{property.Name[..1].ToLower()}{property.Name[1..]}";

            if (!parameters.Contains(propertyName, StringComparer.CurrentCultureIgnoreCase)) continue;

            var propertyValue = property.GetValue(props);
            propertyValue ??= string.Empty;
            p.Add($"@{propertyName}", propertyValue);
        }

        return p;
    }

    /// <summary>
    ///     Builds a set of dynamic parameters based on the model's properties and the required parameters of the stored
    ///     procedure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storedProcedure"></param>
    /// <param name="props"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public static DynamicParameters GetClassParameters<T>(string storedProcedure, T props, IDbTransaction transaction)
    {
        /*Query SQL for stored procedure parameters*/
        var query =
            "DECLARE @sp nvarchar(50) SELECT p.name AS Parameter FROM sys.procedures sp JOIN sys.parameters p " +
            $"ON sp.object_id = p.object_id JOIN sys.types t ON p.system_type_id = t.system_type_id WHERE sp.name = '{StoredProcedure(storedProcedure)}' ";
        var parameters = (List<string>)transaction.Connection.Query<string>(query, transaction: transaction);
        /*Get all the properties from the supplied class*/
        var t = typeof(T);
        var properties = t.GetProperties();
        /*Create dynamic parameters and match the class properties to the needed stored procedure parameters*/
        var p = new DynamicParameters();

        foreach (var property in properties)
        {
            var propertyName = $"@{property.Name[..1].ToLower()}{property.Name[1..]}";

            if (!parameters.Contains(propertyName, StringComparer.CurrentCultureIgnoreCase)) continue;

            var propertyValue = property.GetValue(props);
            propertyValue ??= string.Empty;
            p.Add($"@{propertyName}", propertyValue);
        }

        return p;
    }
}