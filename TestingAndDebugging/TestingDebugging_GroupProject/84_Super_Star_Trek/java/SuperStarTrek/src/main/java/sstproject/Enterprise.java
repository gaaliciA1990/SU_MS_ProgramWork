package sstproject;

import java.util.Locale;
import java.util.stream.IntStream;

/**
 * The starship sstproject.Enterprise.
 */
public class Enterprise {
    public Util util;
    public static final int COORD_X = 0;
    public static final int COORD_Y = 1;

    // devices
    static final int DEVICE_WARP_ENGINES = 0;
    static final int DEVICE_SHORT_RANGE_SENSORS = 1;
    static final int DEVICE_LONG_RANGE_SENSORS = 2;
    static final int DEVICE_PHASER_CONTROL = 3;
    static final int DEVICE_PHOTON_TUBES = 4;
    static final int DEVICE_DAMAGE_CONTROL = 5;
    static final int DEVICE_SHIELD_CONTROL = 6;
    static final int DEVICE_LIBRARY_COMPUTER = 7;
    final double[] deviceStatus = new double[8];   // 8  device damage stats

    // position
    final int[][] cardinalDirections = new int[10][3];   // 9x2 vectors in cardinal directions
    int quadrantX;
    int quadrantY;
    int sectorX;
    int sectorY;

    // ship status
    boolean docked = false;
    int energy = 3000;
    int torpedoes = 10;
    int shields = 0;
    double repairCost;

    final int initialEnergy = energy;
    final int initialTorpedoes = torpedoes;

    public Enterprise(Util util) {
        this.util = util;

        // random initial position
        this.setQuadrant(util.fnr(), util.fnr());
        this.setSector(util.fnr(), util.fnr());

        // init cardinal directions
        IntStream.range(1, 9).forEach(i -> {
            cardinalDirections[i][1] = 0;
            cardinalDirections[i][2] = 0;
        });
        cardinalDirections[3][1] = -1;
        cardinalDirections[2][1] = -1;
        cardinalDirections[4][1] = -1;
        cardinalDirections[4][2] = -1;
        cardinalDirections[5][2] = -1;
        cardinalDirections[6][2] = -1;
        cardinalDirections[1][2] = 1;
        cardinalDirections[2][2] = 1;
        cardinalDirections[6][1] = 1;
        cardinalDirections[7][1] = 1;
        cardinalDirections[8][1] = 1;
        cardinalDirections[8][2] = 1;
        cardinalDirections[9][2] = 1;

        // init devices
        for (int i = 1; i < 8; i++) {
            deviceStatus[i] = 0;
        }
    }

    public int getShields() {
        return shields;
    }

    /**
     * sstproject.Enterprise is hit by enemies.
     *
     * @param hits the number of hit points
     */
    public void sufferHitPoints(int hits) {
        this.shields = shields - hits;
    }

    public int getEnergy() {
        return energy;
    }

    public void replenishSupplies() {
        this.energy = this.initialEnergy;
        this.torpedoes = this.initialTorpedoes;
    }

    public void decreaseEnergy(final double amount) {
        this.energy -= amount;
    }

    public void decreaseTorpedoes(final int amount) {
        torpedoes -= amount;
    }

    public void dropShields() {
        this.shields = 0;
    }

    public int getTotalEnergy() {
        return (shields + energy);
    }

    public int getInitialEnergy() {
        return initialEnergy;
    }

    public int getTorpedoes() {
        return torpedoes;
    }

    public double[] getDeviceStatus() {
        return deviceStatus;
    }

    public int[][] getCardinalDirections() {
        return cardinalDirections;
    }

    public void setDeviceStatus(final int device, final double status) {
        this.deviceStatus[device] = status;
    }

    public boolean isDocked() {
        return docked;
    }

    public void setDocked(boolean docked) {
        this.docked = docked;
    }

    public int[] getQuadrant() {
        return new int[]{quadrantX, quadrantY};
    }

    public void setQuadrant(int x, int y) {
        this.quadrantX = x;
        this.quadrantY = y;
    }

    public int[] getSector() {
        return new int[]{sectorX, sectorY};
    }

    public void setSector(int x, int y) {
        this.sectorX = x;
        this.sectorY = y;
    }

