namespace wsmcbl.src.View.Components.Charts;

public class DistributionDto
{
    public int total { get; set; }
    public int males { get; set; }
    public int droppedOut { get; set; }
    public List<EducationalLevel> levelList { get; set; } = new();
    public List<Degree> degreeList { get; set; } = new();

    public class EducationalLevel
    {
        public int educationalLevel { get; set; }
        public int total { get; set; }
        public int males { get; set; }
    }

    public class Degree
    {
        public string label { get; set; } = "N/A";
        public int educationalLevel { get; set; }
        public int position { get; set; }
        public int total { get; set; }
    }
}