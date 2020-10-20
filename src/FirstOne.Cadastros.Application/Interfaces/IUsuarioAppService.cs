using FirstOne.Cadastros.Application.ViewModels;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task Adicionar(UsuarioViewModel usuarioViewModel);
    }
}
