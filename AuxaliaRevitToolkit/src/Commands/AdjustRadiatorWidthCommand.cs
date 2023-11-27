using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System;
using System.Linq;
using Autodesk.Revit.UI.Selection;

namespace AuxaliaRevitToolkit.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class AdjustRadiatorWidthCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                // Select a window
                Reference pickedRef = uidoc.Selection.PickObject(ObjectType.Element,  "Select a window");
                Element window = doc.GetElement(pickedRef);

                // Select a radiator
                pickedRef = uidoc.Selection.PickObject(ObjectType.Element,  "Select a radiator");
                Element radiator = doc.GetElement(pickedRef);

                // Change radiator width to match window width
                if (!AdjustRadiatorWidth(doc, window, radiator))
                {
                    TaskDialog.Show("Error", "Unable to adjust radiator width.");
                    return Result.Failed;
                }

                TaskDialog.Show("Success", "The radiator width has been adjusted to match the window width.");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        private bool AdjustRadiatorWidth(Document doc, Element window, Element radiator)
        {
            // Assuming width is a parameter of the elements
            Parameter windowWidthParam = window.get_Parameter(BuiltInParameter.WINDOW_WIDTH);
            Parameter radiatorWidthParam = radiator.get_Parameter(new Guid("cc38aa36-7aeb-43f3-9beb-b8634bbd5357")); // Adjust the parameter name as needed

            if (windowWidthParam != null && radiatorWidthParam != null)
            {
                using (Transaction trans = new Transaction(doc, "Adjust Radiator Width"))
                {
                    trans.Start();
                    radiatorWidthParam.Set(windowWidthParam.AsDouble());
                    trans.Commit();
                    return true;
                }
            }
            return false;
        }
    }
}
