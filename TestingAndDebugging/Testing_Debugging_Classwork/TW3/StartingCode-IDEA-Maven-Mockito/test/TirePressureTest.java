import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;
import static org.junit.jupiter.api.Assertions.*;

import static org.mockito.Mockito.*;
import org.mockito.Mockito;

public class TirePressureTest
{
//    // All tests done via parameterized tests. One code block. 12 tests
//
//    @ParameterizedTest(name = "isAlarmOn: pressure={0} => Alarm= {1}")
//    @CsvSource({
//            "16.99, true",  // lower boundary - outside boundary
//            "17.0, false",  // lower boundary - ON boundary
//            "20.99, false", // upper boundary - extra test, inside boundary
//            "21.0, false",  // upper boundary - ON boundary
//            "21.01, true",  // upper boundary - outside boundary
//            "1000, true",   // very high
//            "-1000, true",  // very low
//            "-0.01, true",  // look for behavior at key values
//            "0, true",      // look for behavior at key values
//            "0.01, true",   // look for behavior at key values
//            "19, false",    // nominal success value, no alarm
//            "13, true",     // nominal failure value, alarm
//    })
//  test code goes here



    // test code that uses the "real" sensor, with random pressure values
    @Test
    public void isAlarmOn_willFailIntermittently_AlarmOnMaybe()
    {
        // TODO:  this test sometimes passes, and sometimes fails. Why?
        //  not mocked, uses "real" Sensor class
        Alarm alarm = new Alarm();
        alarm.check();
        System.out.println("expected alarm to be on, but pressure is random...");
        assertTrue(alarm.isAlarmOn(),"expected alarm to be on, but pressure is random...");
    }
}
