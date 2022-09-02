using ApiAgenda.Model;

namespace ApiAgenda.Repository
{
    public interface IAgendaRepository
    {
        Task<IEnumerable<Agenda>> GetAllAgendamentos();
        Task<IEnumerable<Agenda>> GetRangeAgendamentos(string inicio, string fim);
        Task<Agenda> GetIdAgendamentos(int id);
        void CreateAgendamento(Agenda agenda);
        void UpdateAgendamento(Agenda agenda);
        void DeleteAgendamento(Agenda agenda);
        Task<bool> SaveChangesAsync();
    }
}