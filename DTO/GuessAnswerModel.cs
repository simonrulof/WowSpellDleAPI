using WowSpellDleAPI.Enum;

namespace WowSpellDleAPI.DTO;


public class GuessAnswerModel
{
    public GuessAnswerModel()
    {
    }

    int Id { get; set; }

    public GuessFieldAnswer Spell { get; set; }

    public GuessFieldAnswer Class { get; set; }

    public GuessFieldAnswer Spec { get; set; }

    public GuessFieldAnswer School { get; set; }

    public GuessFieldAnswer UseType { get; set; }

    public GuessFieldAnswerRelative Cooldown { get; set; }

}