using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public class SecretKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _vaultUrl;
    private readonly string _secretName;

    public SecretKeyMiddleware(RequestDelegate next, string vaultUrl, string secretName)
    {
        _next = next;
        _vaultUrl = vaultUrl;
        _secretName = secretName;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var secretKey = context.Request.Query["secretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Please provide the secret key");
            return;
        }

        var client = new SecretClient(new Uri(_vaultUrl), new DefaultAzureCredential());
        var secret = await client.GetSecretAsync(_secretName);

        if (secret.Value != secretKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid secret key");
            return;
        }

        await _next(context);
    }
}