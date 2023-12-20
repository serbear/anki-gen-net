using System.Collections.Generic;

namespace anki_gen_net.Commands
{
    public class GeneratorInvoker
    {
        private List<ICommand> _commandList;
        private ICommand _onGenerate;

        /// <summary>
        ///     Set a target command for the invoker.
        /// </summary>
        /// <param name="command"></param>
        private void SetOnGenerate(ICommand command)
        {
            _onGenerate = command;
        }

        /// <summary>
        ///     Set a target command list for the invoker.
        /// </summary>
        /// <param name="commandList"></param>
        public void SetOnGenerate(List<ICommand> commandList)
        {
            _commandList = commandList;
        }

        /// <summary>
        ///     Run multiple commands.
        /// </summary>
        /// <param name="fields">
        ///     A reference to the collection where will be stored the result.
        /// </param>
        public void BatchInvoke(ref List<string> fields)
        {
            foreach (var command in _commandList)
            {
                SetOnGenerate(command);
                command.Execute(ref fields);
            }
        }
    }
}