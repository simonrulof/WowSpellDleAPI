using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WowSpellDleAPI.Data;
using WowSpellDleAPI.DTO;
using WowSpellDleAPI.Logic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WowSpellDleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpellsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private SpellLogic _logic;
        public SpellsController(ApplicationDbContext context)
        {
            _context = context;
            _logic = new SpellLogic(context);
        }

        [HttpGet("all")]
        public ActionResult<List<SpellModel>> GetAllSpells()
        {
            try
            {
                return Ok(_logic.GetAllSpells());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving spells", message = ex.Message });
            }
        }

        [HttpGet("guess/{id}")]
        public ActionResult<GuessAnswerModel> GuessAnswer(int id)
        {
            try
            {
                return Ok(_logic.GuessAnswer(id, DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while guessing the answer", message = ex.Message });
            }
        }

        [HttpGet("guess/{id}/{date}")]
        public ActionResult<GuessAnswerModel> GuessAnswerWithDate(int id, DateTime date)
        {
            try
            {
                return Ok(_logic.GuessAnswer(id, date));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while guessing the answer", message = ex.Message });
            }
        }
    }
}
