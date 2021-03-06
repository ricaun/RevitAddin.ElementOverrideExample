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

            SetFillPatterColor(overrideGraphicSettings, fillPatternId, color);

            ElementId linePatternId = LinePatternElement.GetSolidPatternId();

            SetLinePatterColor(overrideGraphicSettings, linePatternId, ColorUtil.Color_108);

            view.SetElementOverrides(element.Id, overrideGraphicSettings);
        }

        private void SetFillPatterColor(OverrideGraphicSettings overrideGraphicSettings, ElementId fillPatternId, Color color)
        {
#if Revit2018 || Revit2017
            overrideGraphicSettings.SetProjectionFillColor(color);
            overrideGraphicSettings.SetProjectionFillPatternId(fillPatternId);
#else
            overrideGraphicSettings.SetSurfaceBackgroundPatternColor(color);
            overrideGraphicSettings.SetSurfaceBackgroundPatternId(fillPatternId);

            overrideGraphicSettings.SetSurfaceForegroundPatternColor(color);
            overrideGraphicSettings.SetSurfaceForegroundPatternId(fillPatternId);
#endif
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

        private void SetLinePatterColor(OverrideGraphicSettings overrideGraphicSettings, ElementId linePatternId, Color color)
        {
            overrideGraphicSettings.SetProjectionLineColor(color);
            overrideGraphicSettings.SetProjectionLinePatternId(linePatternId);
        }
    }
}