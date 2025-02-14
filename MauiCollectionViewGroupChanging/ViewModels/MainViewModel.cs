using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MauiCollectionViewGroupChanging.ViewModels;

public class MainViewModel : ObservableObject
{
    private List<WorkItemViewModel> _workItems = new();

    private ObservableCollection<WorkItemGroupViewModel> _groupedWorkItems = new();
    public ObservableCollection<WorkItemGroupViewModel> GroupedWorkItems
    {
        get => _groupedWorkItems;
        set => SetProperty(ref _groupedWorkItems, value);
    }

    public static string StatusTODO = "TODO";
    public static string StatusACTIVE = "ACTIVE";
    public static string StatusDONE = "DONE";

    WorkItemGroupViewModel _todoGroup = new(StatusTODO);
    WorkItemGroupViewModel _activeGroup = new(StatusACTIVE);
    WorkItemGroupViewModel _doneGroup = new(StatusDONE);


    public MainViewModel()
    {
        CreateWorkItemData();

        UpdateGroupedWorkItems();
    }

    private void UpdateGroupedWorkItems()
    {
        foreach (var item in _workItems)
        {
            if (item.Status == StatusTODO && !_todoGroup.Contains(item))
            {
                _todoGroup.Add(item);
            }
            else if (item.Status == StatusACTIVE && !_activeGroup.Contains(item))
            {
                // move any existing active item back to TODO
                if (_activeGroup.Any())
                {
                    var currentActiveItem = _activeGroup[0];
                    _activeGroup.Remove(currentActiveItem);
                    currentActiveItem.Status = StatusTODO;
                    _todoGroup.Add(currentActiveItem);
                }
                _activeGroup.Add(item);
            }
            else if (item.Status == StatusDONE && !_doneGroup.Contains(item))
            {
                _doneGroup.Add(item);
            }
        }

        if (_todoGroup.Any())
        {
            if (!GroupedWorkItems.Contains(_todoGroup))
                GroupedWorkItems.Add(_todoGroup);
        }
        else if (GroupedWorkItems.Contains(_todoGroup))
        {
            GroupedWorkItems.Remove(_todoGroup);
        }

        if (_activeGroup.Any())
        {
            // ACTIVE always at top
            if (!GroupedWorkItems.Contains(_activeGroup))
                GroupedWorkItems.Insert(0, _activeGroup);
        }
        else if (GroupedWorkItems.Contains(_activeGroup))
        {
            GroupedWorkItems.Remove(_activeGroup);
        }

        if (_doneGroup.Any())
        {
            if (!GroupedWorkItems.Contains(_doneGroup))
                GroupedWorkItems.Add(_doneGroup);
        }
        else if (GroupedWorkItems.Contains(_doneGroup))
        {
            GroupedWorkItems.Remove(_doneGroup);
        }
    }


    private ObservableCollection<WorkItemGroupViewModel> GetNewGroupedWorkItems()
    {
        ObservableCollection<WorkItemGroupViewModel> newGroupedWorkItems = new();

        foreach (var workItem in _workItems)
        {
            var group = newGroupedWorkItems.FirstOrDefault(g => g.GroupDescription == workItem.Status);
            if (group is null)
            {
                group = new WorkItemGroupViewModel(workItem.Status);
                newGroupedWorkItems.Add(group);
            }
            group.Add(workItem);
        }

        return newGroupedWorkItems;
    }


    private bool _statusUpdatesInProgress = false;
    private void WorkItem_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (_statusUpdatesInProgress)
            return;

        if (sender is not WorkItemViewModel workItem)
            return;

        if (e.PropertyName == nameof(WorkItemViewModel.Status))
        {
            _statusUpdatesInProgress = true;
            RemoveItemFromOldGroup(workItem);

            UpdateGroupedWorkItems();
            _statusUpdatesInProgress = false;

            //GroupedWorkItems = GetNewGroupedWorkItems();

            //var currentGroup = GroupedWorkItems.FirstOrDefault(g => g.Contains(workItem));

            //if (currentGroup?.Count == 1)
            //{
            //    GroupedWorkItems.Remove(currentGroup);
            //}
            //else
            //{
            //    currentGroup?.Remove(workItem);
            //}

            //var newGroup = GroupedWorkItems.FirstOrDefault(g => g.GroupDescription == workItem.Status);
            //if (newGroup is null)
            //{
            //    newGroup = new WorkItemGroupViewModel(workItem.Status);
            //    GroupedWorkItems.Add(newGroup);
            //}
            //newGroup?.Add(workItem);
        }
    }

    private void RemoveItemFromOldGroup(WorkItemViewModel workItem)
    {
        if (_todoGroup.Contains(workItem))
            _todoGroup.Remove(workItem);

        if (_activeGroup.Contains(workItem))
            _activeGroup.Remove(workItem);

        if (_doneGroup.Contains(workItem))
            _doneGroup.Remove(workItem);
    }

    private void CreateWorkItemData()
    {
        var cleanHouse = new WorkItemViewModel
        {
            Description = "Clean House",
            Status = StatusDONE
        };
        cleanHouse.PropertyChanged += WorkItem_PropertyChanged;
        _workItems.Add(cleanHouse);
        
        var doLaundry = new WorkItemViewModel
        {
            Description = "Do Laundry",
            Status = StatusDONE
        };
        doLaundry.PropertyChanged += WorkItem_PropertyChanged;
        _workItems.Add(doLaundry);

        var mowLawn = new WorkItemViewModel
        {
            Description = "Mow Lawn",
            Status = StatusTODO
        };
        mowLawn.PropertyChanged += WorkItem_PropertyChanged;
        _workItems.Add(mowLawn);

    }
}
