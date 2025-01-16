﻿using System.Data;
using BlazorEngine.Models;
using ClosedXML.Excel;

namespace BlazorEngine.Utils
{
  internal static class ExcelUtilities
  {
    internal static Stream ExportToExcel<T>(IList<T> data, List<VisibleField<T>> visibleFields) where T : class
    {
      XLWorkbook wb = new();
      wb.Worksheets.Add(ToDataTable(data, visibleFields), "Export");
      Stream stream = new MemoryStream();
      wb.SaveAs(stream);
      stream.Position = 0;
      return stream;
    }

    static DataTable ToDataTable<T>(IList<T> data, List<VisibleField<T>> visibleFields)
    {
      DataTable table = new();
      foreach (var field in visibleFields)
        table.Columns.Add(field.Caption, field.FieldType);

      foreach (T item in data)
      {
        DataRow row = table.NewRow();
        foreach (var field in visibleFields)
          row[visibleFields.IndexOf(field)] = field.InternalGet(item) ?? DBNull.Value;
        table.Rows.Add(row);
      }
      return table;
    }
  }
}
