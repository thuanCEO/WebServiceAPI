
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Entities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ScanMachineContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DBConnetion")));
            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "API Admin", Version = "v1", 
                    Description = "New Swagger Document",
                  
                });
               var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }
                );

            // JSON serializer
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
                options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            var app = builder.Build();

            //Enable CROS
            app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
//app.UseCors(c => c.WithOrigins("https://main.dbzcuscwyxkm6.amplifyapp.com/").AllowAnyHeader().AllowAnyMethod());

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

