using API1.DTOs;
using API1.Services;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers;

[Route("api/roles")]
public sealed class RolesController(
    ICrudService<RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO> service)
    : CrudControllerBase<RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO>(service)
{
}
