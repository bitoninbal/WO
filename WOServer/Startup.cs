﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WODataAccess.DataAccess;
using WODataAccess.Interfaces;
using WOServer.Services;

namespace WOServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddScoped<ICommentsDataAccess, CommentsDataAccess>();
            services.AddScoped<ITasksDataAccess, TasksDataAccess>();
            services.AddScoped<IUserDataAccess,  UserDataAccess>();
            services.AddScoped<IUpdatesDataAccess, UpdatesDataAccess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CommntsService>();
                endpoints.MapGrpcService<DbUpdatesService>();
                endpoints.MapGrpcService<TasksService>();
                endpoints.MapGrpcService<UsersService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
