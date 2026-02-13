namespace WowSpellDleAPI.DTO
{
    public class FirstHintModel
    {
        public FirstHintModel(char _hintFr, char _hintEn)
        {
            HintFr = _hintFr;
            HintEn = _hintEn;
        }

        public char HintFr { get; set; }
        public char HintEn { get; set; }
    }
}
