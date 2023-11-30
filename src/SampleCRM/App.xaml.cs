﻿#if OPENSILVER

using OpenRiaServices.Client;
using OpenRiaServices.Client.Authentication;
using OpenRiaServices.Client.DomainClients;
using SampleCRM.Web;

#endif

using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Windows;

namespace SampleCRM
{
    public partial class App : Application
    {
        public const string Title = "Sample CRM";

        public App()
        {
            Startup += Application_Startup;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();

            var httpClientHandler = new HttpClientHandler();
            if (RuntimeInformation.ProcessArchitecture != Architecture.Wasm)
            {
                httpClientHandler.UseCookies = true;
                httpClientHandler.CookieContainer = new System.Net.CookieContainer();
            }

            DomainContext.DomainClientFactory = new BinaryHttpDomainClientFactory(new Uri("http://localhost:5282/"), httpClientHandler);

            var webContext = new WebContext();
            webContext.Authentication = new FormsAuthentication()
            {
                DomainContext = new AuthenticationContext()
            };
            //webContext.Authentication = new WindowsAuthentication();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Host.Settings.EnableOptimizationWhereCollapsedControlsAreNotRendered = true;

            // This will enable you to bind controls in XAML to WebContext.Current properties.
            Resources.Add("WebContext", WebContext.Current);

            // This will automatically authenticate a user when using Windows authentication or when the user chose "Keep me signed in" on a previous login attempt.
            // WebContext.Current.Authentication.LoadUser(Application_UserLoaded, null);

            RootVisual = new MainPage();
        }

        /// <summary>
        /// Invoked when the <see cref="LoadUserOperation"/> completes.
        /// Use this event handler to switch from the "loading UI" you created in <see cref="InitializeRootVisual"/> to the "application UI".
        /// </summary>
        private void Application_UserLoaded(LoadUserOperation operation)
        {
            if (operation.HasError)
            {
                ErrorWindow.Show(operation.Error);
                operation.MarkErrorAsHandled();
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                e.Handled = true;
                ErrorWindow.Show(e.ExceptionObject);
            }
        }
    }
}