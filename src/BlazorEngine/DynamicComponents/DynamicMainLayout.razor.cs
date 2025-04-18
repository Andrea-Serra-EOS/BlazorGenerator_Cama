﻿using BlazorEngine.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BlazorEngine.DynamicComponents
{
  public partial class DynamicMainLayout
  {
    FluentDesignTheme? Theme { get; set; }

    [Inject]
    public IHelpService? HelpService { get; set; }

    private void SwitchDarkLightTheme()
    {
      Theme!.Mode = Theme.Mode == DesignThemeModes.Light ? DesignThemeModes.Dark : DesignThemeModes.Light;
    }

  }
}
