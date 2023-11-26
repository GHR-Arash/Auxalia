namespace AuxaliaRevitToolkit.Models
{
    public class LevelModel
    {
        public string Name { get; set; }
        public double Elevation { get; set; }
        public BasePointEnum BasePoint { get; set; }

        // Enum for base point
        public enum BasePointEnum
        {
            ProjectBasePoint,
            SurveyPoint,
            InternalOrigin
        }
    }
}
