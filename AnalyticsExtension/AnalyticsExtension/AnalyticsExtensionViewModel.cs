using System;
using Dynamo.Core;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;
using Dynamo.Models;
using Dynamo.Graph.Nodes.ZeroTouch;
using System.Linq;
using Dynamo.Engine;

using AnalyticsExtension.Data;
using Dynamo.Graph.Workspaces;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;

namespace AnalyticsExtension
{
  class AnalyticsExtensionViewModel : NotificationObject, IDisposable
  {
    private string activeNodeTypes;
    private ReadyParams readyParams;
    private UsageTrackingData tracking;

    // Displays active nodes in the workspace
    public string ActiveNodeTypes
    {
      get
      {
        activeNodeTypes = getNodeTypes();
        return activeNodeTypes;
      }
    }

   

    // Helper function that builds string of active nodes
    public string getNodeTypes()
    {
      tracking.Nodes.Clear();
      

      foreach (NodeModel node in readyParams.CurrentWorkspaceModel.Nodes)
      {
        tracking.Nodes.Add(new NodeData {
          Name = node.Name,
          Category = node.Category

      });
      }

      return "aaa";
    }

  
    public AnalyticsExtensionViewModel(ReadyParams p, UsageTrackingData _tracking)
    {
      readyParams = p;
      tracking = _tracking;
      //p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodesChanged;
      p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodesChanged;
    }

    private void CurrentWorkspaceModel_NodesChanged(NodeModel obj)
    {
      RaisePropertyChanged("ActiveNodeTypes");
    }

    public void Dispose()
    {
      readyParams.CurrentWorkspaceModel.NodeAdded -= CurrentWorkspaceModel_NodesChanged;
      readyParams.CurrentWorkspaceModel.NodeRemoved -= CurrentWorkspaceModel_NodesChanged;
    }
  }
}
