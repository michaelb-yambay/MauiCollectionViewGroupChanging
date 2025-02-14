using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiCollectionViewGroupChanging.ViewModels;

[DebuggerDisplay("{GroupDescription}")]
public class WorkItemGroupViewModel : ObservableCollection<WorkItemViewModel>
{
    public WorkItemGroupViewModel(string groupDescription)
    {
        GroupDescription = groupDescription;
    }

    public string GroupDescription { get; }
}
