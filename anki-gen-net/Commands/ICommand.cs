using System.Collections.Generic;

namespace anki_gen_net.Commands
{
    // The command's interface declares a method for executing a command.
    public interface ICommand
    {
        void Execute();

        void Execute(ref List<string> fields);
    }
}