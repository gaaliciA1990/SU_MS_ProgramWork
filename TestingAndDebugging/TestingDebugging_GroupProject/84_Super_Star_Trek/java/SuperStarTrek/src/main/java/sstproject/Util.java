package sstproject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.Random;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

/**
 * Convenience utility methods for the Super Star Trek game.
 */
public class Util {
    final Random random;
    final BufferedReader reader;

    public Util(Random random, BufferedReader reader) {
        this.random = random;
        this.reader = reader;

    }

    public float random() {
        return random.nextFloat();
    }

    public int fnr() {    // 475
        // Generate a random integer from 1 to 8 inclusive.
        return toInt(random() * 7 + 1);
    }

    public int toInt(final double num) {
        int x = (int) Math.floor(num);

        if (x < 0) {
            x *= -1;
        }
        return x;
    }

    /**
     * This is being used as an override for System.out.println
     * <p>
     * We have confirmed this is functioning in our tests through mockito
     *
     * @param s
     */
    public void println(final String s) {
        System.out.println(s);
    }

    /**
     * This is being used as an override for System.out.print
     * <p>
     * We have confirmed this is functioning in our tests through mockito
     *
     * @param s
     */

    public void print(final String s) {
        System.out.print(s);
    }

    public String tab(final int n) {
        return IntStream.range(1, n).mapToObj(num -> " ").collect(Collectors.joining());
    }

    /**
     * Returns the number of chars in a string. If null or empty, will always return 0.
     *
     * @param s - input string
     * @return - number of letters in the string
     */
    public int strLen(final String s) {
        if (s == null || s.length() <= 0) {
            return 0;
        }
        return s.length();
    }

    public String inputStr(final String message) {
        System.out.print(message + "? ");

        try {
            return reader.readLine();
        } catch (IOException ioe) {
            //ioe.printStackTrace();
            return "";
        }
    }

    public int[] inputCoords(final String message) {
        while (true) {
            final String input = inputStr(message);
            try {
                final String[] splitInput = input.split(",");
                if (splitInput.length == 2) {
                    int x = Integer.parseInt(splitInput[0]);
                    int y = Integer.parseInt(splitInput[1]);
                    return new int[]{x, y};
                }
            } catch (Exception e) {
                //e.printStackTrace();
                throw new IllegalArgumentException("Invalid Coords");
            }
        }
    }

    public float inputFloat(final String message) {
        while (true) {
            System.out.print(message + "? ");
            try {
                final String input = reader.readLine();
                if (input.length() > 0) {
                    return Float.parseFloat(input);
                }
            } catch (Exception e) {
                //e.printStackTrace();
                throw new NullPointerException("NUMBER IS NULL");
            }
        }
    }

    /**
     * Returns the first len characters of the input string.
     *
     * @param input - input string to parse from
     * @param len   - how many chars we want to remove from input
     * @return - the substring
     */
    public String leftStr(final String input, final int len) {
        if (len < 0) {
            return input;
        }

        if (input == null || input.length() < len) {
            return input;
        }

        return input.substring(0, len);
    }

    /**
     * Returns the mid section of the string based on the start position minus the len.
     *
     * @param input - input string to parse from
     * @param start - where to begin the parse of substring
     * @param len   - the end of the parse of substring
     * @return - the substring
     */
    public String midStr(final String input, final int start, final int len) {
        if (start < 0 || len < 0) {
            return input;
        }

        if (input == null || input.length() < ((start - 1) + len)) {
            return input;
        }

        return input.substring(start - 1, (start - 1) + len);
    }

    /**
     * Returns the right side of the string minus len
     *
     * @param input - string input by player
     * @param len   - number of chars to return in substring
     * @return - the substring
     */
    public String rightStr(final String input, final int len) {
        if (len < 0) {
            return "";
        }

        if (input == null || input.length() < len) {
            return "";
        }

        return input.substring(input.length() - len);
    }

    public double round(double value, int places) {
        if (places < 0) {
            throw new IllegalArgumentException();
        }

        BigDecimal bd = new BigDecimal(Double.toString(value));
        bd = bd.setScale(places, RoundingMode.HALF_UP);

        return bd.doubleValue();
    }
}
