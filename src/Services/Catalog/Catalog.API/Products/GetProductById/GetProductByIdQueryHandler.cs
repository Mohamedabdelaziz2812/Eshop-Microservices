// Application Logic Layer
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
// Idocument is for postrge db
internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        // Business logic to create a product
        logger.LogInformation("GetInformation");

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);


        if (product is null)
            throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResult(product);

    }
}
