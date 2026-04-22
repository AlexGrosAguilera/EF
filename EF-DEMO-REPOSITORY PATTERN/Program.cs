/*
 CREATE USER 'appuser'@'%' IDENTIFIED BY '1234';
GRANT ALL PRIVILEGES ON EMPRESA.* TO 'appuser'@'%';
FLUSH PRIVILEGES;
 */

/*
 * Scaffold-DbContext "server=localhost;Port=3306;
 * UserId=appuser;Password=1234;Database=EMPRESA;allowuservariables=True" Pomelo.EntityFrameworkCore.MySql -Outputdir MODEL
 */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using System;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        "server=localhost;database=EMPRESA;user=appuser;password=1234;",
        new MySqlServerVersion(new Version(8, 0, 21))
    ));

services.AddScoped<IEmployeeRepository, EmployeeRepository>();
services.AddScoped<EmployeeService>();

var provider = services.BuildServiceProvider();
var service = provider.GetRequiredService<EmployeeService>();
string option="";
while (option!="0")
{
   Console.WriteLine("\n1. Llistar 2. Crear 3. Eliminar 0. Sortir");
   option = Console.ReadLine();

    if (option == "1")
    {
        var list = await service.GetAllAsync();
        foreach (var e in list)
            Console.WriteLine($"{e.EmployeeId} {e.FirstName} {e.LastName}");
    }
    else if (option == "2")
    {
        Console.Write("ID: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nom: ");
        var name = Console.ReadLine();

        await service.AddAsync(new Employee
        {   EmployeeId = id,
            FirstName = name,
            LastName = "Test",
            Email = "test@test.com",
            JobTitle = "IT_PROG",
            Salary=1000,
            HireDate= DateTime.Now

        });
    }
    else if (option == "3")
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());
        await service.DeleteAsync(id);
    }
    
}
