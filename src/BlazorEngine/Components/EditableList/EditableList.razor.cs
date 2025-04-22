using BlazorEngine.Components.Base;
using BlazorEngine.Models;
using BlazorEngine.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection;

namespace BlazorEngine.Components.EditableList
{
  public partial class EditableList<T> where T : class
  {
    internal T? CurrRec { get; set; }
    private List<T> ItemEdit { get; set; } = [];

    [Inject]
    private IKeyCodeService? KeyCodeService { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
    }

    private async Task EditAsync(T context)
    {
      T? res;
      //if (EditFormType != null)
      //{
      //  res = await UIServices!.OpenPanel(EditFormType, context);
      //}
      //else
      //{
      //  var typeDelegate = RoslynUtilities.CreateAndInstatiateClass(VisibleFields, "edit", ct: (Context as BlazorEngineComponentBase)!.ComponentDetached);
      //  var type = (Type)typeDelegate.Invoke(cancellationToken: (Context as BlazorEngineComponentBase)!.ComponentDetached).Result;
      //  res = await UIServices!.OpenPanel(type, context);
      //  GC.Collect();
      //}
      //if (res != null)
      //  HandleSave(res!);
    }

    protected void HandleSave(T data)
    {
      OnSave?.Invoke(data);
      //RefreshData?.Invoke();
      StateHasChanged();
    }

    protected void HandleDiscard(T data)
    {
      OnDiscard?.Invoke(data);
      //RefreshData?.Invoke();
      StateHasChanged();
    }

    string GetCssGridTemplate(int gridActions, PermissionSet? permissionSet)
    {
      const string select = "50px ";
      string actions = string.Empty;
      if (gridActions > 0)
        actions = "30px ";

      var spacing = 80 / VisibleFields.Count;
      string cols = string.Join(" ", Enumerable.Repeat($"{spacing}%", VisibleFields.Count));
      string rowActions = string.Empty;
      if ((permissionSet?.Modify ?? false) || (permissionSet?.Delete ?? false))
        rowActions = " 100px";

      return select + actions + cols + rowActions;
    }

    private void OnKeyDownAsync(FluentKeyCodeEventArgs args)
    {
      if (_multipleSelectEnabled | _shiftModifierEnabled) return;
      if (args.Key == KeyCode.Ctrl)
      {
        _multipleSelectEnabled = true;
      }
      else if (args.Key == KeyCode.Shift)
      {
        _shiftModifierEnabled = true;
      }

    }
    private void OnKeyUp(FluentKeyCodeEventArgs args)
    {
      if (args.Key == KeyCode.Ctrl)
      {
        _multipleSelectEnabled = false;
      }
      else if (args.Key == KeyCode.Shift)
      {
        _shiftModifierEnabled = false;
      }
    }

    private async Task HandleCellClick(FluentDataGridCell<T> cell)
    {
      if (cell.GridColumn > 1)
      {
        HandleSingleRecSelection(cell.Item);
        cell.ParentReference?.Current.FocusAsync();
        await Task.CompletedTask;
      }
    }

    internal void Refresh()
    {
      StateHasChanged();
    }

    private void SaveChanges(T item)
    {
      OnSave?.Invoke(item);
    }

    private void EditValue(ChangeEventArgs args, string fieldName, T item)
    {
      var itemToEdit = item;
      var index = 0;

      foreach (var itemInList in ItemEdit)
      {
        if (itemInList == item)
        {
          itemToEdit = itemInList;
          break;
        }
        index++;
      }

      if (itemToEdit == null) return;

      var pi = itemToEdit.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);
      
      if (pi?.PropertyType == typeof(string))
      {
        pi?.SetValue(itemToEdit, (string)args?.Value);
      }
      else if (pi?.PropertyType == typeof(decimal))
      {
        pi?.SetValue(itemToEdit, Decimal.Parse((string)args.Value));
      }
      else if (pi?.PropertyType == typeof(int))
      {
        pi?.SetValue(itemToEdit, int.Parse((string)args.Value));
      }

      ItemEdit[index] = itemToEdit;
    }
  }
}
