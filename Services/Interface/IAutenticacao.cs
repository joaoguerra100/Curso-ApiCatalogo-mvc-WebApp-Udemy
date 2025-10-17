using CategoriasMvc.Models;

namespace CategoriasMvc.Services.Interface;

public interface IAutenticacao
{
    Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel usuarioVM);
}