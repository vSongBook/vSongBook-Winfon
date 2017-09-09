﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace vSongBook
{
    public partial class FfSettings : PhoneApplicationPage
    {
        private AppSettings settings = new AppSettings();
        long usedt;
        int secst;

        public FfSettings()
        {
            InitializeComponent();
            FfSettings_Loaded();
        }

        private void FfSettings_Loaded()
        {
            string current_fonts = "font size: " + settings.FontSizeSetting.ToString();
            
            string[] sett_titles = new string[] 
            { 
                "Display Settings", 
                "Application Mode", 
                "Reset App Settings",
                "Call: Jackson Siro",
                "Call: Jackson Siro",
                "Email Us Now",
                "Visit Us Online",
                "Application Version"
            };
            string[] sett_content = new string[] 
            { 
                "Font Size: " + current_fonts,
                installed_on(),
                "Reset App Settings",
                "+254 711 474 787 [safaricom]",
                "+254 731 973 180 [airtel]",
                "smataweb@gmail.com",
                "http://jacksiro.wordpress.com",
                "1.2.0.245"
            };
            for (int i = 0; i < sett_titles.Length; i++)
            {
                Current_SettView sett_obj = new Current_SettView();
                sett_obj.Sett_Title = sett_titles[i];
                sett_obj.Sett_Content = sett_content[i];
                SettListBox.Items.Add(sett_obj);
            }
        }

        private void SettListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SettListBox.SelectedIndex == -1)
                return;

            if (SettListBox.SelectedIndex == 0)
                NavigationService.Navigate(new Uri("/FfSettings1.xaml", UriKind.Relative));

            else if (SettListBox.SelectedIndex == 1)
                NavigationService.Navigate(new Uri("/FfSettings2.xaml", UriKind.Relative));

            else if (SettListBox.SelectedIndex == 2) ResetAppSettings();

            else if (SettListBox.SelectedIndex == 3)
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.PhoneNumber = "+254711474787";
                phoneCallTask.DisplayName = "Jack Siro";
                phoneCallTask.Show();
            }

            else if (SettListBox.SelectedIndex == 4)
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.PhoneNumber = "+254731973180";
                phoneCallTask.DisplayName = "Jack Siro";
                phoneCallTask.Show();
            }

            else if (SettListBox.SelectedIndex == 5)
            {
                EmailComposeTask emailComposeTask = new EmailComposeTask();
                emailComposeTask.Subject = "vSongBook Inquiry";
                emailComposeTask.Body = "God bless you Jack Siro. Am contacting you concerning vSongBook";
                emailComposeTask.To = "smataweb@gmail.com";
                emailComposeTask.Show();
            }

            else if (SettListBox.SelectedIndex == 6)
            {
                WebBrowserTask webBrowserTask = new WebBrowserTask();
                webBrowserTask.Uri = new Uri("http://jacksiro.wordpress.com", UriKind.Absolute);
                webBrowserTask.Show();
            }
            SettListBox.SelectedIndex = -1;
        }

        public void ResetAppSettings()
        {
            MessageBoxResult AreYouSure = MessageBox.Show("God bless you! Are you sure you want to reset all your settings?",
                    "Just a Minute...", MessageBoxButton.OKCancel);
            if (AreYouSure == MessageBoxResult.OK)
            {
                settings.CurrSongSetting = 1;
                settings.CurrFavourSetting = 1;
                settings.CurrMysongSetting = 1;
                settings.FontSizeSetting = 30;
                settings.FontTypeSetting = "";
                settings.FontSizeSetting = 40;
                settings.FontTypeSetting = "";
                settings.Hint1Setting = false;
                settings.Hint2Setting = false;
                settings.Hint3Setting = false;
                settings.Hint4Setting = false;
                settings.Hint5Setting = false;
                settings.Hint6Setting = false;
                settings.Hint7Setting = false;
                NavigationService.GoBack();

            }   
        }

        public class Current_SettView
        {
            public string Sett_Title { get; set; }
            public string Sett_Content { get; set; }
        }

        public string installed_on()
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            DateTime localDate = DateTime.Now;
            long nowMilSecond = (localDate.Ticks - unixEpoch.Ticks) / 10000;

            usedt = nowMilSecond - settings.InstalledonSetting;
            secst = (int)(usedt / 1000);

            if (secst < 60) return "Installed " + secst + " seconds ago";
            else if (secst < 3600) return "Installed " + secst / 60 + " minutes ago";
            else if (secst < 86400) return "Installed " + secst / 3600 + " hours ago";
            else if (secst < 604800) return "Installed " + secst / 86400 + " days ago";
            else if (secst < 2419200) return "Installed " + secst / 604800 + " weeks ago";
            else if (secst < 29030400) return "Installed " + secst / 2419200 + " months ago";
            else if (secst > 29030400) return "Installed " + secst / 29030400 + " years ago";
            return installed_on();
        }

    }
}