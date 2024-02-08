using Backend.Infra.Database;
using Backend.Repositories;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Remember to configure cors in application */
/* Remember to configure the password hash with BCrypt */
/* Remember to configure JsonWebToken do allow an user stay logged in */

builder.Services.AddDbContext<SqlServerContext>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ChatRepository>();
builder.Services.AddTransient<ChatService>();
builder.Services.AddTransient<MessageRepository>();
builder.Services.AddTransient<MessageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
