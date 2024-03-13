
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Entities;

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ScanMachineContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DBConnetion")));
            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // JSON serializer
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
                options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            var app = builder.Build();

            //Enable CROS
            app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

