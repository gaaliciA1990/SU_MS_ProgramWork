/**
 * Author: Alicia Garcia
 * Version: 1.0
 * Date: 4/15/2022 14:56
 */


package Q4;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;

import static org.junit.jupiter.api.Assertions.*;


public class CalcTests {
    @Test
    public void testAdd() {
        assertEquals(5, Calc.add(2, 3), "Calc sum incorrect");
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/Q4testSubtractData.csv", numLinesToSkip = 1)
    public void testSubtract(float a, float b, float result) {
        assertEquals(result, Calc.subtract(a, b), 0.001);
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/Q4testMultiplyData.csv", numLinesToSkip = 1)
    public void testMultiply(float a, float b, float result) {
        assertEquals(result, Calc.multiply(a, b), 0.001);
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/Q4testDivideData.csv", numLinesToSkip = 1)
    public void testDivide(float a, float b, float result) {
        assertEquals(result, Calc.divide(a,b), 0.001);
    }
}
