using System.Collections.Generic;

namespace anki_gen_net.Commands
{
    public class AbstractCommand : ICommand
    {
        protected AbstractCommand(Config config)
        {
            BaseConfig = config;
        }

        protected AbstractCommand()
        {
        }

        protected Config BaseConfig { get; }

        public virtual void Execute()
        {
        }

        public virtual void Execute(ref List<string> fields)
        {
        }
    }
}