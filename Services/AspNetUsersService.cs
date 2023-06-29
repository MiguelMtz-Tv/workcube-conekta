using Microsoft.CodeAnalysis.FlowAnalysis;
using workcube_pagos.ViewModel.Req.Usuario;
using workcube_pagos.ViewModel.Statics;

namespace workcube_pagos.Services
{
    public class AspNetUsersService
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly DataContext _context;

        public AspNetUsersService(DataContext context, SignInManager<AspNetUser> signInManager, UserManager<AspNetUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<AspNetUser> FindLogin(string UserName)
        {
            try 
            {
                return await _context.AspNetUsers.Where(itemUser => itemUser.UserName == UserName || itemUser.Email == UserName).FirstOrDefaultAsync();
            }catch (Exception) {  return null; }
        }

        public async Task<dynamic>GetUser(string id) //obtener un usuario (nombre y apellidos)
        {
            if(id == null) { throw new ArgumentException("Se recibió un id nulo"); }
            AspNetUser user = new AspNetUser{};

            try
            {
                user = await _context.AspNetUsers.FindAsync(id);
                if(user == null) {throw new ArgumentException("No se pudieron recuperar los datos del usuario");}
            }
            catch(Exception e)
            {
                throw new ArgumentException("Error al encontrar el usuario: " + e.Message);
            }

            var resUser = new 
            {
                Nombre = user.Nombre,
                ApellidoPat = user.ApellidoPat,
                ApellidoMat = user.ApellidoMat
            };

            return resUser;
        }
        
        //actualizar un usuario
        public async Task<AspNetUser> UpdateUser(UpdateUserReq data)
        {
            var user = await _context.AspNetUsers.FindAsync(data.Id);
            if (user == null) { throw new ArgumentException("No se pudieron recuperar los datos de la sesión"); }

            user.Nombre =       data.Nombre;
            user.ApellidoPat =  data.ApellidoPat;
            user.ApellidoMat =  data.ApellidoMat;
            
            await _context.SaveChangesAsync();
            return user;

        }

        //actualizar contraseña
        public async Task<AspNetUser> UpdatePassword(UpdatePasswordReq password)
        {
            var user = await _context.AspNetUsers.Where(user => user.Id == password.Id).FirstOrDefaultAsync();
            
            if (user == null)
            {
                throw new ArgumentException("No se encontró al usuario en sesión");
            }
           
            var result = await _signInManager.CheckPasswordSignInAsync(user, password.OldPassword, false);
            if(result.Succeeded)
            {
                var passHasher = new PasswordHasher<AspNetUser>();
                var newPass = passHasher.HashPassword(user, password.NewPassword);
                user.PasswordHash = newPass;
                await _context.SaveChangesAsync();
                return user;
            }

            throw new ArgumentException("Contraseña incorrecta");
        }
        
        //para añadir un nuevo usuario
        public async Task<dynamic> AddUser(SingUpReq model)
        {
            //verificar si el numero de contrato existe y obtener al cliente
            var Cliente = await _context.Clientes.AsNoTracking().Where(c => c.NumeroContrato == model.NumeroContrato).FirstOrDefaultAsync();
            if (Cliente == null) { throw new ArgumentException("Este numero de contrato no existe"); }

            //Verificar si el numero de contrato ya está en uso
            var contractAlredyUsed = await _context.AspNetUsers.AsNoTracking().Where(asp => asp.Cliente.NumeroContrato == model.NumeroContrato).FirstOrDefaultAsync();
            if (contractAlredyUsed != null) { throw new ArgumentException("Este numero de contrato ya está en uso"); }

            //Verificar si el Email ya está en uso
            var emailAlreadyUsed = await _context.AspNetUsers.AsNoTracking().Where(asp => asp.Email == model.Email).FirstOrDefaultAsync();
            if(emailAlreadyUsed != null) { throw new ArgumentException("El correo ya se encuentra en uso"); }

            var NewUser = new AspNetUser
            {
                IdCliente =     Cliente.IdCliente,
                Email =         model.Email,
                Nombre =        model.Nombre,
                ApellidoPat =   model.ApellidoPat,
                ApellidoMat =   model.ApellidoMat,
                UserName =      model.UserName,

                EmailConfirmed =        true,
                PhoneNumberConfirmed =  true,
                TwoFactorEnabled =      true,
                LockoutEnabled =        false,
                AccessFailedCount =     0,

                IsActive = true,
            };
            
            //Encriptación de la contraseña
            var PasswordHasher =    new PasswordHasher<AspNetUser>();
            var password =          model.Password;
            var HashedPassword =    PasswordHasher.HashPassword(null, password);

            NewUser.PasswordHash = HashedPassword;

            var loginTransacction = _context.Database.BeginTransaction();
            await _context.AspNetUsers.AddAsync(NewUser);
            await _context.SaveChangesAsync();
            loginTransacction.Commit();

            return NewUser;

        }

    }
}
