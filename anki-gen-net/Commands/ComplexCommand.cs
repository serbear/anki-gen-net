// using System;
//
// namespace anki_gen_net.Commands
// {
//     public class ComplexCommand: ICommand
//     {
//         private Receiver _receiver;
//
//         // The data about context that is necessary to run methods of the
//         // receiver.
//         private string _a;
//         private string _b;
//
//         public ComplexCommand(Receiver receiver, string a, string b)
//         {
//             this._receiver = receiver;
//             this._a = a;
//             this._b = b;
//         }
//         public void Execute()
//         {
//             Console.WriteLine("ComplexCommand");
//             this._receiver.DoSomething(this._a);
//             this._receiver.DoSomethingElse(this._b);
//         }
//     }
// }

