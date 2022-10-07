import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;


/**
 * "The Art of Software Testing, Second Edition" by Glenford Myers.
 * 
 * Given three integer values representing the lengths of the sides of a triangle, determine what
 * type of triangle it is: scalene, isosceles, or equilateral.
 * 
 */
public class TriangleTypeTest {

    /**
     * Test case for valid scalene triangle.
     * 
     * <i>(1) Do you have a test case that represents a valid scalene triangle? (Note that test
     * cases such as 1, 2, 3 and 2, 5, 10 do not warrant a "yes" answer because there does not exist
     * a triangle having these dimensions.)</i>
     */
    @Test
    public void classify_AsScalene_WhenTheThreeSidesAreDifferent() {
        assertEquals(TriangleType.SCALENE, TriangleType.classify(3, 4, 5));
    }

    /**
     * Test case for valid equilateral triangle.
     * 
     * <i>(2) Do you have a test case that represents a valid equilateral triangle?</i>
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/equilateralEqualSidesData.csv", numLinesToSkip = 1)
    public void classify_AsEquilateral_WhenTheThreeSidesAreEqual(int a, int b, int c) {
        assertEquals(TriangleType.EQUILATERAL, TriangleType.classify(a, b, c));
    }

    /**
     * Test cases for valid isosceles triangles.
     * 
     * <ul>
     * <li><i>(3) Do you have a test case that represents a valid isosceles triangle? (Note that a
     * test case representing 2, 2, 4 would not count because it is not a valid triangle)</i>
     * <li><i>(4) Do you have at least three test cases that represent valid isosceles triangles
     * such that you have tried all three permutations of two equal sides (such as, 3, 3, 4; 3, 4,
     * 3; and 4, 3, 3)?</i>
     * </ul>
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/isoscelesTwoEqualSidesData.csv", numLinesToSkip = 1)
    public void classify_AsIsosceles_WhenTwoSidesAreEqual(int a, int b, int c) {
        assertEquals(TriangleType.ISOSCELES, TriangleType.classify(a, b, c));
    }

    /**
     * Test cases for invalid triangles with a zero side length.
     * <ul>
     * <li><i>(5) Do you have a test case in which one side has a zero value?</i>
     * <li><i>(11) Do you have a test case in which all sides are zero (0, 0, 0)?</i>
     * </ul>
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/invalidTriangleZeroSideData.csv", numLinesToSkip = 1)
    public void classify_AsInvalid_WhenAnySideIsZero(int a, int b, int c) {
        assertEquals(TriangleType.INVALID, TriangleType.classify(a, b, c));
    }

    /**
     * Test cases for invalid triangles with a negative side length.
     * 
     * <i>(6) Do you have a test case in which one side has a negative value?</i>
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/invalidTriangleNegativeSideData.csv", numLinesToSkip = 1)
    public void classify_AsInvalid_WhenAnySideIsNegative(int a, int b, int c) {
        assertEquals(TriangleType.INVALID, TriangleType.classify(a, b, c));
    }

    /**
     * Test cases for invalid triangles with the sum of two sides equal to the third.
     * <ul>
     * <li><i>(7) Do you have a test case with three integers greater than zero such that the sum of
     * two of the numbers is equal to the third? (That is, if the program said that 1, 2, 3
     * represents a scalene triangle, it would contain a bug.)</i>
     * <li><i>(8) Do you have at least three test cases in category (7) such that you have tried all
     * three permutations where the length of one side is equal to the sum of the lengths of the
     * other two sides (for example, 1, 2, 3; 1, 3, 2; and 3, 1, 2)?</i>
     * </ul>
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/invalidTriangleSumOfTwoSidesEqualsThirdData.csv", numLinesToSkip = 1)
    public void classify_AsInvalid_WhenTheSumOfTwoSidesIsEqualToTheThird(int a, int b, int c) {
        assertEquals(TriangleType.INVALID, TriangleType.classify(a, b, c));
    }

    /**
     * Test cases for invalid triangles with the sum of two sides less than the third.
     * 
     * <ul>
     * <li><i>(9) Do you have a test case with three integers greater than zero such that the sum of
     * two of the numbers is less than the third (such as 1, 2, 4 or 12,15,30)?</i>
     * <li><i>(10) Do you have at least three test cases in category 9 such that you have tried all
     * three permutations (for example, 1, 2, 4; 1, 4, 2; and 4, 1, 2)?</i>
     * </ul>
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/invalidTriangleSumOfTwoSidesLessThanThirdData.csv", numLinesToSkip = 1)
    public void classify_AsInvalid_WhenTheSumOfTwoSidesIsLessThanTheThird(int a, int b, int c) {
        assertEquals(TriangleType.INVALID, TriangleType.classify(a, b, c));
    }

    /**
     * Test case for when overflow happens using large numbers, like Max_Value (2147483647). This will throw an
     * ArithmeticException.
     * For this test, our max param values are set to 2147483640
     */
    @ParameterizedTest
    @CsvFileSource(resources = "/overflowIntegerTriangleData.csv", numLinesToSkip = 1)
    public void classify_sideFormTriangle_WhenIntOverflow_ThrowsException(int a, int b, int c) {
        // when

        //do
        Exception exception = assertThrows(ArithmeticException.class, () -> {
            TriangleType.classify(a, b, c);
        });

        // assert
        assertEquals("Overflow error, invalid triangle", exception.getMessage());
    }
}