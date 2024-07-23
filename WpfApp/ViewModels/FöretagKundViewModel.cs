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
    public class FöretagKundViewModel: ObservableObject
    {
         
        private readonly ControllerKonferensBokning controllerKonferens;
        private KonferensLokalViewModel konferenslokalViewModel;

        private ObservableCollection<FöretagKund> tillgängligaFöretagKunder = null!;
        public ObservableCollection<FöretagKund> TillgängligaFöretagKunder { get => tillgängligaFöretagKunder; set { tillgängligaFöretagKunder = value; OnPropertyChanged(); } }

        public ObservableCollection<FöretagKund> valdaFöretagKunder = null!;
        public ObservableCollection<FöretagKund> ValdaFöretagKunder { get => valdaFöretagKunder; set { valdaFöretagKunder = value; OnPropertyChanged(); } }






        private FöretagKund företagkunderSelectedItem = null!;
        public FöretagKund FöretagkunderSelectedItem
        {
            get { return företagkunderSelectedItem; }
            set { företagkunderSelectedItem = value; OnPropertyChanged(); }
        }
        private FöretagKund tillgängligaFöretagKunderSelectedItem = null!;
        public FöretagKund TillgängligaFöretagKunderSelectedItem
        {
            get { return tillgängligaFöretagKunderSelectedItem; }
            set
            {
                tillgängligaFöretagKunderSelectedItem = value;
                OnPropertyChanged();
            }
        }



        private int tillgängligaFöretagKunderSelectedIndex;
        public int TillgängligaFöretagKunderSelectedIndex { get { return tillgängligaFöretagKunderSelectedIndex; } set { tillgängligaFöretagKunderSelectedIndex = value; OnPropertyChanged(); } }


        private int företagkunderSelectedIndex;
        public int FöretagKunderSelectedIndex
        {
            get { return företagkunderSelectedIndex; }
            set { företagkunderSelectedIndex = value; OnPropertyChanged(); }
        }



        private bool isNotModified = true;
        public bool IsNotModified
        {
            get { return isNotModified; }
            set { isNotModified = value; OnPropertyChanged(); }
        }



        public FöretagKundViewModel()
        {
            controllerKonferens = new ControllerKonferensBokning();
            TillgängligaFöretagKunder = new ObservableCollection<FöretagKund>();
            ValdaFöretagKunder = new ObservableCollection<FöretagKund>();
            konferenslokalViewModel = new KonferensLokalViewModel();

            ReadCommandFöretagKund.Execute(null!);

        }

        private string namn;
        public string Namn
        {
            get { return namn; }
            set
            {
                namn = value;
                OnPropertyChanged();
            }
        }

        private float kreditGräns;
        public float KreditGr
        {
            get { return kreditGräns; }
            set
            {
                kreditGräns = value;
                OnPropertyChanged();
            }
        }

        private float rabatt;
        public float Rabatt
        {
            get { return rabatt; }
            set
            {
                rabatt = value;
                OnPropertyChanged();
            }
        }



        ////COMMANDS

        private RelayCommand skapaFöretagKundCommand = null!;
        public RelayCommand SkapaFöretagKundCommand => skapaFöretagKundCommand ??= new RelayCommand(async () =>
        {
            foreach (FöretagKund företagkund in valdaFöretagKunder)
            {
                if (!valdaFöretagKunder.Contains(företagkund))
                {
                    valdaFöretagKunder.Add(företagkund);
                }
            }

            FöretagKund fk = await controllerKonferens.SparaFöretagKund(namn, kreditGräns, rabatt);

            //MessageBox.Show("Registrering är bekräftad:\n\nKundnamn: " + fk.namn + "\nStarttid: " + startTid + "\nAntal dagar: " + antalDagar + "\nSluttid: " + slutTid, "\nUtrustning: " + valdaUtrustningar);

            IList<FöretagKund> tillgängligaFöretagKunder = await controllerKonferens.HämtaFöretagKunderAsync();
            TillgängligaFöretagKunder = new ObservableCollection<FöretagKund>(tillgängligaFöretagKunder);
        });




        private ICommand removeCommandFöretagKund = null!;
        public ICommand RemoveCommandFöretagKund => removeCommandFöretagKund ??= new RelayCommand(() =>
        {
            if (FöretagkunderSelectedItem != null && ValdaFöretagKunder.Contains(FöretagkunderSelectedItem))
            {
                TillgängligaFöretagKunder.Add(FöretagkunderSelectedItem);
                ValdaFöretagKunder.Remove(FöretagkunderSelectedItem);

                IsNotModified = false;
            }
        }, () => FöretagkunderSelectedItem != null && ValdaFöretagKunder.Contains(FöretagkunderSelectedItem));


        private ICommand addCommandFöretagKund = null!;

        public ICommand AddCommandFöretagKund => addCommandFöretagKund ??= new RelayCommand(async () =>
        {
            if (FöretagkunderSelectedItem != null && TillgängligaFöretagKunder.Contains(FöretagkunderSelectedItem))
            {
                ValdaFöretagKunder.Add(FöretagkunderSelectedItem);
                konferenslokalViewModel.ValdaFöretagKunder.Add(FöretagkunderSelectedItem);
                TillgängligaFöretagKunder.Remove(FöretagkunderSelectedItem);

                try
                {
                    var företagKunder = await controllerKonferens.HämtaFöretagKunderAsync();
                    konferenslokalViewModel.ValdaFöretagKunder = new ObservableCollection<FöretagKund>(företagKunder);
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., display error message)
                    Console.WriteLine(ex.Message);
                }

                IsNotModified = false;
            }
        }, () => FöretagkunderSelectedItem != null && TillgängligaFöretagKunder.Contains(FöretagkunderSelectedItem));


        private ICommand readCommandFöretagKund = null!;
        public ICommand ReadCommandFöretagKund => readCommandFöretagKund ??= new RelayCommand(async () =>
        {
            try
            {
                var företagKunder = await controllerKonferens.HämtaFöretagKunderAsync();
                TillgängligaFöretagKunder = new ObservableCollection<FöretagKund>(företagKunder);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., display error message)
                Console.WriteLine(ex.Message);
            }
        });

        private ICommand addCommandFöretagKundTillKonferensBokning = null!;




    }
}

 