    public int[] moveShip(final float course, final int n, final String quadrantMap,
                          final double stardate, final double initialStardate,
                          final int missionDuration, final GameCallback callback) {
        int ic1 = util.toInt(course);
        float x1 = cardinalDirections[ic1][1] +
                   (cardinalDirections[ic1 + 1][1] - cardinalDirections[ic1][1]) * (course - ic1);
        float x = sectorX;
        float y = sectorY;
        float x2 = cardinalDirections[ic1][2] +
                   (cardinalDirections[ic1 + 1][2] - cardinalDirections[ic1][2]) * (course - ic1);
        final int initialQuadrantX = quadrantX;
        final int initialQuadrantY = quadrantY;

        for (int i = 1; i <= n; i++) {
            sectorX += x1;
            sectorY += x2;
            if (sectorX < 1 || sectorX >= 9 || sectorY < 1 || sectorY >= 9) {
                // exceeded quadrant limits
                x = 8 * quadrantX + x + n * x1;
                y = 8 * quadrantY + y + n * x2;
                quadrantX = util.toInt(x / 8);
                quadrantY = util.toInt(y / 8);
                sectorX = util.toInt(x - quadrantX * 8);
                sectorY = util.toInt(y - quadrantY * 8);

                if (sectorX == 0) {
                    quadrantX = quadrantX - 1;
                    sectorX = 8;
                }

                if (sectorY == 0) {
                    quadrantY = quadrantY - 1;
                    sectorY = 8;
                }

                boolean hitEdge = false;

                if (quadrantX < 1) {
                    hitEdge = true;
                    quadrantX = 1;
                    sectorX = 1;
                }

                if (quadrantX > 8) {
                    hitEdge = true;
                    quadrantX = 8;
                    sectorX = 8;
                }

                if (quadrantY < 1) {
                    hitEdge = true;
                    quadrantY = 8;
                    sectorY = 8;
                }

                if (quadrantY > 8) {
                    hitEdge = true;
                    quadrantY = 8;
                    sectorY = 8;
                }
                if (hitEdge) {
                    util.println("LT. UHURA REPORTS MESSAGE FROM STARFLEET COMMAND:");
                    util.println("  'PERMISSION TO ATTEMPT CROSSING OF GALACTIC PERIMETER");
                    util.println("  IS HEREBY *DENIED*.  SHUT DOWN YOUR ENGINES.'");
                    util.println("CHIEF ENGINEER SCOTT REPORTS  'WARP ENGINES SHUT DOWN");
                    util.println("  AT SECTOR " +
                                 sectorX +
                                 "," +
                                 sectorY +
                                 " OF QUADRANT " +
                                 quadrantX +
                                 "," +
                                 quadrantY +
                                 ".'");

                    if (stardate > initialStardate + missionDuration) {
                        callback.endGameFail(false);
                    }
                }
                // check both sides if the addition should happen first or the multiplication.
                // Parenthesis needed if addition should happen first.
                if (8 * quadrantX + quadrantY == 8 * initialQuadrantX + initialQuadrantY) {
                    break;
                }

                callback.incrementStardate(1);
                maneuverEnergySR(n);
                callback.enterNewQuadrant();

                return this.getSector();
            } else {
                int S8 = util.toInt(sectorX) * 24 + util.toInt(sectorY) * 3 - 26; // S8 = pos
                if (!("  ".equals(util.midStr(quadrantMap, S8, 2)))) {
                    sectorX = util.toInt(sectorX - x1);
                    sectorY = util.toInt(sectorY - x2);
                    util.println("WARP ENGINES SHUT DOWN AT ");
                    util.println("SECTOR " + sectorX + "," + sectorY + " DUE TO BAD NAVIGATION");
                    break;
                }
            }
        }
        sectorX = util.toInt(sectorX);
        sectorY = util.toInt(sectorY);

        return this.getSector();
    }

    void randomRepairCost() {
        repairCost = .5 * util.random();
    }

    public void repairDamagedDevices(final float warp) {
        // repair damaged devices and print damage report
        for (int i = 0; i < deviceStatus.length; i++) {
            if (deviceStatus[i] < 0) {
                deviceStatus[i] += Math.min(warp, 1);
                if ((deviceStatus[i] > -.1) && (deviceStatus[i] < 0)) {
                    deviceStatus[i] = -.1;
                    break;
                } else if (deviceStatus[i] >= 0) {
                    util.println("DAMAGE CONTROL REPORT:  ");
                    util.println(util.tab(8) + printDeviceName(i) + " REPAIR COMPLETED.");
                }
            }
        }
    }

