using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Linq;

namespace AuxaliaRevitToolkit.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CenterElementCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            try
            {
                // Prompt user to select an element
                Reference elementRef = uiDoc.Selection.PickObject(ObjectType.Element, "Select an element");
                Element element = doc.GetElement(elementRef);

                // Prompt user to select two points
                XYZ point1 = uiDoc.Selection.PickPoint("Select first point");
                XYZ point2 = uiDoc.Selection.PickPoint("Select second point");

                // Calculate center point
                XYZ centerPoint = (point1 + point2) / 2;

                // Centering the element: this depends on the type of element and may involve moving it
                // This is a simple example and might need adjustment depending on element types
                LocationPoint locationPoint = element.Location as LocationPoint;
                if (locationPoint != null)
                {
                    using (Transaction trans = new Transaction(doc, "Center Element"))
                    {
                        trans.Start();
                        locationPoint.Point = centerPoint;
                        trans.Commit();
                    }
                }

                TaskDialog.Show("Success", "Element has been centered.");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
