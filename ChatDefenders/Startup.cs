using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ChatDefenders.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;

namespace ChatDefenders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // Determine whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

			// Configure access to a given database.
			services.AddDbContext<PostContext>(options => {
				options
					.UseLazyLoadingProxies()
					.UseSqlServer("Data Source=MSI;Initial Catalog=ChatDefenderDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			Action<AuthenticationOptions> configureOptions = options =>
			   {
				   options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				   options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				   options.DefaultChallengeScheme = "Discord";
			   };

			services.AddAuthentication(configureOptions)
			.AddCookie()
			.AddOAuth("Discord", options =>
			{
				options.ClientId = Configuration["Discord:ClientId"];
				options.ClientSecret = Configuration["Discord:ClientSecret"];
				options.CallbackPath = new PathString("/signin-discord");

				options.AuthorizationEndpoint = "https://discordapp.com/api/oauth2/authorize";
				options.TokenEndpoint = "https://discordapp.com/api/oauth2/token";
				options.UserInformationEndpoint = "https://discordapp.com/api/users/@me";

				options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
				options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
				options.ClaimActions.MapJsonKey("urn:discord:avatar", "avatar");

				options.Scope.Clear();
				options.Scope.Add("identify");

				options.Events = new OAuthEvents
				{
					OnCreatingTicket = async context =>
					{
						var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
						request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
						request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

						var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
						response.EnsureSuccessStatusCode();
						var user = JObject.Parse(await response.Content.ReadAsStringAsync());
						context.RunClaimActions(user);

						Account.UpdateOrRegister(context.Identity);
					}
				};
			});

			ServiceProviderInstance.RegisterInstance(services.BuildServiceProvider());
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
			app.UseAuthentication();

			app.Use(async (context, next) =>
			{
				var userDbAccount = Account.GetByUserIdentity((ClaimsIdentity) context.User.Identity);

				if(context.User.Identity.IsAuthenticated)
					Account.UpdateOrRegister((ClaimsIdentity)context.User.Identity);

				await next.Invoke();
			});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
