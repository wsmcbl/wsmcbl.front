namespace wsmcbl.src.Model.Secretary;

public class StudentMeasurements
{
    public int measurementId { get; set; }
    public int height { get; set; }
    public int weight { get; set; }

    public void SetDefaultValues()
    {
        if (height != 0 || weight != 0)
        {
            return;
        }
        
        height = 100;
        weight = 100;
    }
    
    public bool IsMeasurementsValid()
    {
        return (height is >= 50 and <= 300) && (weight is >= 5 and <= 200);
    }
    
}