using FirstOne.Cadastros.Domain.Entities;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
    }
}
