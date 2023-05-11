using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace workcube_pagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<List<UsuarioModel>> Get()
        {
            return await _context.Usuario.ToListAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<UsuarioModel> Post(UsuarioModel usuario)
        {
            var NewUser = new UsuarioModel();

            NewUser.IdCliente = usuario.IdCliente;
            NewUser.Nombre = usuario.Nombre;
            NewUser.ApellidoPat = usuario.ApellidoPat;
            NewUser.ApellidoMat = usuario.ApellidoMat;
            NewUser.UserName = usuario.UserName;
            NewUser.EmailConfirmed = true;
            NewUser.PhoneNumberConfirmed = true;
            NewUser.TwoFactorEnabled = true;
            NewUser.LockoutEnabled = false;
            NewUser.AccessFailedCount = 0;

            //hash password
            var passwordHasher = new PasswordHasher<UsuarioModel>();
            var password = usuario.PasswordHash;
            var hashedPassword = passwordHasher.HashPassword(usuario, password);
            NewUser.PasswordHash = hashedPassword; 

            await _context.Usuario.AddAsync(NewUser);
            await _context.SaveChangesAsync();
            return NewUser; 
        }

        //POST api/<UsuarioController>/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioModel>> getUsuario(UsuarioModel usuario)
        {
            var emailOrUser = usuario.UserName;
            var contrasenia = usuario.PasswordHash;
            var foundedUser = await _context.Usuario.Where(u => u.Email == emailOrUser || u.UserName == emailOrUser).FirstOrDefaultAsync();
            
            if (foundedUser == null)
            {
                return NotFound();
            }

            var passwordHasher = new PasswordHasher<UsuarioModel>();
            var verifyPassword = passwordHasher.VerifyHashedPassword(foundedUser, foundedUser.PasswordHash, contrasenia);

            if(verifyPassword == PasswordVerificationResult.Success)
            {
                return Ok(foundedUser);
            }
            else
            {
                return Unauthorized("Contraseña incorrecta");
            }

        }


        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
