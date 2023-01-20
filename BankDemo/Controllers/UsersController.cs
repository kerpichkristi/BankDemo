using BankDemo.Models;
using BankDemo.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankDemo.Core.Security;
using BankDemo.ViewModels;

namespace BankDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        // POST: api/Users/Register
        [HttpPost]
        [Route("Register")]
        public async Task<Object> Register(ApplicationUserRegisterModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: '{ex}'");
                throw;
            }
        }

        // POST: api/Users/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(ApplicationUserLoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                string token;
                GenerateJwtToken.GenerateToken(user, userRoles, out token);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Invalid login attempt." });
            }
        }

        // GET: api/Users/GetAuthorizedUserInfo
        [HttpGet]
        [Authorize]
        [Route("GetAuthorizedUserInfo")]
        public async Task<Object> GetAuthorizedUserInfo()
        {
            string userId = User.Claims.First(c => c.Type == "Id").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Email
            };
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<ApplicationUserViewModel>>> Users()
        {
            List<ApplicationUserViewModel> applicationUserViewModels = new List<ApplicationUserViewModel>();
            List<ApplicationUser> applicationUsers = await _applicationDbContext.Users.ToListAsync();
            foreach (ApplicationUser applicationUser in applicationUsers)
            {
                applicationUserViewModels.Add(new ApplicationUserViewModel()
                {
                    Id = applicationUser.Id,
                    Email = applicationUser.Email,
                    Roles = _userManager.GetRolesAsync(applicationUser).Result.ToArray()
                });
            }
            return applicationUserViewModels;
        }

        // GET: api/Users/
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUser(string id)
        {
            var applicationUser = await _applicationDbContext.Users.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            ApplicationUserViewModel applicationUserViewModel = new ApplicationUserViewModel()
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Roles = _userManager.GetRolesAsync(applicationUser).Result.ToArray()
            };

            return applicationUserViewModel;
        }

        // DELETE: api/Users/
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApplicationUserViewModel>> DeleteUser(string id)
        {
            var applicationUser = await _applicationDbContext.Users.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            _applicationDbContext.Users.Remove(applicationUser);
            await _applicationDbContext.SaveChangesAsync();

            return new ApplicationUserViewModel()
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Roles = _userManager.GetRolesAsync(applicationUser).Result.ToArray()
            };
        }

        // PUT: api/Users/
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> PutUser(string id, ApplicationUserViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var applicationUser = await _applicationDbContext.Users.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            _applicationDbContext.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                var userRoles = await _userManager.GetRolesAsync(applicationUser);
                await _userManager.RemoveFromRolesAsync(applicationUser, userRoles.ToArray());
                await _userManager.AddToRolesAsync(applicationUser, model.Roles);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (applicationUser == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/Users/GetRoles
        [HttpGet]
        [Route("GetRoles")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            return await _applicationDbContext.Roles.ToListAsync();
        }
    }
}
