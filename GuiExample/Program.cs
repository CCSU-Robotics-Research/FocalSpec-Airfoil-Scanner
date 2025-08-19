// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="FocalSpec Ltd">
// FocalSpec Ltd 2016
// </copyright>
// <summary>
// Starting point for the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Adapters;
using FocalSpec.GuiExample.Presenter;
using FocalSpec.GuiExample.View;

namespace FocalSpec.GuiExample
{
    static class Program
    {
        /// <summary>
        /// Main view of the application.
        /// </summary>
        private static MainView
            _mainView;

        //private static MainPresenter _mainPresenter;
        //public static MainPresenter MainPresenter => _mainPresenter;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Cursor.Current = Cursors.WaitCursor;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += ApplicationOnThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            _mainView = new MainView();
            // ReSharper disable once ObjectCreationAsStatement 

            new MainPresenter(_mainView, ShowMainView);     // Opens the application

        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs threadExceptionEventArgs)
        {
            MessageBox.Show(string.Format("{0}\n\n{1}", threadExceptionEventArgs.Exception.Message, threadExceptionEventArgs.Exception.StackTrace), "Unhandled Exception");
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Exception e = unhandledExceptionEventArgs.ExceptionObject as Exception;
            if (e != null)
                MessageBox.Show(string.Format("{0}\n\n{1}", e.Message, e.StackTrace), "Unhandled Exception");
            else
                MessageBox.Show("Could not get Exception object for detailed report.", "Unhandled Exception");
        }

        /// <summary>
        /// Shows the main view.
        /// </summary>
        static void ShowMainView()
        {
            if (_mainView != null)
            {
                Application.Run(_mainView);
            }
        }

        public static MainView getMainView() { return _mainView; }
    }
}
