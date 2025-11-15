namespace MizeBazi.Store.Common.Abstractions; 
public interface IRequest<TResponse> { }

public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    Task<TResponse> Handle(TRequest request);
}

//----------TCommand--------------
public interface ICommand<TResponse> : IRequest<TResponse> { }
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>{}
public abstract class CommandBase<TCommand, TResponse> : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    public virtual Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TResponse> Handle(TCommand request)
    {
        throw new NotImplementedException();
    }
}

//----------TQuery--------------
public interface IQuery<TResponse> : IRequest<TResponse> { }
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>{}
public abstract class QueryBase<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    public virtual Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TResponse> Handle(TQuery request)
    {
        throw new NotImplementedException();
    }
}


//----------Behavior--------------
public interface IBehaviorHandler<T>
{
    Task Handle(T command, CancellationToken cancellationToken);
    Task Handle(T command);
}
public abstract class BehaviorBase<TRequest> : IBehaviorHandler<TRequest>
{
    public virtual Task Handle(TRequest command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public virtual Task Handle(TRequest command)
    {
        throw new NotImplementedException();
    }
}


//--------Pipeline----------------
public interface IPipelineBehavior<TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest model, CancellationToken cancellationToken);
    Task<TResponse> Handle(TRequest model);
}

public abstract class PipelineBase<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public virtual Task<TResponse> Handle(TRequest model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public virtual Task<TResponse> Handle(TRequest model)
    {
        throw new NotImplementedException();
    }
}


//------------------------
public interface IAppMediator
{
    Task<TResponse> Send<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default
    );
    Task<TResponse> Send<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default
    );
    Task<TAfterBehavior> Pipline<TResponse, TAfterBehavior>(
        IRequest<TResponse> request,
        IRequest<TAfterBehavior> afterBehavior = null,
        CancellationToken cancellationToken = default
    );
}
