// Application Presentation Layer
namespace Catalog.API.Products.DeleteProduct;
public record DeleteProductResponse(bool IsSuccess);
public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {

            // send to the command object with mediator
            var result = await sender.Send(new DeleteProductCommand(id));
            // map result to Response with mapster
            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}
