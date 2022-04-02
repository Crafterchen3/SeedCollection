using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace SeedCollection
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        ObservableCollection<Seed> seeds = new ObservableCollection<Seed>();

        public MainPage()
        {
            readSeeds();

            this.InitializeComponent();
        }

        private ApplicationDataCompositeValue createCompositeSeed(Seed seed)
        {
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
            composite["seed"] = seed.seed;
            composite["description"] = seed.description;
            composite["biomes"] = seed.biomes;
            return composite;

        }

        private Seed getSeed(ApplicationDataCompositeValue keys)
        {
            return new Seed((String)keys["seed"], (String)keys["description"], (String)keys["biomes"]);
        }

        private void pushSeeds()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // Create a setting in a container

            if (localSettings.Containers.ContainsKey("seeds"))
            {
                    localSettings.DeleteContainer("seeds");

                Windows.Storage.ApplicationDataContainer container =
                    localSettings.CreateContainer("seeds", Windows.Storage.ApplicationDataCreateDisposition.Always);


            }
            else
            {
                Windows.Storage.ApplicationDataContainer container =
                    localSettings.CreateContainer("seeds", Windows.Storage.ApplicationDataCreateDisposition.Always);

            }

            for (int i = 0; i < seeds.Count; i++)
            {
                localSettings.Containers["seeds"].Values[i.ToString()] = createCompositeSeed(seeds[i]);

            }

        }

        private void readSeeds()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Containers.ContainsKey("seeds"))
            {
                for (int i = 0; i < localSettings.Containers["seeds"].Values.Count; i++)
                {
                    seeds.Add( getSeed((ApplicationDataCompositeValue) localSettings.Containers["seeds"].Values[i.ToString()]));
                }
            }
            else
            {
                seeds.Add(new Seed("6546382", "Plains", "A cool seed"));
                seeds.Add(new Seed("9079234341", "Forest", "A cool seed"));
                seeds.Add(new Seed("324891124", "Taiga", "A cool seed"));
                pushSeeds();
            }

        }

        NewSeed newSeed;

        private async void AddSeed_Click(object sender, RoutedEventArgs e)
        {
            newSeed = new NewSeed();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Add Seed";
            dialog.PrimaryButtonText = "Add";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.HorizontalAlignment = HorizontalAlignment.Stretch;
            dialog.PrimaryButtonClick += AddSeedPrimary_Click;
            dialog.Content = newSeed;
            await dialog.ShowAsync();

        }

        private void AddSeedPrimary_Click(ContentDialog dialog,ContentDialogButtonClickEventArgs args)
        {
            seeds.Add(newSeed.getSeed());
            pushSeeds();
        }

        private void EditSeedPrimary_Click(ContentDialog dialog, ContentDialogButtonClickEventArgs args)
        {
            seeds[listView.SelectedIndex] = newSeed.getSeed();
            pushSeeds();
        }


        private void DeleteSeed_Click(object sender, RoutedEventArgs e)
        {
            seeds.Remove((Seed) listView.SelectedItem);
            pushSeeds();
        }

        private void CopySeed_Click(object sender, RoutedEventArgs e)
        {
            String seed = (String)((AppBarButton) sender).Tag;
            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(seed);
            Clipboard.SetContent(dataPackage);
        }

        private async void EditSeed_Click(object sender, RoutedEventArgs e)
        {
            newSeed = new NewSeed();
            Seed nSeed = (Seed)listView.SelectedItem;
            newSeed.setSeed(new Seed(nSeed.seed,nSeed. biomes,nSeed.description));
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Add Seed";
            dialog.PrimaryButtonText = "Add";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.HorizontalAlignment = HorizontalAlignment.Stretch;
            dialog.PrimaryButtonClick += EditSeedPrimary_Click;
            dialog.Content = newSeed;
            await dialog.ShowAsync();
        }
    }
}
