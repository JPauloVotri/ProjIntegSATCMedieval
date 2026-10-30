using MedievalApi.Data;
using MedievalApi.Middlewares;
using MedievalApi.Services;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

public partial class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUsuarioService, UsuarioService>();
        builder.Services.AddScoped<IProdutoService, ProdutoService>();
        builder.Services.AddScoped<ICotacaoService, CotacaoService>();
        builder.Services.AddScoped<ICotacaoItemService, CotacaoItemService>();
        builder.Services.AddScoped<ICotacaoItemFornecedorService, CotacaoItemFornecedorService>();
        builder.Services.AddScoped<IFornecedorService, FornecedorService>();
        builder.Services.AddScoped<IEstoqueService, EstoqueService>();
        builder.Services.AddScoped<IGrupoProdutoService, GrupoProdutoService>();
        builder.Services.AddScoped<IMovimentacaoEstoqueService, MovimentacaoEstoqueService>();
        builder.Services.AddScoped<IPedidoCompraService, PedidoCompraService>();
        builder.Services.AddScoped<IPedidoCompraItemService, PedidoCompraItemService>();
        builder.Services.AddScoped<ISaidaEstoqueService, SaidaEstoqueService>();
        builder.Services.AddScoped<ISaidaEstoqueItemService, SaidaEstoqueItemService>();
        builder.Services.AddScoped<IUnidadeMedidaService, UnidadeMedidaService>();

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("ReactPolicy", policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:5173",
                        "http://127.0.0.1:5173"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        WebApplication app = builder.Build();

        app.UseCors("ReactPolicy");
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("MedievalApi")
                    .WithTheme(ScalarTheme.Moon)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });
        }

        app.MapControllers();
        app.Run();
    }
}
