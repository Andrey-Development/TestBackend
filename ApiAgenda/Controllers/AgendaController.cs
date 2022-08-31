using ApiAgenda.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiAgenda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private static List<Agenda> Agendas()
        {
            return new List<Agenda>{
                new Agenda{
                    Id = 1,
                    Titulo = "Reunião Henrique",
                    Descricao = "Reunião com o Henrique para feedback do códigin...",
                    Status = "Working",
                    dataPrazo = DateTime.Parse("04/09/2022 23:59:59.999"),
                    DataInicio = DateTime.Parse("05/09/2022 17:00:00"),
                    DataConclusao = DateTime.Parse("05/09/2022 17:20:00"),
                    Responsavel = "Andrey Nunes",
                    Created_at = DateTime.Now,
                }
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Agendas());
        }

        [HttpPost]
        public IActionResult Post(Agenda agenda)
        {
            var list = Agendas();
            list.Add(agenda);

            return Ok(list);
        }
    }
}