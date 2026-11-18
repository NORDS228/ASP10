using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/users")]
public sealed class UsersController(
    ICrudService<UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO> service)
    : CrudControllerBase<UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO>(service)
{
}
