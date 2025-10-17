using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CategoriasMvc.Models;
using CategoriasMvc.Services.Interface;

namespace CategoriasMvc.Services;

public class ProdutoService : IProdutoService
{
    private readonly IHttpClientFactory _clienteFactory;
    private const string apiEndpoint = "/api/v1/Produtos/";

    private readonly JsonSerializerOptions _options;
    private ProdutoViewModel produtoVM;
    private IEnumerable<ProdutoViewModel> produtosVM;

    public ProdutoService(IHttpClientFactory clienteFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clienteFactory = clienteFactory;
    }

    public async Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token)
    {
        var client = _clienteFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtosVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtosVM;
    }

    public async Task<ProdutoViewModel> GetProdutoPorId(int id, string token)
    {
        var client = _clienteFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtoVM;
    }

    public async Task<ProdutoViewModel> CriarProduto(ProdutoViewModel produtoVM, string token)
    {
        var client = _clienteFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        var produto = JsonSerializer.Serialize(produtoVM);
        StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtoVM;
    }

    public async Task<bool> AtualizarProduto(int id, ProdutoViewModel produtoVM, string token)
    {
        var client = _clienteFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, produtoVM))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public async Task<bool> DeletarProduto(int id, string token)
    {
        var client = _clienteFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}