using System.Collections.Generic;

namespace anki_gen_net
{
    public class WordListItem
    {
        public WordListItem(IDictionary<string, object> data)
        {
            var copy = new Dictionary<string, object>(data);
            Data = copy;
        }

        public Dictionary<string, object> Data { get; }

        public override string ToString()
        {
            var n = Data["main_entity"];
            var m = Data["meaning"].ToString().ToUpper();
            var listItemText = m.Equals("") ? $"{n}" : $"{n} ({m})";
            return listItemText;
        }
    }
}