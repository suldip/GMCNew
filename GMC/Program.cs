using GMC.BL.GMC;
using GMC.DAL;
using GMC.DAL.Repository.GMC;
using GMC.Helper;
using GMC.Interface;
using GMC.Interface.GMC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<SessionAuthFilter>();
});
builder.Services.AddScoped<IGMCUploader, GMCUploaderBL>();
builder.Services.AddScoped<IGMCUploaderRepo, GMCUploaderRepo>();
builder.Services.AddScoped<GMCUploaderDAL>();
builder.Services.AddScoped<IGMCCalculatorDetails, GMCCalculatorDetailsBL>();
builder.Services.AddScoped<IGMCCalculatorDetailsRepo, GMCCalculatorDetailsRepo>();
builder.Services.AddScoped<GMCCalculatorDetailsDAL>();
builder.Services.AddScoped<ILoginBL, LoginBL>();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<LoginDAL>();
builder.Services.AddScoped<GMC.Models.GMC.BusinessLogic.IUserRegistrationBL, GMC.Models.GMC.BusinessLogic.UserRegistrationBL>();
builder.Services.AddScoped<IUserRegistrationRepo, UserRegistrationRepo>();
builder.Services.AddScoped<UserRegistrationDAL>();
builder.Services.AddScoped<GMC.DAL.Repository.GMC.MasterDAL>();
builder.Services.AddScoped<ISqlHelperQuery, SqlHelperQuery>();
builder.Services.AddScoped<CommonBAL>();
builder.Services.AddHttpClient();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UsePathBase("/GMC");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Enable Session
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
