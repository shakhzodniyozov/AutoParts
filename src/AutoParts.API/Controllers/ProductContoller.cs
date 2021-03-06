using AutoParts.Application.Products.Commands;
using AutoParts.Application.Products.Queries;
using AutoParts.Application.Sales.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoParts.API.Controllers;

[Authorize(Roles = "Admin,Employee")]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<ProductDto[]>> GetAll(int? page)
    {
        return Ok(await Mediator.Send(new GetAllProductsQuery(page)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        return Ok(await Mediator.Send(new GetProductQuery(id)));
    }

    [HttpGet("preliminary")]
    public async Task<ActionResult<PreliminaryDataDto>> GetPreliminaryData()
    {
        return Ok(await Mediator.Send(new GetPreliminaryDataQuery()));
    }

    [HttpGet("filter")]
    public async Task<ActionResult<ProductDto[]>> GetFilteredProducts([FromQuery] int[]? categories,
                                                                    [FromQuery] int[]? manufactors,
                                                                    [FromQuery] string[]? models,
                                                                    [FromQuery] int? yearFrom,
                                                                    [FromQuery] int? yearTo,
                                                                    [FromQuery] int? priceFrom,
                                                                    [FromQuery] int? priceTo)
    {
        return Ok(await Mediator.Send(new GetFilteredProductsQuery(categories, manufactors, models, yearFrom, yearTo, priceFrom, priceTo)));
    }

    [HttpGet("suggestion")]
    public async Task<ActionResult<ProductForAutocompleteDto[]>> GetSuggestions([FromQuery] string value)
    {
        return Ok(await Mediator.Send(new GetSuggestionsQuery(value)));
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("sale")]
    public async Task<IActionResult> CreateSale(CreateSaleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    #endregion

    #region UPDATE

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }
    #endregion

    #region DELETE

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
    #endregion
}