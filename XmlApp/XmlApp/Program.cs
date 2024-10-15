using Microsoft.EntityFrameworkCore;
using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories;
using XmlApp.Services.Abstract;
using XmlApp.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));

});

builder.Services.AddTransient<IFaturaRepository, FaturaRepository>();
builder.Services.AddTransient<IFaturabilgileriRepository, FaturaBilgileriRepository>();
builder.Services.AddTransient<IFonDekontRepository, FonDekontRepository>();
builder.Services.AddTransient<IGenelBilgilerRepository, GenelBilgilerRepository>();
builder.Services.AddTransient<IGumrukMudurluguOnayiRepository, GumrukMudurluguOnayiRepository>();
builder.Services.AddTransient<IGumrukMuhafazaMudurluguOnayiRepository, GumrukMuhafazaMudurluguOnayiRepository>();
builder.Services.AddTransient<IIslemKonusuBilgileriRepository, IslemKonusuBilgileriRepository>();
builder.Services.AddTransient<IIslemTuruBilgileriRepository, IslemTuruBilgileriRepository>();
builder.Services.AddTransient<IIslemYonuBilgileriRepository, IslemYonuBilgileriRepository>();
builder.Services.AddTransient<ITeslimAdresiRepository, TeslimAdresiRepository>();
builder.Services.AddTransient<IKarsiFirmaBilgileriRepository, KarsiFirmaBilgileriRepository>();
builder.Services.AddTransient<IMalKalemRepository, MalKalemRepository>();
builder.Services.AddTransient<IMalKalemBilgileriRepository, MalKalemBilgileriRepository>();
builder.Services.AddTransient<ISBIFBilgileriRepository, SBIFBilgileriRepository>();
builder.Services.AddTransient<ISbifBilgiFisiRepository, SbifBilgiFisiRepository>();
builder.Services.AddTransient<ISbifGumrukBilgileriRepository, SbifGumrukBilgileriRepository>();
builder.Services.AddTransient<ISevkiyatSekliRepository, SevkiyatSekliRepository>();
builder.Services.AddTransient<ITalepEdilenIsleticiHizmetleriRepository, TalepEdilenIsleticiHizmetleriRepository>();
builder.Services.AddTransient<ISBIFService, SBIFService>();
builder.Services.AddTransient<DataContext, DataContext>();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await next.Invoke();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
