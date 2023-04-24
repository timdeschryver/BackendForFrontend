using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies", options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = false;
        options.Cookie.Name = "__MySPA";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("OIDC:Authority");
        options.ClientId = builder.Configuration.GetValue<string>("OIDC:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("OIDC:ClientSecret");
 
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.ResponseMode = OpenIdConnectResponseMode.Query;
 
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
 
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
    });
builder.Services.AddBff();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
// Use the BFF middleware (must be before UseAuthorization)
app.UseBff();
app.UseAuthorization();

// TOOD: add groupings
app.Map("/api/private-endpoint", () => Results.Ok(new {Hello = "From private endpoint"}))
    .RequireAuthorization();

app.Map("/api/private-bff-endpoint", () => Results.Ok(new {Hello = "From private BFF endpoint"}))
    .AsBffApiEndpoint()
    .RequireAuthorization();

app.Map("/api/public-endpoint", () => Results.Ok(new {Hello = "From public endpoint"}));

app.Map("/api/public-bff-endpoint", () => Results.Ok(new {Hello = "Fron public BFF endpoint"}))
    .AsBffApiEndpoint();

app.MapBffManagementEndpoints();

app.Run();
