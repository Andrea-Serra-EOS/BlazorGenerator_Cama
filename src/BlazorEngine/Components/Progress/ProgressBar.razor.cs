﻿using BlazorEngine.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorEngine.Components.Progress
{
  public partial class ProgressBar
  {
    [Inject]
    ProgressService? ProgressService { get; set; }

    bool _showProgress = false;

    protected override void OnInitialized()
    {
      ProgressService!.OnChange += UpdateProgress;
    }

    private void UpdateProgress(bool show)
    {
      _showProgress = show;
      StateHasChanged();
    }

    public void Dispose()
    {
      ProgressService!.OnChange -= UpdateProgress;
    }
  }
}
