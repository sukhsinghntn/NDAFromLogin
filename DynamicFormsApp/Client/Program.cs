using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DynamicFormsApp.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1) Register a named HttpClient for your Server API
builder.Services.AddHttpClient("DynamicFormsApp.ServerAPI", client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
);

// 2) Supply the default HttpClient from the factory
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("DynamicFormsApp.ServerAPI")
);

await builder.Build().RunAsync();
