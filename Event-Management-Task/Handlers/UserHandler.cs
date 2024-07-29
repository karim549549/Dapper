// File: UserHandlers.cs

using Dapper;
using Microsoft.Data.SqlClient;
using Event_Management_Task.Models;
using Event_Management_Task.Services;
using Microsoft.AspNetCore.Mvc;
using Event_Management_Task.Utilities;
using Event_Management_Task.DTOs.User;
using Event_Management_Task.Services.AuthenticationService;
using System.Security.Claims;
using System.Data;

using Event_Management_Task.Repositories;

namespace Event_Management_Task.Handlers
{
    public static class UserHandlers
    {
        public static async Task<IResult> GetUsers(
            [FromServices] BaseRepository UserRepository,
            [FromQuery] int pageSize=10,
            [FromQuery] int pageNumber=1
            )
        {
            try
            {

                var parameters = new DynamicParameters();
                parameters.Add("PageSize", pageSize);
                parameters.Add("PageNumber", pageNumber);

                var users = await UserRepository.BulkExecuteProcedureAsync<User>
                    (SystemProcedures.GETUSERS, parameters );
                if (users is null || ! users.Any())
                {
                    return Results.NotFound(new StanderdResponse
                    {
                        Message = "No users found",
                        Status = MagicStrings.FAILED
                    });
                }

                return Results.Ok(new StanderdResponse
                {
                    Message = "Users retrieved successfully",
                    Status = MagicStrings.SUCCESS,
                    Data = users
                });
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
             throw new Exception(ex.Message);
            }
        }

        public static async Task<IResult> Login(
            [FromServices] BaseRepository UserRepository,
            [FromServices] TokenProvider tokenProvider,
            [FromBody] LoginDto Dto
            )
        {
            try
            {
                var parameters   = new DynamicParameters();
                parameters.Add("Email", Dto.Email);
                var user = await UserRepository.ExecuteProcedureAsync<User>
                    (SystemProcedures.GETUSER, parameters);

                if (user is null  || !BCrypt.Net.BCrypt.Verify(Dto.Password, user.Password_hash))
                {
                    return Results.NotFound(new StanderdResponse
                    {
                        Message = "Email or password is incorrect",
                        Status = MagicStrings.FAILED
                    });
                }

                return Results.Ok(new StanderdResponse
                {
                    Message = "Successfull Login",
                    Status = MagicStrings.SUCCESS,
                    Data = tokenProvider.Generate(user)
                });
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
           
        public static async Task<IResult> Register( 
            [FromServices] BaseRepository UserRepository,
            [FromServices] TokenProvider tokenProvider,
            [FromBody] RegisterDto dto
            )
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Email", dto.Email);
                parameters.Add("PasswordHash", BCrypt.Net.BCrypt.HashPassword(dto.Password));
                parameters.Add("FirstName", dto.FirstName);
                parameters.Add("LastName", dto.LastName);
                parameters.Add("Role", MagicStrings.USER_ROLE);
                var user = await UserRepository.ExecuteProcedureAsync<User>
                    (SystemProcedures.CREATEUSER,parameters);

                if (user is null )
                {
                    return Results.NotFound(new StanderdResponse
                    {
                        Message = "User not found",
                        Status = MagicStrings.FAILED
                    });
                }
                return Results.Ok(new StanderdResponse
                {
                    Message = "User created successfully",
                    Status = MagicStrings.SUCCESS,
                    Data= tokenProvider.Generate(user)
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }    
    }
}
