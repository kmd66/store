using Dapper;
using System.Data;

namespace MizeBazi.Store.Data.Repositories;
public static class DapperCommandHelper
{
    public static CommandDefinition CreateQuery(
        string storedProcedureName,
        CancellationToken cancellationToken = default,
        int timeout = 30,
        params (string Name, object Value)[] parameters)
    {
        var dynamicParameters = new DynamicParameters();

        foreach (var param in parameters)
        {
            dynamicParameters.Add(param.Name, param.Value);
        }

        var commandDefinition = new CommandDefinition(
            commandText: storedProcedureName,
            parameters: dynamicParameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: timeout,
            cancellationToken: cancellationToken
        );

        return commandDefinition;
    }
    public static CommandDefinition CreateQuery(
        this DynamicParameters parameters,
        string storedProcedure,
        int? commandTimeout = 30,
        CancellationToken cancellationToken = default)
    {
        return new CommandDefinition(
            commandText: storedProcedure,
            parameters: parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout,
            cancellationToken: cancellationToken
        );
    }
}

