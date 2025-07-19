// Application Presentation Layer
namespace Catalog.API.Products.UpdateProduct;
public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record UpdateProductResponse(bool isSuccess);
public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            // map to command object with mapster
            var command = request.Adapt<UpdateProductCommand>();
            // send to the command object with mediator
            var result = await sender.Send(command);
            // map result to Response with mapster
            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
    .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
    }
}
