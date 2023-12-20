using System.Collections.Generic;
using System.Windows.Forms;

namespace anki_gen_net.Commands
{
    public class CopyToClipboardCommand : AbstractCommand
    {
        public override void Execute(ref List<string> fields)
        {
            // When the method is calling, there must be just one element in
            // the 'fields' collection.
            // todo: Check for the collection lenght.

            Clipboard.SetText(fields[0]);
        }
    }
}