using AffärsLager;
using Entiteterna;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Views;

namespace WpfApp.ViewModels
{
    public class LoggInViewModel : ObservableObject
    {
        private int? anstäldNr;
        
        private string lösenord;
        private string namn;


        private Anställd AnstäldLoggedIn;

       

        public int? AnstäldNr

        {
            get { return anstäldNr; }
            set { anstäldNr = value; OnPropertyChanged(); }
        }
       
        public string Lösenord
        {
            get { return lösenord; }
            set { lösenord = value; OnPropertyChanged(); }
        }
        public string Namn
        {
            get { return namn; }
            set { namn = value; OnPropertyChanged(); }
        }

        public Anställd LoggedIn
        {
            get { return AnstäldLoggedIn; }
            set { AnstäldLoggedIn = value; OnPropertyChanged(); }
        }



        public LoggInViewModel()
        {
            ExitApplicationCommand = new RelayCommand(ExitApplication);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        ////COMMANDS

        private ICommand loggaInCommand;
        public ICommand LoggaInCommand
        {
            get
            {
                if (loggaInCommand == null)
                {
                    loggaInCommand = new RelayCommand(() =>
                    {
                        var controller = new ControllerMottagare();
                        bool success = controller.LoggaIn(AnstäldNr ?? 0, Lösenord );



                        if (success)
                        {

                           

                            if(anstäldNr== 2)
                            {
                                LoggedIn = controller.AnställdLoggedIn;
                                // Navigera till nästa vy här
                                MainWindow MW = new MainWindow();
                                MW.Show();
                                App.Current.MainWindow.Close();
                                App.Current.MainWindow = MW;
                            }

                            if (anstäldNr == 1)
                            {
                                LoggedIn = controller.AnställdLoggedIn;
                                // Navigera till nästa vy här
                                Views.SkidshopBokning Sb = new Views.SkidshopBokning();
                                Sb.Show();
                                App.Current.MainWindow.Close();
                                App.Current.MainWindow = Sb;
                            }

                            



                            if (anstäldNr == 4)
                            {
                                LoggedIn = controller.AnställdLoggedIn;
                                // Navigera till nästa vy här 
                                //mwm motsvarar mainwindowMarknads vyn
                                Views.MainWindowMarknad  Mwm= new Views.MainWindowMarknad();
                                Mwm.Show();
                                App.Current.MainWindow.Close();
                                App.Current.MainWindow = Mwm;
                            }


                        }
                        else
                        {
                            MessageBox.Show("Fel användarnamn eller lösenord, försök igen.");
                        }
                    });
                }

                return loggaInCommand;
            }
        }







        private ICommand exitCommand = new RelayCommand(() => App.Current.Shutdown());
        public ICommand ExitCommand => exitCommand;



        public ICommand ExitApplicationCommand { get; set; }
        private void ExitApplication()
        {
            App.Current.Shutdown();
        }


    }
}
