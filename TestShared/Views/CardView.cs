﻿using BlazorGenerator.Attributes;
using BlazorGenerator.Layouts;
using BlazorGenerator.Models;
using BlazorGenerator.Utils;
using Microsoft.AspNetCore.Components;
using TestShared.Data;

namespace TestShared.Views
{
  [Route("card")]
  [AddToMenu(Title = "Card Page", Route = "card")]
  public class CardView : CardPage<Mock>
  {


    protected override void OnParametersSet()
    {
      VisibleFields = new List<VisibleField<Mock>>();
      VisibleFields.AddField(nameof(Mock.Id));
      VisibleFields.AddField(nameof(Mock.Name));
      VisibleFields.AddField(nameof(Mock.Price));
      VisibleFields.AddField(nameof(Mock.Description), (ref VisibleField<Mock> o)=> o.TextFieldType = Microsoft.FluentUI.AspNetCore.Components.TextFieldType.Password );
      VisibleFields.AddField(nameof(Mock.OrderDate));
      VisibleFields.AddField(nameof(Mock.type));
      VisibleFields.AddField(nameof(Mock.NullTest));

      Content = Mock.getSingleMock();
    }


    [PageAction(Caption = "ShowProgress")]
    public async void ShowProgress()
    {
      UIServices.ProgressService.StartProgress();
      await Task.Delay(10000);
      UIServices.ProgressService.StopProgress();
    }

    [PageAction(Caption = "Open Modal")]
    public async void OpenModal()
    {
      var mock = Mock.getSingleMock();
      var res = await UIServices.OpenModal(typeof(ModalView), mock);

    }

    [PageAction(Caption = "Test1", Group ="grouped")]
    public async void Test1()
    {
    }
    [PageAction(Caption = "Test2", Group = "grouped")]
    public async void Test2()
    {
    }


  }
}
