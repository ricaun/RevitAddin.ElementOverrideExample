using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.ElementOverrideExample.Services;

namespace RevitAddin.ElementOverrideExample.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class ColorElementCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            OverrideGraphicController overrideGraphicController = new OverrideGraphicController(uiapp);

            overrideGraphicController.ColorSelectElement();

            return Result.Succeeded;
        }
    }
}
