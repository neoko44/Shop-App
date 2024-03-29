﻿using Azure.Core;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public ActionResult Login(string email,string password)
        {
            var userToLogin = _authService.Login(email,password);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("register")]
        public ActionResult Register(string firstName,string lastName,string email,string phone,string password,string address,string city)
        {
            var userExists = _authService.UserExists(email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            var registerResult = _authService.Register(firstName,lastName,email,phone,address,city,password);
            var result = _authService.CreateAccessToken(registerResult.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        //body - query

        [HttpPost("changepassword")]
        //[Authorize("Member")]
        public ActionResult ChangePassword(string newPassword, string confirmPassword, string token)
        {
            if (newPassword != confirmPassword)
            {
                return BadRequest(Messages.PasswordNotMatch);
            }
            var changePassResult = _authService.ChangePassword(newPassword, token);
            if (changePassResult.Success)
            {
                return Ok(changePassResult.Message);
            }
            else return BadRequest(changePassResult.Message);
        }
    }
}
