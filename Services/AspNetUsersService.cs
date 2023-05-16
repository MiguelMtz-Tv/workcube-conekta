﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using workcube_pagos.ViewModel.Req;
using workcube_pagos.ViewModel.Statics;

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

        public async Task<AspNetUserFullName> GetUser(string id)
        {
            var user = await _context.AspNetUsers.Where(user => user.Id == id).FirstOrDefaultAsync();
            if(user == null)
            {
                return null;
            }

            var resUser = new AspNetUserFullName
            {
                Nombre = user.Nombre,
                ApellidoPat = user.ApellidoPat,
                ApellidoMat = user.ApellidoMat
            };

            return resUser;
        }
        
        //actualizar un usuario
        public async Task<AspNetUser> UpdateUser(string id, UpdateUserReq data)
        {
            var user = await _context.AspNetUsers.Where(user => user.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            user.Nombre = data.Nombre;
            user.ApellidoPat = data.ApellidoPat;
            user.ApellidoMat = data.ApellidoMat;
            await _context.SaveChangesAsync();
            return user;

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
