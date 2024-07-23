using AffärsLager;
using Entiteterna;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class Mainwindow : ObservableObject
    {
        public ControllerMottagare controller = new ControllerMottagare();



        private ICommand exitCommand = new RelayCommand(() => App.Current.Shutdown());
        public ICommand ExitCommand => exitCommand;



        public BokningViewModel Bokning { get; set; } = new BokningViewModel();
        
        public KundViewModel Kund { get; set; } = new KundViewModel();
        public SkidshopViewModel Skidshop { get; set; } = new SkidshopViewModel();
       
     
        public KonferensLokalViewModel KonferensLokal { get; set; } = new KonferensLokalViewModel();
        public FöretagKundViewModel FöretagKund { get; set; } = new FöretagKundViewModel();
        




    }
}

