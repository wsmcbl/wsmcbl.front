namespace wsmcbl.src.Model.Secretary;

public class StudentMeasurements
{
    public int measurementId { get; set; }
    public int height { get; set; }
    public int weight { get; set; }

    public bool IsMeasurementsValid()
    {
        if (height < 50 || height > 300 || weight < 5 || weight > 200)
        {
            return false;
        }

        return true;
    }
    
}