using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Dtos;
using DevFreela.Core.Repositories;
using DevFreela.Infrastucture.Persistence;
using DevFreela.Infrastucture.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

//Commands - Projects
builder.Services.AddScoped<IRequestHandler<CreateProjectCommand, int>, CreateProjectCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateProjectCommentCommand, Unit>, CreateProjectCommentCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateProjectCommand, Unit>, UpdateProjectCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteProjectCommand, Unit>, DeleteProjectCommandHandler>();
builder.Services.AddScoped<IRequestHandler<StartProjectCommand, Unit>, StartProjectCommandHandler>();
builder.Services.AddScoped<IRequestHandler<FinishProjectCommand, Unit>, FinishProjectCommandHandler>();

//Queries - Projects
builder.Services.AddScoped<IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>, GetAllProjectsQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetProjectByIdQuery, ProjectDetailsDto>, GetProjecByIdQueryHandler>();

//Commands - Users
builder.Services.AddScoped<IRequestHandler<CreateUserCommand, int>, CreateUserCommandHandler>();

//Queries - Users
builder.Services.AddScoped<IRequestHandler<GetUserByIdQuery, UserDto>, GetUserByIdQueryHandler>();


//Queries - Skills
builder.Services.AddScoped<IRequestHandler<GetAllSkillsQuery, List<SkillDto>>, GetAllSkillsQueryHandler>();

//Repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

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
