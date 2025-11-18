using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class GetProductByIdHandler(
    IProductRead dataSource
    ) : QueryBase<GetProductByIdQuery, GetProductResult>
{

    public override Task<GetProductResult> Handle(GetProductByIdQuery query){
        query.Check(BrandConstants.ValidatError_Id);
        return dataSource.GetAsync(query);
    }
}