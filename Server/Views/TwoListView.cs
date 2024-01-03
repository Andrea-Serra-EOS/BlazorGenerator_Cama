﻿using BlazorGenerator.Attributes;
using BlazorGenerator.Layouts;
using BlazorGenerator.Models;
using BlazorGenerator.Utils;
using Microsoft.AspNetCore.Components;
using Server.Data;

namespace Server.Views
{
    [Route("/twolist")]
    [AddToMenu(Title = "TwoList Page", Route= "/twolist")]
    public class TwoListView : TwoList<Mock, Mock>
    {

        public override string Title => "List View";



        protected override void OnParametersSet()
        {
            FirstListVisibleFields = new List<VisibleField<Mock>>();
            FirstListVisibleFields.AddField(nameof(Mock.Id));
            FirstListVisibleFields.AddField(nameof(Mock.Name));
            FirstListVisibleFields.AddField(nameof(Mock.Price));
            FirstListVisibleFields.AddField(nameof(Mock.Description));
            FirstListVisibleFields.AddField(nameof(Mock.OrderDate));

            FirstListData = Mock.getMultipleMock().AsQueryable();

            SecondListVisibleFields = new List<VisibleField<Mock>>();
            SecondListVisibleFields.AddField(nameof(Mock.Id));
            SecondListVisibleFields.AddField(nameof(Mock.Name));


            SecondListData = Mock.getMultipleMock().AsQueryable();
        }

        public override void FirstListSave(Mock Rec, Mock xRec)
        {
            var tmp = FirstListData.ToList();
            tmp[FirstListData.ToList().FindIndex(o => o.Id == xRec.Id)] = Rec;
            FirstListData = tmp.AsQueryable();
        }
    }
}
