﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace TerminalClient
{
    [Activity(Theme = "@style/Maui.SplashTheme", LaunchMode =LaunchMode.SingleTask, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
