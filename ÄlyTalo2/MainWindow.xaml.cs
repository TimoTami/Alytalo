using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ÄlyTalo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Sauna TaaSauna = new Sauna();
        public Lights OlohuoneValot = new Lights();
        public Lights MakuuhuoneValot = new Lights();
        public Thermostat TaaTermostaatti = new Thermostat();
        public DispatcherTimer SaunaTimerUp = new DispatcherTimer();
        public DispatcherTimer SaunaTimerDown = new DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            SaunaTimerUp.Tick += SaunanLampo_Tick;
            SaunaTimerUp.Interval = new TimeSpan(0, 0, 1);

            SaunaTimerDown.Tick += SaunanLampo_Tick;
            SaunaTimerDown.Interval = new TimeSpan(0, 0, 1);

            TaaTermostaatti.Temperature = 22;

        }

        private void BtnSaunaOnOff_Click(object sender, RoutedEventArgs e)
        {
            if (TaaSauna.Switched == false)
            {
                LabelOnkoSaunaPäällä.Content = "Sauna on päällä";
                TaaSauna.SaunaOn();
                SaunaTimerUp.Start();
                SaunaTimerDown.Stop();
                BtnSaunaOnOff.Background = Brushes.OrangeRed;

            }
            else
            {
                LabelOnkoSaunaPäällä.Content = null;
                TaaSauna.SaunaOff();
                SaunaTimerUp.Stop();
                SaunaTimerDown.Start();
                BtnSaunaOnOff.ClearValue(Button.BackgroundProperty);

            }
        }
        private void SaunanLampo_Tick(object sender, EventArgs e)
        {
            if (TaaSauna.Switched == true)
            {
                TaaSauna.SaunaTemperature = TaaSauna.SaunaTemperature + 1;
                LabelSaunanLampotila.Content = "Saunan lämpötila: " + TaaSauna.SaunaTemperature + "°c";
            }

            if (TaaSauna.Switched == false)
            {
                TaaSauna.SaunaTemperature = TaaSauna.SaunaTemperature - 1;
                LabelSaunanLampotila.Content = "Saunan lämpötila: " + TaaSauna.SaunaTemperature + "°c";

                if (TaaSauna.SaunaTemperature <= 0)
                {
                    SaunaTimerDown.Stop();
                    LabelSaunanLampotila.Content = null;
                }
            }
            if (TaaSauna.SaunaTemperature >= 89)
            {

                SaunaTimerUp.Stop();
            }
        }

        private void BtnAsetaUusiLampotila_Click(object sender, RoutedEventArgs e)
        {
            TaaTermostaatti.SetTemperature(TextBoxUusiLampotila.Text);
            TextBlockLampotila.Text = "Talon lämpötila: " + TaaTermostaatti.Temperature.ToString() + "°c";
            TextBoxUusiLampotila.Text = "";
        }
        private void TextBlockLampotila_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlockLampotila.Text = "Talon lämpötila: " + TaaTermostaatti.Temperature.ToString() + "°c";
        }

        private void TextBoxUusiLampotila_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void SliderOlohuone_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SliderOlohuone.Value == 0)
            {
                OlohuoneValot.Switched = false;
                TextBlockOlohuoneValotArvo.Text = "Olohuoneen valot eivät ole päällä";
            }
            else
            {
                OlohuoneValot.Switched = true;
                OlohuoneValot.Dimmer = SliderOlohuone.Value.ToString("#");
                TextBlockOlohuoneValotArvo.Text = "Olohuoneen valot ovat " + OlohuoneValot.Dimmer + "% teholla";
            }
        }

        private void SliderMakuuhuone_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SliderMakuuhuone.Value == 0)
            {
                MakuuhuoneValot.Switched = false;
                TextBlockMakuuhuoneValotArvo.Text = "Makuuhuoneenvalot eivät ole päällä";
            }
            else
            {
                MakuuhuoneValot.Switched = true;
                MakuuhuoneValot.Dimmer = SliderMakuuhuone.Value.ToString("#");
                TextBlockMakuuhuoneValotArvo.Text = "Makuuhuoneenvalot ovat " + MakuuhuoneValot.Dimmer + "% teholla";
            }

        }

        private void TextBlockOlohuoneValotArvo_Loaded(object sender, RoutedEventArgs e)
        {
            if (OlohuoneValot.Switched== false)
            {
                TextBlockOlohuoneValotArvo.Text = "Olohuoneen valot eivät ole päällä";
            }
        }

        private void TextBlockMakuuhuoneValotArvo_Loaded(object sender, RoutedEventArgs e)
        {
            if (MakuuhuoneValot.Switched == false)
            {
                TextBlockMakuuhuoneValotArvo.Text = "Makuuhuoneen valot eivät ole päällä";
            }
        }
    }
}
