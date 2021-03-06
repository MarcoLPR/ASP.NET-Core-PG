﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ASP.NET_Core_PG.Models;

namespace ASP.NET_Core_PG.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<PGUser> _signInManager;
        private TravelContext _context;
        private ITravelRepository _repository;

        public AuthController(SignInManager<PGUser> signInManager, TravelContext context, ITravelRepository repository)
        {
            _signInManager = signInManager;
            _context = context;
            _repository = repository;
        }

        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Database", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                                                      vm.Password,
                                                                      true,false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Database", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }

        [HttpPost("")]
        public async Task<IActionResult> Register([FromBody]LoginViewModel requestedUser)
        {
            if (ModelState.IsValid)
            {
                var newUser = Mapper.Map<PGUser>(requestedUser);
                _repository.AddUser(newUser);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"{requestedUser.Username}", Mapper.Map<LoginViewModel>(newUser));
                }
            }

            return BadRequest("Failed to Save User");
        }
    }
}