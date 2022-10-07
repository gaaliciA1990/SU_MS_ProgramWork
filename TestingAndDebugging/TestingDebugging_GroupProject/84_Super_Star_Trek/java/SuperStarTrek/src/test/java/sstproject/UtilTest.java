package sstproject;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;
import sstproject.Util;


import java.io.BufferedReader;
import java.io.IOException;
import java.util.Random;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

/**
 * Author: Alicia Garcia, Mustafa Abuthuraya, Sanchita Jain
 * Version: 1.0
 * Date: 4/20/2022 12:30
 * **********************************************************************
 * **********************************************************************
 * **********************************************************************
 * *********************  READ ME!   **************************************
 * THE TESTS BELOW ARE PLACEHOLDER AND SHOULD BE USED AS A GUIDE TO IMPLEMENT
 * YOUR TESTS AND DETERMINING WHAT TO TEST. PLEASE BE SURE TO CLAIM YOUR SECTION
 * OF CODE YOU'RE TESTING AND PUSH YOUR CHANGES IMMEDIATELY AND NOTIFY THE TEAM
 * **********************************************************************
 * **********************************************************************
 * **********************************************************************
 */

class UtilTest {
    Random rand;
    BufferedReader inputReader;

    @BeforeEach
    public void utilSetUp() {
        this.rand = mock(Random.class, withSettings().withoutAnnotations());
        this.inputReader = mock(BufferedReader.class);

    }

