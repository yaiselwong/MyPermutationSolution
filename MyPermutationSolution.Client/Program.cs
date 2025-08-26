using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyPermutationSolution.Client;
using MyPermutationSolution.Client.Interfaces;
using MyPermutationSolution.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ICustomHttpClient, CustomHttpClient>();
builder.Services.AddScoped<IPermutationService, PermutationService>();

await builder.Build().RunAsync();
