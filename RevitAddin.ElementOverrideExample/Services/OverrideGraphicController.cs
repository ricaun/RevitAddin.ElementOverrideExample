using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitAddin.ElementOverrideExample.Services
{
    public class OverrideGraphicController
    {
        #region Constructor
        private readonly UIApplication uiapp;
        private readonly OverrideGraphicService overrideGraphicService;

        public OverrideGraphicController(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.overrideGraphicService = new OverrideGraphicService();
        }
        #endregion

        public void ClearAll()
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;


            var elements = new FilteredElementCollector(document, view.Id)
                .WhereElementIsNotElementType()
                .ToElements();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Clear All");
                foreach (var element in elements)
                {
                    overrideGraphicService.Clear(view, element);
                }
                transaction.Commit();
            }

        }

        public void ColorAll()
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;

            var elements = new FilteredElementCollector(document, view.Id)
                .WhereElementIsNotElementType()
                .ToElements();

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("Color All");
                foreach (var element in elements)
                {
                    overrideGraphicService.FillColor(view, element);
                }
                transaction.Commit();
            }

        }

        public void ColorSelectElement()
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;

            var element = PickElement(uidoc);
            while (element != null)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Color Element");
                    overrideGraphicService.FillColor(view, element, new Color(255, 0, 0));
                    transaction.Commit();
                }
                element = PickElement(uidoc);
            }
        }

        private Element PickElement(UIDocument uidoc)
        {
            Document document = uidoc.Document;
            Selection selection = uidoc.Selection;
            Element element = null;

            try
            {
                var reference = selection.PickObject(ObjectType.Element);
                element = document.GetElement(reference);
            }
            catch { }

            return element;
        }
    }
}