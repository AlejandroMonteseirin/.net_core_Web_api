using System;
using Microsoft.AspNetCore.Mvc;
using proyecto.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;  
using proyecto.Services;

namespace proyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class LoginController : Controller  
    {  
        private IConfiguration _config;
        private IUserService _userService;  
    
        public LoginController(IConfiguration config, IUserService userService)  
        {  
            _config = config;  
            _userService = userService;
        }  
        [HttpPost]  
        [AllowAnonymous]//no hace falta tener validado el json web token
        public IActionResult Login([FromBody]UserDto login)  
        {  
            IActionResult response = Unauthorized();  
			//Método responsable de Validar las credenciales del usuario y devolver el modelo Usuario
		    //Para demostración (en este punto) he usado datos de prueba sin persistencia de Datos
			//Si no retorna un objeto nulo, se procede a generar el JWT.
			//Usando el método GenerateJSONWebToken
            //var user = AuthenticateUser(login);  
            var user = _userService.Authenticate(login.username,login.password);  

            if (user != null)  
            {  
                var tokenString = GenerateJSONWebToken(user);  
                response = Ok(new { token = tokenString });  
            }  
    
            return response;  
        }  
    
        [Authorize]
        [HttpPost("register")]
        [AllowAnonymous]//no hace falta tener validado el json web token

        public IActionResult Register([FromBody] UserDto userDto)
        {
            User user = new User {} ;
            user.username = userDto.username;
            user.email = userDto.email;
            user.FechaCreado = userDto.FechaCreado;

            try
            {
                _userService.Create(user, userDto.password);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        private string GenerateJSONWebToken(User userInfo)  
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
            
            if(userInfo.email==null){
                userInfo.email="";
            }

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.email),
                new Claim("FechaCreado", userInfo.FechaCreado.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
    
			//Se crea el token utilizando la clase JwtSecurityToken
			//Se le pasa algunos datos como el editor (issuer), audiencia
			// tiempo de expiración y la firma.
			
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],  
                _config["Jwt:Issuer"],  
                claims,  
                expires: DateTime.Now.AddMinutes(120),  
                signingCredentials: credentials);  
    
			//Finalmente el método JwtSecurityTokenHandler genera el JWT. 
			//Este método espera un objeto de la clase JwtSecurityToken 
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }  
    
        private Usuario AuthenticateUser(Usuario login)  
        {  
            Usuario user = null;  
    
            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            if (login.username == "Daniel")  
            {  
                //user = new Usuario { username = "Daniel", password = "123456" };  
                user = new Usuario {username = login.username, password=login.password,email=login.email,FechaCreado=login.FechaCreado};
            }  
            return user;  
        }  
    }  
}
