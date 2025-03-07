using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MvcLab4.Entities;
using MvcLab4.Identity;
using MvcLab4.Repository;
using MvcLab4.Requirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region LessonOne


builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddHttpContextAccessor();

#endregion

#region LessonTwo



builder.Services.AddDbContext<AppIdentityDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region Lesson3

// 3. Uygulama ile alakal� k�s�m

// Uygulamam�za User ve Role s�n�flar�n� tan�tt�k
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
{
  opt.SignIn.RequireConfirmedEmail = false; // email confirm etmeden login etsin
  opt.User.RequireUniqueEmail = true; // email unique olmal�
  opt.Password.RequireDigit = true;
  // parolada numeric alan olaml� gibi ayarlar� yapabiliyoruz.
  
}).AddEntityFrameworkStores<AppIdentityDbContext>();



// Authentication Scheme CookieAuthentication belirttik.
builder.Services.AddAuthentication()
     .AddCookie();


//// Authentication Cookie ayarlar�
builder.Services.ConfigureApplicationCookie(option => //cookie burada yarat�l�r.
{
  option.Cookie.Name = "UserLoginCookie";
  option.LoginPath = "/Auth/Login";
  option.AccessDeniedPath = "/Auth/AccessDenied";
});

// uygulamaya yetkilendirme servisi
builder.Services.AddAuthorization(opt =>
{
  
  opt.AddPolicy("AdminOrManager", policy => policy.RequireRole("Manager", "Admin")); 
  opt.AddPolicy("DomainCheck", policy => policy.AddRequirements(new SpesificDomainRequirement("neominal.com")));

});

// DomainRequirementHandler servisi sisteme tan�tt�k
builder.Services.AddTransient<IAuthorizationHandler, DomainRequirementHandler>();

#endregion


var app = builder.Build();

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


#region Lesson3


// yoksa login olam�yoruz. cookie olu�uyor ama [Authorize] attribute �al��m�yor
app.UseAuthentication();

//Yekilendirme Middleware
app.UseAuthorization();

#endregion



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
