using System.Collections.Generic;

namespace anki_gen_net.Commands
{
    public class GenerateNoTagFieldCommand : AbstractCommand
    {
        private readonly string _value;

        public GenerateNoTagFieldCommand(string value)
        {
            _value = value;
        }

        public override void Execute(ref List<string> fields)
        {
            fields.Add(_value);
        }
    }
}