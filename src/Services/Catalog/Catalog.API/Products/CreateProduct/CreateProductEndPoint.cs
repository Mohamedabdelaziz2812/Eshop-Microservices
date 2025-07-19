// Application Presentation Layer
namespace Catalog.API.Products.CreateProduct;
public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);


public record CreateProductResponse(Guid Id);

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // map to command object with mapster
            var command = request.Adapt<CreateProductCommand>();
            // send to the command object with mediator
            var result = await sender.Send(command);
            // map result to Response with mapster
            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
            // Here Some configuration
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}
