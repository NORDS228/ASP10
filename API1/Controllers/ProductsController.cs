using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/products")]
public sealed class ProductsController(
    ICrudService<ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO> service)
    : CrudControllerBase<ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO>(service)
{
}
