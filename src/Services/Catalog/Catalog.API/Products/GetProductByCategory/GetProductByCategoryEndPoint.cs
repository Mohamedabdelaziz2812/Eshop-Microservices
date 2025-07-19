// Application Presentation Layer
namespace Catalog.API.Products.GetProductByCategory;
//public record GetProductByIdRequest();
public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {

            // send to the command object with mediator
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            // map result to Response with mapster
            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
        // Here Some configuration
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
