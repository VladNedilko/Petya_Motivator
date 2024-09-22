using Microsoft.Extensions.Configuration;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("companies.json", optional: false, reloadOnChange: true)
    .AddXmlFile("companies.xml", optional: false, reloadOnChange: true)
    .AddIniFile("companies.ini", optional: false, reloadOnChange: true);
var app = builder.Build();

app.MapGet("/", (IConfiguration config) =>
{
    var employeesMicrosoft = config.GetSection("Microsoft:Employees").Get<int>();
    var employeesApple = config.GetSection("Apple:Employees").Get<int>();
    var employeesGoogle = config.GetSection("Google:Employees").Get<int>();

    var maxEmployees = Math.Max(employeesMicrosoft, Math.Max(employeesApple, employeesGoogle));
    string company = maxEmployees == employeesMicrosoft ? "Microsoft"
                   : maxEmployees == employeesApple ? "Apple"
                   : "Google";

    return $"������� � ��������� ������� �����������: {company}";
});
builder.Configuration.AddJsonFile("mydata.json", optional: false, reloadOnChange: true);

app.MapGet("/mydata", (IConfiguration config) =>
{
    var name = config["Name"];
    var age = config["Age"];
    var proffesion = config["Proffesion"];

    return $"��'�: {name}, ³�: {age}, ��� �� � �� �����: {proffesion}";
});

app.Run();