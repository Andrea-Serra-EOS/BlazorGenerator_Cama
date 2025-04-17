using BlazorEngine.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorEngine.Components.EditableList;

public partial class EditableList<T>
{
  [Inject] public UIServices? UIServices { get; set; }

  [Inject] private IJSRuntime? JSRuntime { get; set; }
}
