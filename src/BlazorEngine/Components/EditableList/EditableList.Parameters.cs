using System.Collections.ObjectModel;
using System.Reflection;
using BlazorEngine.Attributes;
using BlazorEngine.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorEngine.Components.EditableList
{
  public partial class EditableList<T>
  {
    [Parameter] public required object Context { get; set; }
    [Parameter] public required List<VisibleField<T>> VisibleFields { get; set; }
    [Parameter]
    public IQueryable<T>? Data
    {
      get => _data; set
      {
        _data = value;
        Selected.Clear();
      }
    }
    [Parameter] public ObservableCollection<T> Selected { get; set; } = [];
    [Parameter] public EventCallback<ObservableCollection<T>> SelectedChanged { get; set; }
    [Parameter] public Action<T>? OnSave { get; set; }
    [Parameter] public Action<T>? OnDiscard { get; set; }
    private IQueryable<T>? _data;
  }
}
