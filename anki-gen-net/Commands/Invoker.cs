using System;

namespace anki_gen_net.Commands
{
    // Отправитель связан с одной или несколькими командами. Он отправляет
    // запрос команде.
    public class Invoker
    {
        private ICommand _onFinish;
        private ICommand _onStart;

        // Command initiating.
        public void SetOnStart(ICommand command)
        {
            _onStart = command;
        }

        public void SetOnFinish(ICommand command)
        {
            _onFinish = command;
        }

        // Отправитель не зависит от классов конкретных команд и получателей.
        // Отправитель передаёт запрос получателю косвенно, выполняя команду.
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: start");

            if (_onStart is ICommand) _onStart.Execute();

            Console.WriteLine("Invoker: doing...");

            Console.WriteLine("Invoker: finish");

            if (_onFinish is ICommand) _onFinish.Execute();
        }
    }
}