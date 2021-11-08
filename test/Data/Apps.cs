﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Eos.Blazor.Generator;
using Microsoft.AspNetCore.Components;
using Eos.Blazor.Generator.Attributes;
using Eos.Blazor.Generator.Components;
using System.Threading.Tasks;
using RestSharp;
using System.Runtime.Serialization;
using Eos.Nav.Common.Apps;
using Newtonsoft.Json.Converters;
using System.Linq;
using Eos.Blazor.Generator.Models;
using Eos.Blazor.Generator.Enum;
using Eos.Bare.Client.Model;

namespace test.Data
{
  [AddToMenu("Apps", "/apps")]
  [Route("/apps")]
  public class ServicesApps : ListPage<ServiceAppPackage>
  {

    protected override Task OnInitializedAsync()
    {
      var client = new RestClient("http://br-labsdev2:9462/api/v2/services/Integration/apps/all");
      var res = client.Execute(new RestRequest());
      var test = new VisibleField<ServiceAppPackage>(nameof(ServiceAppPackage.Name)) { 
        Getter = f => f.Name,
        Setter = (f, v) =>
        {
          f.Name = v as string;
        }
      };
      VisibleFields.Add(test);

      VisibleFields = new List<VisibleField<ServiceAppPackage>>()        {
        new VisibleField<ServiceAppPackage>(nameof(ServiceAppPackage.IsInstalled), FieldType.Boolean){Getter = f => f.IsInstalled},
        new VisibleField<ServiceAppPackage>(nameof(ServiceAppPackage.Name)){Getter = f => f.Name},
        new VisibleField<ServiceAppPackage>(nameof(ServiceAppPackage.Publisher)){ Getter = f => f.Publisher},
        new VisibleField<ServiceAppPackage>(nameof(ServiceAppPackage.Version)){Getter = f => f.Version.ToString()},
        new VisibleField<ServiceAppPackage>(nameof(ServiceAppPackage.DataVersion)){ Getter = f => f.DataVersion.ToString()}
      };
      Data = JsonConvert.DeserializeObject<List<ServiceAppPackage>>(res.Content);
      return base.OnInitializedAsync();
    }
  }


}
