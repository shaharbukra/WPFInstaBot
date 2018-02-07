using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InstaBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.Initialize();
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.SetLicense("runtimelite,1000,rud1687177477,none,PM0RJAY3FYY7GTJ89211");

            if (!Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.IsInitialized)
                Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.InstallPath = Directory.GetCurrentDirectory();
        }
    }
}
