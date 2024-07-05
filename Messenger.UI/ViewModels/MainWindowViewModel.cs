﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Messenger.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = false;

    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if(value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if(instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), "HomeRegular"),
        new ListItemTemplate(typeof(ButtonPageViewModel), "CursorHoverRegular")
    };

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");
        
        Application.Current!.TryFindResource(iconKey, out var res);
        ListItemIcon = (StreamGeometry)res!;
    }
    public string Label { get; }
    public Type ModelType { get; }
    public StreamGeometry ListItemIcon { get; }
}