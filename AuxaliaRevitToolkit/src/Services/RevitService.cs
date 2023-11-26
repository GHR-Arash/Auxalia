using System.Collections.Generic;
using System.Linq;
namespace AuxaliaRevitToolkit.Services
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using AuxaliaRevitToolkit.Models;
    using AuxaliaRevitToolkit.Utilities;

    public class RevitService
    {
    
        private readonly Document _doc;

        public RevitService(UIApplicationWrapper uiAppWrapper)
        {
            _doc = uiAppWrapper.UIApplication.ActiveUIDocument.Document;
        }

        public List<LevelModel> GetLevels()
        {
            List<LevelModel> levels = new List<LevelModel>();

            // Start a read-only transaction (if necessary)
            using (Transaction tx = new Transaction(_doc, "Get Levels"))
            {
                tx.Start();

                // Retrieve all Level elements from the Revit document
                FilteredElementCollector collector = new FilteredElementCollector(_doc);
                ICollection<Element> levelElements = collector.OfClass(typeof(Level)).ToElements();
                foreach (Element element in levelElements)
                {
                    Level revitLevel = element as Level;
                    if (revitLevel != null)
                    {
                        // Convert Revit Level to LevelModel
                        LevelModel levelModel = new LevelModel
                        {
                            Name = revitLevel.Name,
                            Elevation = revitLevel.Elevation,
                            // BasePoint property needs to be set based on your requirements
                        };
                        levels.Add(levelModel);
                    }
                }
                tx.Commit();
            }
            return levels;
        }

        public void CreateLevel(string name, double elevation)
        {
            using (Transaction tx = new Transaction(_doc, "Create Level"))
            {
                tx.Start();

                // Create a new level at the specified elevation
                Level newLevel = Level.Create(_doc, elevation);
                if (newLevel != null)
                {
                    newLevel.Name = name;
                }

                tx.Commit();
            }
        }

        public void DeleteLevel(string levelName)
        {
            using (Transaction tx = new Transaction(_doc, "Delete Level"))
            {
                tx.Start();

                // Find the level by name
                FilteredElementCollector collector = new FilteredElementCollector(_doc);
                Level levelToDelete = collector.OfClass(typeof(Level))
                                                .FirstOrDefault(e => e.Name.Equals(levelName)) as Level;

                if (levelToDelete != null)
                {
                    _doc.Delete(levelToDelete.Id); // Delete the level
                }

                tx.Commit();
            }
        }
    }
}
