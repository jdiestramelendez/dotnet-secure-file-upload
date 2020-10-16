using System.Collections.Generic;
using System.Text;

namespace SecureFileUpload.Models
{
    public class OperationsGroup
    {
        public List<OperationResult> Operations { get; set; }

        public OperationsGroup()
        {
            Operations = new List<OperationResult>();
        }

        public long TotalElapsedTimeInMilliseconds
        {
            get
            {
                long total = 0;
                Operations.ForEach(o => total += o.ElapsedTimeInMilliseconds);
                return total;
            }
        }

        public OperationResult Add(string name, long timeinMilliseconds)
        {
            var o = new OperationResult { Name = name, ElapsedTimeInMilliseconds = timeinMilliseconds };
            Operations.Add(o);
            return o;
        }

        public override string ToString()
        {
            var text = new StringBuilder();

            text.AppendLine("<br/><br/><b>Operations:</b><ul>");
            Operations.ForEach(o => text.AppendLine($"<li>{o.Name}, Duration: {o.ElapsedTimeInMilliseconds:N0} ms</li>"));
            text.AppendLine($"</ul><br/>Total Operations {Operations.Count}");
            text.AppendLine($"<br/>Total Duration {TotalElapsedTimeInMilliseconds:N0}");

            return text.ToString();
        }
    }
}