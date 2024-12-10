using PaymentGateway.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();
app.MapControllers();
app.Run();