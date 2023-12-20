using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace anki_gen_net.Commands
{
    public class GenerateMarkersFieldCommand : AbstractCommand
    {
        private readonly CheckedListBox.CheckedItemCollection _markers;
        private readonly List<object> _markersObject;
        private string _template;

        public GenerateMarkersFieldCommand(
            CheckedListBox.CheckedItemCollection markers) : base(Program.config)
        {
            _markers = markers;
        }

        public GenerateMarkersFieldCommand(List<object> checkedItems) : base(
            Program.config)
        {
            _markersObject = checkedItems;
        }

        public override void Execute(ref List<string> fields)
        {
            var tagFormatter = new TagFormatter(BaseConfig);

            var sb = new StringBuilder();


            if (_markers != null)
                foreach (var x in _markersObject.Select(marker =>
                             tagFormatter.TagValue(
                                 marker.ToString(),
                                 BaseConfig.EntityTag)))
                    sb.Append(x);
            else
                foreach (var x in _markersObject.Select(marker =>
                             tagFormatter.TagValue(
                                 marker.ToString(),
                                 BaseConfig.EntityTag)))
                    sb.Append(x);

            _template = BaseConfig.MarkersTemplate;

            _template = tagFormatter.TagValueByRef(
                sb.ToString(),
                _template,
                "{markers}"
            );

            fields.Add(_template);
        }
    }
}