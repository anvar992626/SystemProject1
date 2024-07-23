using AffärsLager;
using Entiteterna;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class KundViewModel : ObservableObject
    {
        private readonly ControllerMottagare controller;
        private BokningViewModel bokningViewModel;

        private ObservableCollection<Kund> tillgängligaKunder = null!;
        public ObservableCollection<Kund> TillgängligaKunder { get => tillgängligaKunder; set { tillgängligaKunder = value; OnPropertyChanged(); } }

        public ObservableCollection<Kund> valdaKunder = null!;
        public ObservableCollection<Kund> ValdaKunder { get => valdaKunder; set { valdaKunder = value; OnPropertyChanged(); } }

        private Kund kunderSelectedItem = null!;
        public Kund KunderSelectedItem
        {
            get { return kunderSelectedItem; }
            set { kunderSelectedItem = value; OnPropertyChanged(); }
        }
        private Kund tillgängligaKunderSelectedItem = null!;
        public Kund TillgängligaKunderSelectedItem
        {
            get { return tillgängligaKunderSelectedItem; }
            set
            {
                tillgängligaKunderSelectedItem = value;
                OnPropertyChanged();
            }
        }


        private int tillgängligaKunderSelectedIndex;
        public int TillgängligaKunderSelectedIndex { get { return tillgängligaKunderSelectedIndex; } set { tillgängligaKunderSelectedIndex = value; OnPropertyChanged(); } }


        private int kunderSelectedIndex;
        public int KunderSelectedIndex
        {
            get { return kunderSelectedIndex; }
            set { kunderSelectedIndex = value; OnPropertyChanged(); }
        }



        private bool isNotModified = true;
        public bool IsNotModified
        {
            get { return isNotModified; }
            set { isNotModified = value; OnPropertyChanged(); }
        }
        public KundViewModel()
        {
            controller = new ControllerMottagare();
            TillgängligaKunder = new ObservableCollection<Kund>();
            ValdaKunder = new ObservableCollection<Kund>();
            bokningViewModel = new BokningViewModel();
            
            ReadCommandKund.Execute(null!);

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

        private RelayCommand skapaKundCommand = null!;
        public RelayCommand SkapaKundCommand => skapaKundCommand ??= new RelayCommand(async () =>
        {

            foreach (Kund kund in valdaKunder)
            {
                if (!valdaKunder.Contains(kund))
                {
                    valdaKunder.Add(kund);
                }
            }

            Kund b = await controller.SparaKundAsync(namn, kreditGräns, rabatt);


            //MessageBox.Show("Registrering är bekräftad:\n\nKundnamn: " + b.namn + "\nStarttid: " + startTid + "\nAntal dagar: " + antalDagar + "\nSluttid: " + slutTid, "\nUtrustning: " + valdaUtrustningar);

            TillgängligaKunder = new ObservableCollection<Kund>(controller.HämtaKunder());
        });


        private ICommand removeCommandKund = null!;
        public ICommand RemoveCommandKund => removeCommandKund ??= new RelayCommand(() =>
        {
            if (KunderSelectedItem != null && ValdaKunder.Contains(KunderSelectedItem))
            {
                TillgängligaKunder.Add(KunderSelectedItem);
                ValdaKunder.Remove(KunderSelectedItem);

                IsNotModified = false;
            }
        }, () => KunderSelectedItem != null && ValdaKunder.Contains(KunderSelectedItem));
        
      
        private ICommand addCommandKund = null!;

        public ICommand AddCommandKund => addCommandKund ??= new RelayCommand(() =>
        {
            if (KunderSelectedItem != null && TillgängligaKunder.Contains(KunderSelectedItem))
            {
                ValdaKunder.Add(KunderSelectedItem);
                
                TillgängligaKunder.Remove(KunderSelectedItem);
               
                IsNotModified = false;
            }
        }, () => KunderSelectedItem != null && TillgängligaKunder.Contains(KunderSelectedItem));
        private ICommand readCommandKund = null!;
        public ICommand ReadCommandKund => readCommandKund ??= readCommandKund = new RelayCommand(() =>
        {
            TillgängligaKunder = new ObservableCollection<Kund>(controller.HämtaKunder());
            
        });
        private ICommand addCommandKundTillBokning = null!;

        //public ICommand AddCommandKundTillBookning => addCommandKundTillBokning ??= new RelayCommand(() =>
        //{
        //    if (KunderSelectedItem != null && TillgängligaKunder.Contains(KunderSelectedItem))
        //    {
        //        ValdaKunder.Add(KunderSelectedItem);

        //        TillgängligaKunder.Remove(KunderSelectedItem);

        //        IsNotModified = false;
        //    }
        //});
       

















    }
}
