using ApiAgenda.Model;
using ApiAgenda.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiAgenda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository _repository;

        public AgendaController(IAgendaRepository repositoryAgenda)
        {
            this._repository = repositoryAgenda;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agendamentos = await _repository.GetAllAgendamentos();
            return agendamentos.Any() ? Ok(agendamentos) : NoContent();
        }

        [HttpGet("{inicio}&{fim}")]
        public async Task<IActionResult> GetRange(string inicio, string fim)
        {
            bool verificaData = ValidaData(inicio) && ValidaData(fim);
            if (!verificaData) return NotFound("Por favor, insira datas válida!");

            var agendamentos = await _repository.GetRangeAgendamentos(inicio, fim);
            return agendamentos.Any() ? Ok(agendamentos) : NoContent();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agendamento = await _repository.GetIdAgendamentos(id);
            return agendamento != null ? Ok(agendamento) : NotFound("Agendamento não encontrado!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Agenda agenda)
        {
            bool verificaData = ValidaData(agenda.DataInicio.ToString()) && ValidaData(agenda.DataConclusao.ToString()) && ValidaData(agenda.DataPrazo.ToString());
            if (!verificaData) return NotFound("Por favor, insira datas válidas!");

            var agendamentos = await _repository.GetRangeAgendamentos(agenda.DataInicio.ToString(), agenda.DataConclusao.ToString());
            if (agendamentos.Any()) return NotFound("Não é possível cadastrar mais de uma atividade no mesmo horário. Por favor, verifique sua agenda!");

            _repository.CreateAgendamento(agenda);
            return await _repository.SaveChangesAsync() ? Ok("Agendamento cadastrado com Sucesso!") : BadRequest("Erro ao cadastrar o Agendamento!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Agenda agenda)
        {
            var agendamento = await _repository.GetIdAgendamentos(id);
            if (agendamento == null) return NotFound("Agendamento não encontrado!");

            bool verificaData = ValidaData(agenda.DataInicio.ToString()) && ValidaData(agenda.DataConclusao.ToString()) && ValidaData(agenda.DataPrazo.ToString());
            if (!verificaData) return NotFound("Digite datas válidas!");

            var agendamentos = await _repository.GetRangeAgendamentos(agenda.DataInicio.ToString(), agenda.DataConclusao.ToString());
            if (agendamentos.Where(a => a.Id != agenda.Id).Any()) return NotFound("Não é possível cadastrar mais de uma atividade no mesmo horário. Por favor, verifique sua agenda!");

            agendamento.Titulo = agenda.Titulo ?? agendamento.Titulo;
            agendamento.Descricao = agenda.Descricao ?? agendamento.Descricao;
            agendamento.DataPrazo = agenda.DataPrazo;
            agendamento.DataInicio = agenda.DataInicio;
            agendamento.DataConclusao = agenda.DataConclusao;
            agendamento.Responsavel = agenda.Responsavel ?? agendamento.Responsavel;

            _repository.UpdateAgendamento(agendamento);
            return await _repository.SaveChangesAsync() ? Ok("Agendamento atualizado com Sucesso!") : BadRequest("Erro ao atualizar o Agendamento!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var agendamento = await _repository.GetIdAgendamentos(id);
            if (agendamento == null) return NotFound("Agendamento não encontrado!");

            _repository.DeleteAgendamento(agendamento);
            return await _repository.SaveChangesAsync() ? Ok("Agendamento excluído com Sucesso!") : BadRequest("Erro ao excluir o Agendamento!");
        }

        private static bool ValidaData(string dt)
        {
            DateTime tmpDt;
            return DateTime.TryParse(dt, out tmpDt);
        }
    }
}