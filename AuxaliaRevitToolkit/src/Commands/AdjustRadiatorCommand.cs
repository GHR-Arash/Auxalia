using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace AuxaliaRevitToolkit.Commands
{

    [Transaction(TransactionMode.Manual)]
    public class AdjustRadiatorCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            try
            {
                using (Transaction trans = new Transaction(doc, "Adjust Radiators"))
                {
                    trans.Start();

                    // Collect all windows
                    FilteredElementCollector windows = new FilteredElementCollector(doc)
                        .OfCategory(BuiltInCategory.OST_Windows)
                        .WhereElementIsNotElementType();

                    // Collect all radiators
                    FilteredElementCollector radiators = new FilteredElementCollector(doc)
                        .OfCategory(BuiltInCategory.OST_SpecialityEquipment) // Update category as needed
                        .WhereElementIsNotElementType();

                    foreach (FamilyInstance window in windows)
                    {
                        // Get window width and location
                        Parameter windowWidthParam = window.get_Parameter(BuiltInParameter.WINDOW_WIDTH);
                        if (windowWidthParam == null) continue;
                        double windowWidth = windowWidthParam.AsDouble();

                        LocationPoint windowLocation = window.Location as LocationPoint;
                        if (windowLocation == null) continue;

                        // Find the nearest radiator to the window
                        FamilyInstance nearestRadiator = FindNearestRadiator(windowLocation, radiators);
                        if (nearestRadiator == null) continue;

                        // Set radiator width to match the window's width
                        Parameter radiatorWidthParam = nearestRadiator.LookupParameter("Width"); // Replace "Width" with your parameter name
                        if (radiatorWidthParam != null)
                        {
                            radiatorWidthParam.Set(windowWidth);
                        }
                    }

                    trans.Commit();
                }

                TaskDialog.Show("Result", "Radiator widths adjusted to match nearest windows.");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                TaskDialog.Show("Error", message);
                return Result.Failed;
            }
        }

        private FamilyInstance FindNearestRadiator(LocationPoint windowLocation, FilteredElementCollector radiators)
        {
            FamilyInstance nearestRadiator = null;
            double minDistance = double.MaxValue;

            foreach (FamilyInstance radiator in radiators)
            {
                LocationPoint radiatorLocation = radiator.Location as LocationPoint;
                if (radiatorLocation == null) continue;

                double distance = GetDistance(windowLocation, radiatorLocation);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestRadiator = radiator;
                }
            }

            return nearestRadiator;
        }

        private double GetDistance(LocationPoint point1, LocationPoint point2)
        {
            return (point1.Point - point2.Point).GetLength();
        }
    }
}