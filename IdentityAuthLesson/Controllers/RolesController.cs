using IdentityAuthLesson.DTOs;
using IdentityAuthLesson.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuthLesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateRole(RoleDTO role)
        {
            
            var result = await _roleManager.FindByNameAsync(role.RoleName);
                
            if (result == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role.RoleName));

                return Ok(new ResponseDTO
                {
                    Message = "Role Created",
                    IsSuccess = true,
                    StatusCode = 201
                });
            }

            return Ok(new ResponseDTO
            {
                Message = "Role cann not created",
                StatusCode = 403
            });
        }


        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return Ok(roles);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(string roleName, string updateToRoleName)
        {
            var res = await _roleManager.FindByNameAsync(roleName);
            if (res is null)
                throw new Exception("Not found");
            res.Name = updateToRoleName;
            res.NormalizedName = updateToRoleName.ToUpper();

            var updatedRole = await _roleManager.UpdateAsync(res);
            if (!updatedRole.Succeeded)
                throw new Exception("Something went wrong!");
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteRolesByName(RoleDTO name)
        {
            IdentityRole? roleName = await _roleManager
                .FindByNameAsync(roleName: name.RoleName);

            if (roleName is not null)
            {
                IdentityResult? roles = await _roleManager.DeleteAsync(role: roleName);
                return Ok(value: roles.Succeeded);
            }

            return Ok(value: false);
        }

    }
}
