public class Alarm
{
    private final double LowPressureThreshold = 17;
    private final double HighPressureThreshold = 21;

    Sensor sensor;
    
    public Alarm(Sensor injSensor)  // test constructor, with injection of test stub
    {
        sensor = injSensor;
    }
    
    public Alarm()  // default constructor
    {
        sensor = new Sensor(); // instantiate real sensor class
    }

    boolean alarmOn = false;

    public void check()
    {
        double psiPressureValue = sensor.popNextPressurePsiValue();

        if (psiPressureValue < LowPressureThreshold || HighPressureThreshold < psiPressureValue)
        {
            alarmOn = true;
        }
    }

    public boolean isAlarmOn()
    {
        return alarmOn;
    }
}
