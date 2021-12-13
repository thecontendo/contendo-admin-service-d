using System.Net.Http.Headers;
using Azure.Identity;
using ContendoAdmin.Db.Context;
using ContendoAdmin.Models;
using ContendoAdmin.Models.Dto;
using ContendoAdmin.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using User = ContendoAdmin.Models.Identity.User;

namespace ContendoAdmin.Services.Identity;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly ITokenCacheService _tokenCacheService;
    private readonly IConfiguration _configuration;

    public UserService(AppDbContext dbContext, ITokenCacheService tokenCacheService, IConfiguration configuration)
    {
        _db = dbContext;
        _tokenCacheService = tokenCacheService;
        _configuration = configuration;
    }

    public async Task<ApiResponse<List<UserDto>>> Get()
    {
        var response = new ApiResponse<List<UserDto>>();
        try
        {
            var users = await _db.Users.Select(e => new UserDto
            {
                Age = e.Age,
                Address = e.Address,
                Id = e.Id,
                Username = e.Username
            }).ToListAsync();

            response.Data = users;
            response.AddSuccess();
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            response.AddError(ex);
            return await Task.FromResult(response);
        }
    }

    public async Task<ApiResponse<User>> Post(UserCreateDto data)
    {
        var response = new ApiResponse<User>();
        try
        {
            var result = await _db.Users.AddAsync(new User
            {
                Username = data.Username,
                Address = data.Address,
                Age = data.Age
            });
            await _db.SaveChangesAsync();

            response.Data = result.Entity;
            response.AddSuccess();
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            response.AddError(ex);
            return await Task.FromResult(response);
        }
    }


    private async Task<TokenCacheDto> GetToken()
    {
        var cachedToken = new TokenCacheDto
        {
            Name = "Graph-token",
            Token = string.Empty,
            Duration = int.Parse(this._configuration["TokenExpiryTime"])
        };

        try
        {
            cachedToken = _tokenCacheService.CheckTokenCache(cachedToken);


            if (!string.IsNullOrEmpty(cachedToken.Token)) return cachedToken;

            var defaultAuth = new DefaultAzureCredentialOptions { ExcludeSharedTokenCacheCredential = true };

            var credential = new DefaultAzureCredential(defaultAuth);

            var token = await credential.GetTokenAsync(
                new Azure.Core.TokenRequestContext(new[] { "https://graph.microsoft.com/.default" }));

            cachedToken.Token = token.Token;

            _tokenCacheService.SetTokenCache(cachedToken);

            return cachedToken;
        }
        catch (Exception)
        {
            return cachedToken;
        }
    }

    private async Task<GraphServiceClient> GetServiceClient()
    {
        var token = await GetToken();

        var accessToken = token.Token;

        var graphServiceClient =
            new GraphServiceClient(new DelegateAuthenticationProvider((requestMessage) =>
            {
                requestMessage
                    .Headers
                    .Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                return Task.CompletedTask;
            }));
        return graphServiceClient;
    }

    public async Task<ApiResponse<List<UserDto>>> GetGraphUsers(string param)
    {
        var response = new ApiResponse<List<UserDto>>();
        try
        {
            var graphServiceClient = await GetServiceClient();
            var filterString = $"startswith(givenName, '{param}')";
            var users = string.IsNullOrEmpty(param) || param == "*"
                ? await graphServiceClient.Users
                    .Request()
                    .GetAsync()
                : await graphServiceClient.Users
                    .Request()
                    .Filter(filterString)
                    .GetAsync();

            var msGraphUsers = users.Select(u => new UserDto
                {
                    Id = Guid.Parse(u.Id),
                    Username = $"{u.GivenName} {u.Surname}",
                    Address = u.OfficeLocation,
                    Age = 30
                })
                .ToList();

            response.Data = msGraphUsers;
            response.Dictionaries = new Dictionary<string, object>
            {
                {
                    "Count", msGraphUsers.Count
                }
            };

            response.AddSuccess();
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            response.Data = new List<UserDto>();
            response.Dictionaries = new Dictionary<string, object>
            {
                {
                    "Exception", ex
                }
            };
            response.AddError(ex);
            return await Task.FromResult(response);
        }
    }

    public async Task<ApiResponse<bool>> SendUserInvitation()
    {
        var response = new ApiResponse<bool>();
        try
        {
            var graphServiceClient = await GetServiceClient();

            //var graphServiceClient = CreateGraphServiceClient();
            var invitation = await graphServiceClient.Invitations.Request().AddAsync(new Invitation
            {
                InviteRedirectUrl = "https://localhost:27216/signin-oidc",
                InvitedUserDisplayName = "Abhi",
                InvitedUserEmailAddress = "abhinav10p@gmail.com",
                InvitedUserMessageInfo = new InvitedUserMessageInfo
                {
                    CustomizedMessageBody = "Hello !, Welcome to Contendo Admin"
                },
                SendInvitationMessage = true
            });

            // inviteUserModel.Status = invitation.Status;

            response.AddSuccess();
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            response.AddError(ex);
            return await Task.FromResult(response);
        }
    }

    public async Task<ApiResponse<bool>> CreateUser()
    {
        var response = new ApiResponse<bool>();
        try
        {
            var user = new Microsoft.Graph.User
            {
                AccountEnabled = true,
                City = "Seattle",
                Country = "United States",
                Department = "Sales & Marketing",
                DisplayName = "Melissa Darrow",
                GivenName = "Melissa",
                JobTitle = "Marketing Director",
                MailNickname = "MelissaD",
                PasswordPolicies = "DisablePasswordExpiration",
                PasswordProfile = new PasswordProfile
                {
                    Password = "Test@123",
                    ForceChangePasswordNextSignIn = false
                },
                OfficeLocation = "131/1105",
                PostalCode = "98052",
                PreferredLanguage = "en-US",
                State = "WA",
                StreetAddress = "9256 Towne Center Dr., Suite 400",
                Surname = "Darrow",
                MobilePhone = "+1 206 555 0110",
                UsageLocation = "DE",
                UserPrincipalName = "abhinav10p@contoso.com"
            };


            var graphServiceClient = await GetServiceClient();

            await graphServiceClient.Users
                .Request()
                .AddAsync(user);

            // inviteUserModel.Status = invitation.Status;

            response.AddSuccess();
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            response.AddError(ex);
            return await Task.FromResult(response);
        }
    }


    public static GraphServiceClient CreateGraphServiceClient()
    {
        const string tenant = "6be85c80-857c-41e8-a3ba-6f922f52a5bb"; //.onmicrosoft.com
        //TODO: fill on your application ID
        const string appId = "579d27bf-df6c-486b-9e72-b43dd5605d9c";
        //TODO: Fill in your application secret
        const string appSecret = "3lc7Q~e7By4.178hxUT5o0c2XxHh9auJ8AWtV";

        var clientCredential = new ClientCredential(appId, appSecret);
        var authenticationContext = new AuthenticationContext($"https://login.microsoftonline.com/{tenant}");
        var authenticationResult = authenticationContext
            .AcquireTokenAsync("https://graph.microsoft.com", clientCredential).Result;

        var delegateAuthProvider = new DelegateAuthenticationProvider((requestMessage) =>
        {
            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue("bearer", authenticationResult.AccessToken);

            return Task.FromResult(0);
        });

        return new GraphServiceClient(delegateAuthProvider);
    }
}