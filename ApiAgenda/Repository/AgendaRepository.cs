using ApiAgenda.Data;
using ApiAgenda.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiAgenda.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly AgendaContext _context;

        public AgendaRepository(AgendaContext context)
        {
            this._context = context;
        }

        async Task<IEnumerable<Agenda>> IAgendaRepository.GetAllAgendamentos()
        {
            return await _context.Agenda.OrderBy(a => a.DataInicio).ToListAsync();
        }

        async Task<IEnumerable<Agenda>> IAgendaRepository.GetRangeAgendamentos(string i, string f)
        {
            DateTime inicio = DateTime.Parse(i);
            DateTime fim = DateTime.Parse(f);

            return await _context.
                            Agenda.Where(
                                a => a.Status
                                && (a.DataInicio <= inicio && a.DataConclusao >= inicio)
                                || (a.DataInicio <= fim && a.DataConclusao >= fim)
                                || (a.DataInicio >= inicio && a.DataConclusao <= fim)
                            )
                            .OrderBy(a => a.DataInicio).
                            ToListAsync();
        }

        async Task<Agenda> IAgendaRepository.GetIdAgendamentos(int id)
        {
            return await _context.Agenda.Where(a => a.Id == id).FirstAsync();
        }

        void IAgendaRepository.CreateAgendamento(Agenda agenda)
        {
            _context.Add(agenda);
        }

        void IAgendaRepository.UpdateAgendamento(Agenda agenda)
        {
            _context.Update(agenda);
        }

        void IAgendaRepository.DeleteAgendamento(Agenda agenda)
        {
            _context.Remove(agenda);
        }

        async Task<bool> IAgendaRepository.SaveChangesAsync()
        {
            Console.Write(_context.SaveChangesAsync());
            return await _context.SaveChangesAsync() > 0;
        }
    }
}