    /**
     * OWNER: MUSTAFA
     */
    @Test
    void toInt_returns_int_equal_Negative1000_when_user_enters_1000() {
        // ARRANGE
        Util util = new Util(rand, inputReader);
        int expectedResult = 1000;

        // ACT
        int result = util.toInt((float) -1000);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: MUSTAFA
     * toInt returns 0 when user inputs 0 of type float
     */
    @Test
    void toInt_returns_int_equal_zero_when_user_enters_float_zero() {
        // ARRANGE
        Util util = new Util(rand, inputReader);
        int expectedResult = 0;

        // ACT
        int result = util.toInt((float) 0);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: MUSTAFA
     * toInt returns 1 when user inputs -1.0
     */
    @Test
    void toInt_returns_int_equal_1_when_user_enters_a_negative_double_equalTo_1() {
        // ARRANGE
        Util util = new Util(rand, inputReader);
        int expectedResult = 1;

        // ACT
        int result = util.toInt((float) -1);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: MUSTAFA
     * toInt returns 100 when user inputs 100.5
     */
    @Test
    void toInt_returns_int_equal_100_when_user_enters_a_double_equalTo_100Point5() {
        // ARRANGE
        Util util = new Util(rand, inputReader);
        int expectedResult = 100;

        // ACT
        int result = util.toInt(100.5);

        // ASSERT
        assertEquals(expectedResult, result);
    }


    /**
     * OWNER: Sanchita
     * convert the system.out to a buffer stream then verify is the required string is present in the bytestream.
     */
    @Test
    void inputStr_verify_if_system_println_prints_message_with_question_mark_appended() throws IOException {
        // ARRANGE
        //Redirect System.out to buffer
        ByteArrayOutputStream bo = new ByteArrayOutputStream();
        System.setOut(new PrintStream(bo));
        Util util = new Util(rand, inputReader);
        String expectedResult = "Input?";
        // ACT
        util.inputStr("Input");
        bo.flush();
        String allWrittenLines = new String(bo.toByteArray());

        // ASSERT
        assertTrue(allWrittenLines.contains(expectedResult));
    }

    /**
     * OWNER: Sanchita
     * the exception is always raised when the readline() method is called.
     * It is not an option to refactor this as the readline() method needs to be enclosed in a try catch block
     */
    @Test
    void inputStr_verify_if_IOexception_is_raised() throws IOException {
        // ARRANGE
        Util util = new Util(rand, inputReader);
        String expectedResult = "";
        String invalidinput = "input \n input1";

        // ACT

        when(inputReader.readLine()).thenThrow(new IOException());
        String result = util.inputStr(invalidinput);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: MUSTAFA
     * When the user inputs 3,5 in the command line, inputCoords will return an array [3,5]
     */
    @Test
    void inputCoords_returns_array_of_3_and_5_when_user_inputs_3_and_5_in_commandLine() throws IOException, Exception {
        Util util = new Util(rand, inputReader);

        //ARRANGE
        when(inputReader.readLine()).thenReturn("3,5");

        int[] result;
        int[] expectedResult = {3, 5};

        // ACT
        result = util.inputCoords("  FINAL COORDINATES (X,Y)");

        // ASSERT
        assertArrayEquals(expectedResult, result);
    }

    /**
     * OWNER: MUSTAFA
     * When the user inputs null, an exception is thrown
     */
    @Test
    void inputCoords_throwException_when_user_inputs_invalid_input_null() throws IOException {
        Util util = new Util(rand, inputReader);

        //ARRANGE
        when(inputReader.readLine()).thenReturn(null);

        // ACT
        // ASSERT
        assertThrows(IllegalArgumentException.class, () -> util.inputCoords("  FINAL COORDINATES (X,Y)"));
    }

    /**
     * OWNER: Sanchita
     * validate if inputfloat converts the string passed to a float value
     */
    @Test
    void inputFloat_returns_the_corresponding_float_value() throws Exception {
        Util util = new Util(rand, inputReader);
        Float expectedResult = 0.100f;

        // ACT
        when(inputReader.readLine()).thenReturn("0.1");

        // ASSERT
        assertEquals(expectedResult, util.inputFloat("0.1"));
    }

    /**
     * OWNER: Sanchita
     * validate when the readline() reads null it throws a null pointer exception
     */
    @Test
    void inputFloat_throws_exception_when_readline_is_called() throws Exception {

        Util util = new Util(rand, inputReader);

        // ACT
        when(inputReader.readLine()).thenThrow(new NullPointerException());

        // ASSERT
        assertThrows(NullPointerException.class, () -> util.inputFloat("0.1"));
    }


    /**
     * OWNER: ALICIA
     * <p>
     * This tests for all conditions where the input string is returned
     * Conditions:
     * 1. empty string,
     * 2. input.length() < len,
     * 3. len < 0,
     * 4. null string
     * 5. Substring is first 3 chars of input str
     *
     * @param input - input string
     * @param len   - lenght of substring returned
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/util_leftStr_Tests.csv", numLinesToSkip = 1)
    void leftStr_parameterized_tests(String input, int len, String expectedResult) {
        // ARRANGE
        Util util = new Util(rand, inputReader);

        // ACT
        String result = util.leftStr(input, len);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: ALICIA
     * <p>
     * Tests all conditions for midStr
     * Conditions:
     * 1. start < 0,
     * 2. len < 0,
     * 3. empty string returns empty string,
     * 4. returns substring starting at 5 and is 3 chars long,
     * 5. return substring with space starting at 8 and is 3 chars long,
     * 6. returns substring with special chars starting at 6 and is 3 chars long,
     * 7. null string returns null,
     * 8. input less than (start -1) + len returns input string
     *
     * @param input - input string
     * @param start - start point value
     * @param len   - length of substring value
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/util_midStr_Tests.csv", numLinesToSkip = 1)
    void midStr_parameterized_tests(String input, int start, int len, String expectedResult) {
        // ARRANGE
        Util util = new Util(rand, inputReader);

        // ACT
        String result = util.midStr(input, start, len);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: ALICIA
     * Tests all conditions for rightStr()
     * <p>
     * Conditions:
     * 1. Null input returns empty string,
     * 2. input.length() < len returns empty string,
     * 3. Empty input returns empty string,
     * 4. len = 0 returns empty string,
     * 5. len > input.length() returns empty string,
     * 6. len < 0 returns empty string,
     * 7. len = 5 returns substring of last 5 chars
     *
     * @param input - input string
     * @param len   - substring length
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/util_rightStr_Tests.csv", numLinesToSkip = 1)
    void rightStr_parameterized_tests(String input, int len, String expectedResult) {
        // ARRANGE
        Util util = new Util(rand, inputReader);

        // ACT
        String result = util.rightStr(input, len);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void random_returns_value_when_called() {
        // ARRANGE
        Util util = new Util(rand, inputReader);
        float expectedValue = 0.123f;

        when(rand.nextFloat()).thenReturn(expectedValue);

        // ACT
        float result = util.random();

        // ASSERT
        assertEquals(expectedValue, result);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void fnr_returns_integer_value_between_1_and_8() {
        // ARRANGE
        Util util = new Util(rand, inputReader);
        float randomValue = 0.251f;
        int expectedResult = 2;
        when((rand.nextFloat())).thenReturn(randomValue);

        // ACT
        int result = util.fnr();

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: ALICIA
     * <p>
     * Tests all conditions for strLen inputs
     * <p>
     * Conditions:
     * 1. NULL input,
     * 2. Empty string input
     * 3. string with 6 letters returns 6
     *
     * @param input          - input string
     * @param expectedResult - expected str length
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/util_strlen_Tests.csv", numLinesToSkip = 1)
    void strLen_returns_zero(String input, int expectedResult) {
        // ARRANGE
        Util util = new Util(rand, inputReader);

        // ACT
        int result = util.strLen(input);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: ALICIA
     * <p>
     * Tests values of n
     * <p>
     * Conditions:
     * 1. n = 0,
     * 2. n = 1,
     * 3. n = -1,
     * 4. n = -100,
     * 5. n = 6 returns string with 5 spaces
     *
     * @param n - value of spaces
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/util_Tab_Tests.csv", numLinesToSkip = 1)
    void tab_returns_no_spaces(int n, String expectedResult) {
        // ARRANGE
        Util util = new Util(rand, inputReader);

        // ACT
        String result = util.tab(n);

        // ASSERT
        assertEquals(expectedResult, result);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void round_throws_illegalArgumentException_message_null_when_places_lessThan_0() {
        // ARRANGE
        Util util = new Util(rand, inputReader);

        double value = 20.0;
        int places = -12;

        // ACT
        Exception exception = assertThrows(IllegalArgumentException.class, () -> {
            util.round(value, places);
        });

        // ASSERT
        assertNull(exception.getMessage());
    }

    /**
     * OWNER: ALICIA
     * <p>
     * Tests rounds with various values
     * <p>
     * Conditions:
     * 1. n = 20.66325854 and places = 6 returns 20.663259
     * 2. n = 20.6 and places = 0 return 21 (rounds up)
     *
     * @param value
     * @param places
     * @param expectedResult
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/util_round_tests.csv", numLinesToSkip = 1)
    void round_returns_double_value_rounded_up_to_6_decimal_places(double value, int places, double expectedResult) {
        // ARRANGE
        Util util = new Util(rand, inputReader);

        // ACT
        double result = util.round(value, places);

        // ASSERT
        assertEquals(expectedResult, result);
    }
}
