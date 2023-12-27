﻿using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using Todo.Views;

namespace Todo;

public partial class App : Application
{
    public App()
    {
        CustomHandler();

        InitializeComponent();

        MainPage = new NavigationPage(new HomeTaskPage());
    }

    private void CustomHandler()
    {
        EntryNoBoder();
        DatePikerNoBoder();
    }

    private static void EntryNoBoder()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
#if ANDROID
            //ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

#elif IOS || MACCATALYST
            //IOS || MACCATALIST
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

#elif WINDOWS
            //WINDOWS - não funciona 100%
            handler.PlatformView.BorderThickness = new Thickness(0).ToPlatform();
#endif
        });
    }

    private static void DatePikerNoBoder()
    {
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
#if ANDROID
            //ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

#elif IOS || MACCATALYST
            //IOS || MACCATALIST
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

#elif WINDOWS
            //WINDOWS - não funciona 100%
            handler.PlatformView.BorderThickness = new Thickness(0).ToPlatform();
#endif
        });
    }
}
