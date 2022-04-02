using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Inhaltsdialogfeld" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace SeedCollection
{
    public sealed partial class NewSeed : Page
    {
        public NewSeed()
        {
            this.InitializeComponent();
        }

        public Seed getSeed()
        {
            return new Seed(seedBox.Text,biomesBox.Text,descriptionBox.Text);
        }

        public void setSeed(Seed seed)
        {
            seedBox.Text = seed.seed;
            descriptionBox.Text = seed.description;
            biomesBox.Text = seed.biomes;

        }

    }
}