    public void maneuverEnergySR(final int N) {
        energy = energy - N - 10;

        if (energy >= 0) {
            return;
        }

        util.println("SHIELD CONTROL SUPPLIES ENERGY TO COMPLETE THE MANEUVER.");
        shields = shields + energy;
        energy = 0;

        if (shields <= 0) {
            shields = 0;
        }
    }

    /**
     * TODO: Consider refactoring method to allow for increase of same number
     */
    void shieldControl() {
        if (deviceStatus[DEVICE_SHIELD_CONTROL] < 0) {
            util.println("SHIELD CONTROL INOPERABLE");
            return;
        }

        util.println("ENERGY AVAILABLE = " + (energy + shields));
        int energyToShields = util.toInt(util.inputFloat("NUMBER OF UNITS TO SHIELDS"));

        if (energyToShields < 0 || shields == energyToShields) {
            util.println("<SHIELDS UNCHANGED>");
            return;
        }
        if (energyToShields > energy) {
            util.println("SHIELD CONTROL REPORTS  'THIS IS NOT THE FEDERATION TREASURY.'");
            util.println("<SHIELDS UNCHANGED>");
            return;
        }

        energy = energy + shields - energyToShields;
        shields = energyToShields;

        util.println("DEFLECTOR CONTROL ROOM REPORT:");
        util.println("  'SHIELDS NOW AT " + util.toInt(shields) + " UNITS PER YOUR COMMAND.'");
    }

    void damageControl(GameCallback callback) {
        if (deviceStatus[DEVICE_DAMAGE_CONTROL] <= 0) {
            util.println("ALL SYSTEMS OPERABLE! NO DAMAGE TO REPORT!");
        } else {
            util.println("\nDEVICE STATE OF REPAIR");
            for (int deviceNr = 0; deviceNr < deviceStatus.length; deviceNr++) {
                util.print(printDeviceName(deviceNr) +
                           util.leftStr(GalaxyMap.QUADRANT_ROW,
                                        25 - util.strLen(printDeviceName(deviceNr))) +
                           " " +
                           util.toInt(deviceStatus[deviceNr] * 100) * .01 +
                           "\n");
            }
        }

        if (!docked) {
            util.println("DEVICE NOT DOCKED!");
            return;
        }

        double deltaToRepair = 0;

        for (int i = 0; i < deviceStatus.length; i++) {
            if (deviceStatus[i] < 0) {
                deltaToRepair += .1;
                util.println("DELTA REPAIR INCREASED FOR " + deviceStatus[i]);
            }
        }

        if (deltaToRepair > 0) {
            deltaToRepair += repairCost;

            if (deltaToRepair >= 1) {
                deltaToRepair = .9;
            }

            util.println("TECHNICIANS STANDING BY TO EFFECT REPAIRS TO YOUR SHIP;");
            util.println("ESTIMATED TIME TO REPAIR:'" +
                         .01 * util.toInt(100 * deltaToRepair) +
                         " STARDATES");

            final String reply = util.inputStr("WILL YOU AUTHORIZE THE REPAIR ORDER (Y/N)");

            if ("Y".equals(reply.toUpperCase(Locale.ROOT))) {
                for (int deviceNr = 0; deviceNr < deviceStatus.length; deviceNr++) {
                    if (deviceStatus[deviceNr] < 0) {
                        deviceStatus[deviceNr] = 0;
                    }
                }
                callback.incrementStardate(deltaToRepair + .1);
            }
        }
    }

    public static String printDeviceName(final int deviceNumber) {
        // PRINTS DEVICE NAME
        switch (deviceNumber) {
            case DEVICE_WARP_ENGINES:
                return "WARP ENGINES";
            case DEVICE_SHORT_RANGE_SENSORS:
                return "SHORT RANGE SENSORS";
            case DEVICE_LONG_RANGE_SENSORS:
                return "LONG RANGE SENSORS";
            case DEVICE_PHASER_CONTROL:
                return "PHASER CONTROL";
            case DEVICE_PHOTON_TUBES:
                return "PHOTON TUBES";
            case DEVICE_DAMAGE_CONTROL:
                return "DAMAGE CONTROL";
            case DEVICE_SHIELD_CONTROL:
                return "SHIELD CONTROL";
            case DEVICE_LIBRARY_COMPUTER:
                return "LIBRARY-COMPUTER";
        }
        return "";
    }

}
