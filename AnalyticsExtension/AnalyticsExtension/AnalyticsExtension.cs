using System;
using System.Windows;
using System.Windows.Controls;
using Dynamo.Wpf.Extensions;
using Dynamo.Extensions;
using Dynamo.ViewModels;
using Dynamo.Models;
using Dynamo.Graph.Workspaces;
using AnalyticsExtension.Data;
using System.Linq;

using AnalyticsExtension.Data;
using Dynamo.Graph.Nodes.ZeroTouch;

namespace AnalyticsExtension
{
  public class AnalyticsExtension : IViewExtension
  {

    private UsageTrackingData tracking;
    private MenuItem menuItem;
    private ViewLoadedParams readyParams;

    public void Dispose()
    {
    }

    public void Startup(ViewStartupParams p)
    {
      tracking = new UsageTrackingData
      {
        SessionStart = DateTime.Now,
        DynamoVersion = p.DynamoVersion.ToString()
      };
      p.CustomNodeManager.AddUninitializedCustomNodesInPath(@"C:\code\DynamoExtensionTests\packages", false, true);
    }

    public void Loaded(ViewLoadedParams p)
    {
      readyParams = p;

      
     
      menuItem = new MenuItem { Header = "Show ANALYTICS" };
      menuItem.Click += (sender, args) =>
          {
            var viewModel = new AnalyticsExtensionViewModel(p, tracking);
            var window = new AnalyticsExtensionUi
            {
              // Set the data context for the main grid in the window.
              MainGrid = { DataContext = viewModel },
              
              // Set the owner of the window to the Dynamo window.
              Owner = p.DynamoWindow
            };
            window.AddNode.Click += AddNode;

            window.Left = window.Owner.Left + 400;
            window.Top = window.Owner.Top + 200;

            // Show a modeless window.
            window.Show();
          };
      p.AddMenuItem(MenuBarType.View, menuItem);


    }
    private void AddNode(object sender, RoutedEventArgs e)
    {
      //var dynViewModel = readyParams.DynamoWindow.DataContext as DynamoViewModel;
      //var dm = dynViewModel.Model as DynamoModel;
      //var addNode = new DSFunction(dm.LibraryServices.GetAllFunctionDescriptors("+").First());
      
      //dm.ExecuteCommand(new DynamoModel.CreateNodeCommand(addNode, 0, 0, true, false));


    }

    public void Shutdown()
    {
      var dynViewModel = readyParams.DynamoWindow.DataContext as DynamoViewModel;

      tracking.SessionEnd = DateTime.Now;
      tracking.Filename = readyParams.CurrentWorkspaceModel.FileName;
      tracking.DynamoVersion = dynViewModel.HostVersion;
    }

    public string UniqueId
    {
      get
      {
        return Guid.NewGuid().ToString();
      }
    }

    public string Name
    {
      get
      {
        return "Sample View Extension";
      }
    }


  }
}
