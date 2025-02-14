using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace MauiCollectionViewGroupChanging.ViewModels;

[DebuggerDisplay("{Status} - {Description}")]

public class WorkItemViewModel : ObservableObject
{
    private string? _description;
    public string? Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private string? _status;
    public string? Status
    {
        get => _status;
        set
        {
            SetProperty(ref _status, value);
            IsChangeStatusVisible = (Status != MainViewModel.StatusDONE);
        }
    }


    private bool _isChangeStatusVisible;
    public bool IsChangeStatusVisible
    {
        get => _isChangeStatusVisible;
        set => SetProperty(ref _isChangeStatusVisible, value);
    }


    RelayCommand<string>? _changeStatusCommand;
    public RelayCommand<string> ChangeStatusCommand => 
        _changeStatusCommand ??= new(ChangeStatusHandler);

    private void ChangeStatusHandler(string? newStatus)
    {
        if (string.IsNullOrEmpty(newStatus))
            return;

        Status = newStatus;
    }
}
