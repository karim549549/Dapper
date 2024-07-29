using Dapper; // Ensure you have the Dapper namespace
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;
using Event_Management_Task.Services;
using Event_Management_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Event_Management_Task.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Event_Management_Task.Routes
{
    public static class UserRoute
    {

        public static void MapUserRoute(this IEndpointRouteBuilder builder)
        {
            var Route = builder.MapGroup("/users");

            Route.MapGet("/", UserHandlers.GetUsers);
            //Route.MapGet("/Login", UserHandlers.Login);
            Route.MapPost("/Register", UserHandlers.Register);
            Route.MapPost("/Login", UserHandlers.Login);
        }
    }
}
