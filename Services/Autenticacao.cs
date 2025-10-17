using System.Text;
using System.Text.Json;
using CategoriasMvc.Models;
using CategoriasMvc.Services.Interface;

namespace CategoriasMvc.Services;

public class Autenticacao : IAutenticacao
{
    private readonly IHttpClientFactory _clienteFactory;
    private const string apiEndpointAutentica = "/api/v1/AuthTeste/login/";
    private readonly JsonSerializerOptions _options;
    private TokenViewModel tokenUsuario;

    public Autenticacao(IHttpClientFactory clienteFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clienteFactory = clienteFactory;
    }

    public async Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel usuarioVM)
    {
        var client = _clienteFactory.CreateClient("AutenticaApi");
        var usuario = JsonSerializer.Serialize(usuarioVM);
        StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpointAutentica, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                tokenUsuario = await JsonSerializer.DeserializeAsync<TokenViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return tokenUsuario;
    }
}