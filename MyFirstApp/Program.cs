using System.IO;
using System.ComponentModel.Design;
using Microsoft.Extensions.Primitives;
using MyFirstApp.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

app.Run(async (HttpContext context ) =>
{
    StreamReader reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();
   
    Dictionary<string, StringValues> querydict =
    Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    if (querydict.ContainsKey("firstName"))
    {
        string firstName = querydict["firstName"][0];
        await context.Response.WriteAsync(firstName);
    }
});


app.UseMiddleware<MyCustomMiddleware>();



app.Run();
