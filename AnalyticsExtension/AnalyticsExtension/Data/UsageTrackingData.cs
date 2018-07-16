using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsExtension.Data
{
  public class UsageTrackingData
  {
    public DateTime SessionStart { get; set; }
    public DateTime SessionEnd { get; set; }
    public string DynamoVersion { get; set; }
    public string Filename { get; set; }
    public List<NodeData> Nodes { get; set; }

    public UsageTrackingData()
    {
      Nodes = new List<NodeData>();
    }

  }
}
