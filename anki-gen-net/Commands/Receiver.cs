using System;

namespace anki_gen_net.Commands
{
    // Классы Получателей содержат некую важную бизнес-логику. Они умеют
    // выполнять все виды операций, связанных с выполнением запроса. Фактически,
    // любой класс может выступать Получателем.
    public class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($@"Receiver: Working on ({a})");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($@"Receiver: Also working on ({b})");
        }
    }
}