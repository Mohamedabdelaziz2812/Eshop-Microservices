// Application Presentation Layer
namespace Catalog.API.Products.GetProductById;
//public record GetProductByIdRequest();
public record GetProductByIdResponse(Product Product);
public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {

            // send to the command object with mediator
            var result = await sender.Send(new GetProductByIdQuery(id));
            // map result to Response with mapster
            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
       // Here Some configuration
       .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
