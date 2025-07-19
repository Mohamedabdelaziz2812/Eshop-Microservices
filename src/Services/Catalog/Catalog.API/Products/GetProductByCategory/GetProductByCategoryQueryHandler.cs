// Application Logic Layer
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
// Idocument is for postrge db
internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        // Business logic to create a product
        logger.LogInformation("GetByCategory");

        var products = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);

    }
}
