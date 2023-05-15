using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;
using System.Security.Claims;
using Workcube.Libraries;
using Workcube.ViewModels;
using workcube_pagos.ViewModel.Req;
using workcube_pagos.ViewModel.Res;

namespace workcube_pagos.Services
{
    public class AspNetUsersService
    {
        private readonly DataContext _context;
        private readonly UserManager<AspNetUser> _userManager;

        public AspNetUsersService(DataContext context, UserManager<AspNetUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AspNetUser> FindLogin(string UserName)
        {
            return await _context.AspNetUsers.Where(itemUser => itemUser.UserName == UserName).FirstOrDefaultAsync();
        }
        
        //para añadir un nuevo usuario
        public async Task<AspNetUser> AddUser(SingUpReq user)
        {
            var NewUser = new AspNetUser();

            NewUser.IdCliente = user.IdCliente;
            NewUser.Email = user.Email;
            NewUser.IdCliente = user.IdCliente;
            NewUser.Nombre = user.Nombre;
            NewUser.ApellidoPat = user.ApellidoPat;
            NewUser.ApellidoMat = user.ApellidoMat;
            NewUser.UserName =  user.UserName;

            NewUser.EmailConfirmed = true;
            NewUser.PhoneNumberConfirmed = true;
            NewUser.TwoFactorEnabled = true;
            NewUser.LockoutEnabled = false;
            NewUser.AccessFailedCount = 0;

            var PasswordHasher = new PasswordHasher<AspNetUser>();
            var password = user.Password;
            var HashedPassword = PasswordHasher.HashPassword(null, password);
            
            NewUser.PasswordHash = HashedPassword;

            await _context.AspNetUsers.AddAsync(NewUser);
            await _context.SaveChangesAsync();

            return NewUser;

        }

    }
}
