using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitAddin.ElementOverrideExample.Services
{
    public class OverrideGraphicService
    {
        public void Clear(View view, Element element)
        {
            OverrideGraphicSettings overrideGraphicSettings = new OverrideGraphicSettings();
            view.SetElementOverrides(element.Id, overrideGraphicSettings);
        }

        public void FillColor(View view, Element element, Color color = null)
        {
            if (color == null) color = new Color(0, 255, 0);

            Document document = view.Document;

            OverrideGraphicSettings overrideGraphicSettings = new OverrideGraphicSettings();

            ElementId fillPatternId = GetSolidFillPatternElement(document).Id;

            overrideGraphicSettings.SetSurfaceBackgroundPatternColor(color);
            overrideGraphicSettings.SetSurfaceBackgroundPatternId(fillPatternId);

            overrideGraphicSettings.SetSurfaceForegroundPatternColor(color);
            overrideGraphicSettings.SetSurfaceForegroundPatternId(fillPatternId);

            view.SetElementOverrides(element.Id, overrideGraphicSettings);
        }

        private FillPatternElement GetSolidFillPatternElement(Document document)
        {
            var element = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfClass(typeof(FillPatternElement))
                .Cast<FillPatternElement>()
                .FirstOrDefault(e => e.GetFillPattern().IsSolidFill);

            return element;
        }

    }
}