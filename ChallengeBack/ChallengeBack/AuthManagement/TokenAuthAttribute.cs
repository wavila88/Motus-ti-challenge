using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class TokenAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string TokenHeader = "Authorization";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Get the secret key from configuration or environment variable
        var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
        var secretKey = config?["Jwt:Secret"] ?? Environment.GetEnvironmentVariable("Jwt:Secret");

        string token = null;

        if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            token = authHeader.ToString().Replace("Bearer ", "");
        }

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedObjectResult("Invalid or missing token.");
            return;
        }

        token = token.ToString().Replace("Bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            await next();
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult("Invalid token.");
        }   
    }
}