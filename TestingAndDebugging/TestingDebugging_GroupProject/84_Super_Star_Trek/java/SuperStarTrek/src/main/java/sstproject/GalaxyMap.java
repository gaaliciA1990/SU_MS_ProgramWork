package sstproject;

import java.util.stream.IntStream;

/**
 * Map of the galaxy divided in Quadrants and Sectors,
 * populated with stars, starbases, klingons, and the sstproject.Enterprise.
 */
public class GalaxyMap {

    Util util;
    // markers
    static final String MARKER_EMPTY = "   ";
    static final String MARKER_ENTERPRISE = "<*>";
    static final String MARKER_KLINGON = "+K+";
    static final String MARKER_STARBASE = ">!<";
    static final String MARKER_STAR = " * ";
    public static final String QUADRANT_ROW = "                         ";

    static final int AVG_KLINGON_SHIELD_ENERGY = 200;

    // galaxy map
    String quadrantMap;       // current quadrant map
    final int[][] galaxy = new int[9][9];    // 8x8 galaxy map G
    final int[][] klingonQuadrants = new int[4][4];    // 3x3 position of klingons K
    final int[][] chartedGalaxy = new int[9][9];    // 8x8 charted galaxy map Z

    // galaxy state
    int basesInGalaxy = 0;
    int remainingKlingons;
    int klingonsInGalaxy = 0;
    final Enterprise enterprise;

    // quadrant state
    int klingons = 0;
    int starbases = 0;
    int stars = 0;
    int starbaseX = 0; // X coordinate of starbase
    int starbaseY = 0; // Y coord of starbase

    public GalaxyMap(Util util, Enterprise enterprise) {
        this.util = util;
        //this.enterprise = new sstproject.Enterprise(util);
        this.enterprise = enterprise;
        this.quadrantMap = QUADRANT_ROW +
                           QUADRANT_ROW +
                           QUADRANT_ROW +
                           QUADRANT_ROW +
                           QUADRANT_ROW +
                           QUADRANT_ROW +
                           QUADRANT_ROW +
                           util.leftStr(QUADRANT_ROW, 17);

        int[] quadrant = enterprise.getQuadrant();
        int quadX = quadrant[0];
        int quadY = quadrant[1];
        int quadrantX = quadX;
        int quadrantY = quadY;

        // populate Klingons, Starbases, Stars
        IntStream.range(1, 8).forEach(x -> {
            IntStream.range(1, 8).forEach(y -> {
                klingons = 0;
                chartedGalaxy[x][y] = 0;
                float random = util.random();
                if (random > .98) {
                    klingons = 3;
                    klingonsInGalaxy += 3;
                } else if (random > .95) {
                    klingons = 2;
                    klingonsInGalaxy += 2;
                } else if (random > .80) {
                    klingons = 1;
                    klingonsInGalaxy += 1;
                }
                starbases = 0;
                if (util.random() > .96) {
                    starbases = 1;
                    basesInGalaxy = +1;
                }
                galaxy[x][y] = klingons * 100 + starbases * 10 + util.fnr();
            });
        });

        if (basesInGalaxy == 0) {
            if (galaxy[quadrantX][quadrantY] < 200) {
                galaxy[quadrantX][quadrantY] = galaxy[quadrantX][quadrantY] + 120;
                klingonsInGalaxy = +1;
            }
            basesInGalaxy = 1;
            galaxy[quadrantX][quadrantY] = +10;
            enterprise.setQuadrant(util.fnr(), util.fnr());
        }
        remainingKlingons = klingonsInGalaxy;
    }

    public Enterprise getEnterprise() {
        return enterprise;
    }

    public int getBasesInGalaxy() {
        return basesInGalaxy;
    }

    public int getRemainingKlingons() {
        return remainingKlingons;
    }

    public int getKlingonsInGalaxy() {
        return klingonsInGalaxy;
    }

    double fnd(int i) {
        return Math.sqrt((klingonQuadrants[i][1] - enterprise.getSector()[Enterprise.COORD_X]) ^
                         2 + (klingonQuadrants[i][2] - enterprise.getSector()[Enterprise.COORD_Y]) ^
                         2);
    }

    void newQuadrant(final double stardate, final double initialStardate) {   // 1320
        int[] quadrant = enterprise.getQuadrant();
        final int quadrantX = quadrant[0];
        final int quadrantY = quadrant[1];
        stars = 0;
        enterprise.randomRepairCost();

        if (!(quadrantX < 1 || quadrantX > 8 || quadrantY < 1 || quadrantY > 8)) {
            chartedGalaxy[quadrantX][quadrantY] = galaxy[quadrantX][quadrantY];
            final String quadrantName = getQuadrantName(false, quadrantX, quadrantY);

            if (initialStardate == stardate) {
                util.println("YOUR MISSION BEGINS WITH YOUR STARSHIP LOCATED\n" +
                             "IN THE GALACTIC QUADRANT, '" +
                             quadrantName +
                             "'.");
            } else {
                util.println("NOW ENTERING " + quadrantName + " QUADRANT . . .");
            }

            util.println("");
            klingons = (int) Math.round(galaxy[quadrantX][quadrantY] * .01);
            starbases = (int) Math.round(galaxy[quadrantX][quadrantY] * .1) - 10 * klingons;
            stars = galaxy[quadrantX][quadrantY] - 100 * klingons - 10 * starbases;

            if (klingons != 0) {
                util.println("COMBAT AREA      CONDITION RED");
                if (enterprise.getShields() <= 200) {
                    util.println("   SHIELDS DANGEROUSLY LOW");
                }
            }

            for (int i = 1; i < 3; i++) {
                klingonQuadrants[i][1] = 0;
                klingonQuadrants[i][2] = 0;
            }
        } else {
            throw new IllegalArgumentException("ENTERPRISE HAS NAVIGATED OUT OF OUR KNOWN UNIVERSE AND CAN NO LONGER " +
                                               "BE FOUND. THE FATE OF THE FEDERATION IS DOOMED!");
        }

        for (int i = 1; i < 3; i++) {
            klingonQuadrants[i][3] = 0;
        }

        // position enterprise in quadrant
        insertMarker(MARKER_ENTERPRISE, enterprise.getSector());

        // position klingons
        if (klingons >= 1) {
            for (int i = 1; i <= klingons; i++) {
                final int[] emptyCoordinate = findEmptyPlaceInQuadrant(quadrantMap);
                insertMarker(MARKER_KLINGON, emptyCoordinate);
                klingonQuadrants[i][1] = emptyCoordinate[0];
                klingonQuadrants[i][2] = emptyCoordinate[1];
                klingonQuadrants[i][3] = (int) Math.round(AVG_KLINGON_SHIELD_ENERGY * (0.5 + util.random()));
            }
        }

        // position bases
        if (starbases >= 1) {
            final int[] emptyCoordinate = findEmptyPlaceInQuadrant(quadrantMap);
            starbaseX = emptyCoordinate[0];
            starbaseY = emptyCoordinate[1];
            insertMarker(MARKER_STARBASE, emptyCoordinate);
        }

        // position stars - not really testable
        for (int i = 1; i <= stars; i++) {
            final int[] emptyCoordinate = findEmptyPlaceInQuadrant(quadrantMap);
            insertMarker(MARKER_STAR, emptyCoordinate);
        }
    }

    public void klingonsMoveAndFire(GameCallback callback) {
        for (int i = 1; i <= klingons; i++) {
            if (klingonQuadrants[i][3] == 0) {
                continue;
            }
            int[] newKlingonQuadrants = new int[2];
            newKlingonQuadrants[0] = klingonQuadrants[i][1];
            newKlingonQuadrants[1] = klingonQuadrants[i][2];

            insertMarker(MARKER_EMPTY, newKlingonQuadrants);
            final int[] newCoords = findEmptyPlaceInQuadrant(quadrantMap);
            klingonQuadrants[i][1] = newCoords[0];
            klingonQuadrants[i][2] = newCoords[1];
            newKlingonQuadrants[0] = klingonQuadrants[i][1];
            newKlingonQuadrants[1] = klingonQuadrants[i][2];

            insertMarker(MARKER_KLINGON, newKlingonQuadrants);
        }

        boolean action = klingonsShoot(callback);
        if (action) {
            util.println("KLINGONS ATTACKED!");
        } else {
            util.println("NO ATTACK DAMAGE!");
        }
    }

    boolean klingonsShoot(GameCallback callback) {
        if (klingons <= 0) {
            return false; // no klingons
        }

        if (enterprise.isDocked()) {
            util.println("STARBASE SHIELDS PROTECT THE ENTERPRISE");
            return false;
        }

        for (int i = 1; i <= 3; i++) {
            if (klingonQuadrants[i][3] <= 0) {
                continue;
            }

            int hits = util.toInt((klingonQuadrants[i][3] / fnd(1)) * (2 + util.random()));
            enterprise.sufferHitPoints(hits);
            klingonQuadrants[i][3] = util.toInt(klingonQuadrants[i][3] / (3 + util.random()));

            util.println(hits +
                         " UNIT HIT ON ENTERPRISE FROM SECTOR " +
                         klingonQuadrants[i][1] +
                         "," +
                         klingonQuadrants[i][2]);

            if (enterprise.getShields() <= 0) {
                callback.endGameFail(true);
                break; // we lost, game ends
            }

            util.println("      <SHIELDS DOWN TO " + enterprise.getShields() + " UNITS>");

            if (hits < 20) {
                continue;
            }

            if ((util.random() > .6) || (hits / enterprise.getShields() <= .02)) {
                continue;
            }

            int randomDevice = util.fnr();

            enterprise.setDeviceStatus(randomDevice, enterprise.getDeviceStatus()[randomDevice] -
                                                     hits / enterprise.getShields() -
                                                     .5 * util.random());
            util.println("DAMAGE CONTROL REPORTS " + Enterprise.printDeviceName(randomDevice) + " DAMAGED BY THE HIT'");
        }
        return true;
    }

    public void moveEnterprise(final float course, final float warp, final int n, final double stardate,
                               final double initialStardate, final int missionDuration, final GameCallback callback) {
        int[] sectorsXY = enterprise.getSector();
        insertMarker(MARKER_EMPTY, sectorsXY);

        final int[] sector = enterprise.moveShip(course, n, quadrantMap, stardate, initialStardate, missionDuration,
                                                 callback);

        insertMarker(MARKER_ENTERPRISE, sector);
        enterprise.maneuverEnergySR(n);
        double stardateDelta = 1;

        if (warp < 1) {
            stardateDelta = .1 * util.toInt(10 * warp);
        }
        callback.incrementStardate(stardateDelta);

        if (stardate > initialStardate + missionDuration) {
            callback.endGameFail(false);
        }
    }

    void shortRangeSensorScan(final double stardate) {
        final int sectorX = enterprise.getSector()[Enterprise.COORD_X];
        final int sectorY = enterprise.getSector()[Enterprise.COORD_Y];
        boolean docked = false;
        String shipCondition; // ship condition (docked, red, yellow, green)

        for (int i = sectorX - 1; i <= sectorX + 1; i++) {
            for (int j = sectorY - 1; j <= sectorY + 1; j++) {
                if ((util.toInt(i) >= 1) && (util.toInt(i) <= 8) && (util.toInt(j) >= 1) && (util.toInt(j) <= 8)) {
                    if (compareMarker(quadrantMap, MARKER_STARBASE, i, j)) {
                        docked = true;
                    }
                }
            }
        }

        if (!docked) {
            enterprise.setDocked(false);
            if (klingons > 0) {
                shipCondition = "*RED*";
            } else {
                shipCondition = "GREEN";
                if (enterprise.getEnergy() < enterprise.getInitialEnergy() * .1) {
                    shipCondition = "YELLOW";
                }
            }
        } else {
            enterprise.setDocked(true);
            shipCondition = "DOCKED";
            enterprise.replenishSupplies();
            util.println("SHIELDS DROPPED FOR DOCKING PURPOSES");
            enterprise.dropShields();
        }

        if (enterprise.getDeviceStatus()[Enterprise.DEVICE_SHORT_RANGE_SENSORS] < 0) { // are short range sensors out?
            util.println("\n*** SHORT RANGE SENSORS ARE OUT ***\n");
            return;
        }

        final String row = "---------------------------------";
        util.println(row);

        for (int i = 1; i <= 8; i++) {
            String sectorMapRow = "";
            for (int j = (i - 1) * 24 + 1; j <= (i - 1) * 24 + 22; j += 3) {
                sectorMapRow += " " + util.midStr(quadrantMap, j, 3);
            }
            switch (i) {
                case 1:
                    util.println(sectorMapRow + "        STARDATE           " + util.toInt(stardate * 10) * .1);
                    break;
                case 2:
                    util.println(sectorMapRow + "        CONDITION          " + shipCondition);
                    break;
                case 3:
                    util.println(sectorMapRow +
                                 "        QUADRANT           " +
                                 enterprise.getQuadrant()[Enterprise.COORD_X] +
                                 "," +
                                 enterprise.getQuadrant()[Enterprise.COORD_Y]);
                    break;
                case 4:
                    util.println(sectorMapRow + "        SECTOR             " + sectorX + "," + sectorY);
                    break;
                case 5:
                    util.println(sectorMapRow + "        PHOTON TORPEDOES   " + util.toInt(enterprise.getTorpedoes()));
                    break;
                case 6:
                    util.println(
                            sectorMapRow + "        TOTAL ENERGY       " + util.toInt(enterprise.getTotalEnergy()));
                    break;
                case 7:
                    util.println(sectorMapRow + "        SHIELDS            " + util.toInt(enterprise.getShields()));
                    break;
                case 8:
                    util.println(sectorMapRow + "        KLINGONS REMAINING " + util.toInt(klingonsInGalaxy));
            }
        }

        util.println(row);
    }

    void longRangeSensorScan() {
        final int quadrantX = enterprise.getQuadrant()[Enterprise.COORD_X];
        final int quadrantY = enterprise.getQuadrant()[Enterprise.COORD_Y];

        if (enterprise.getDeviceStatus()[Enterprise.DEVICE_LONG_RANGE_SENSORS] < 0) {
            util.println("LONG RANGE SENSORS ARE INOPERABLE");
            return;
        }

        util.println("LONG RANGE SCAN FOR QUADRANT " + quadrantX + "," + quadrantY);
        final String rowStr = "-------------------";
        util.println(rowStr);
        final int[] n = new int[4];

        for (int i = quadrantX - 1; i <= quadrantX + 1; i++) {
            n[1] = -1;
            n[2] = -2;
            n[3] = -3;
            for (int j = quadrantY - 1; j <= quadrantY + 1; j++) {
                if (i > 0 && i < 9 && j > 0 && j < 9) {
                    n[j - quadrantY + 2] = galaxy[i][j];
                    chartedGalaxy[i][j] = galaxy[i][j];
                }
            }
            for (int l = 1; l <= 3; l++) {
                util.print(": ");
                if (n[l] < 0) {
                    util.print("*** ");
                    continue;
                }
                util.print(util.rightStr(Integer.toString(n[l] + 1000), 3) + " ");
            }
            util.println(": \n" + rowStr);
            util.println("SCAN COMPLETE!");
        }
    }

    void firePhasers(GameCallback callback) {
        final double[] deviceStatus = enterprise.getDeviceStatus();
        final int quadrantX = enterprise.getQuadrant()[Enterprise.COORD_X];
        final int quadrantY = enterprise.getQuadrant()[Enterprise.COORD_Y];

        if (deviceStatus[Enterprise.DEVICE_PHASER_CONTROL] < 0) {
            util.println("PHASERS INOPERATIVE");
            return;
        }

        if (klingons <= 0) {
            printNoEnemyShipsMessage();
            return;
        }

        if (deviceStatus[Enterprise.DEVICE_LIBRARY_COMPUTER] < 0) {
            util.println("COMPUTER FAILURE HAMPERS ACCURACY");
        }
        util.println("PHASERS LOCKED ON TARGET;  ");
        int nrUnitsToFire;

        while (true) {
            util.println("ENERGY AVAILABLE = " + enterprise.getEnergy() + " UNITS");
            nrUnitsToFire = util.toInt(util.inputFloat("NUMBER OF UNITS TO FIRE"));
            if (nrUnitsToFire <= 0) {
                return;
            }
            if (enterprise.getEnergy() - nrUnitsToFire >= 0) {
                break;
            }
        }
        enterprise.decreaseEnergy(nrUnitsToFire);

        if (deviceStatus[Enterprise.DEVICE_SHIELD_CONTROL] < 0) {
            nrUnitsToFire = util.toInt(nrUnitsToFire * util.random());
        }
        int h1 = util.toInt(nrUnitsToFire / klingons);

        for (int i = 1; i <= 3; i++) {
            if (klingonQuadrants[i][3] <= 0) {
                break;
            }
            int hitPoints = util.toInt((h1 / fnd(0)) * (util.random() + 2));

            if (hitPoints <= .15 * klingonQuadrants[i][3]) {
                util.println(
                        "SENSORS SHOW NO DAMAGE TO ENEMY AT " + klingonQuadrants[i][1] + "," + klingonQuadrants[i][2]);
                continue;
            }
            klingonQuadrants[i][3] = klingonQuadrants[i][3] - hitPoints;
            util.println(hitPoints +
                         " UNIT HIT ON KLINGON AT SECTOR " +
                         klingonQuadrants[i][1] +
                         "," +
                         klingonQuadrants[i][2]);

            if (klingonQuadrants[i][3] <= 0) {
                util.println("*** KLINGON DESTROYED ***");
                klingons -= 1;
                klingonsInGalaxy -= 1;

                int[] newKlingonQuadrants = new int[2];
                newKlingonQuadrants[0] = klingonQuadrants[i][1];
                newKlingonQuadrants[1] = klingonQuadrants[i][2];

                insertMarker(MARKER_EMPTY, newKlingonQuadrants);
                klingonQuadrants[i][3] = 0;
                galaxy[quadrantX][quadrantY] -= 100;
                chartedGalaxy[quadrantX][quadrantY] = galaxy[quadrantX][quadrantY];
                if (klingonsInGalaxy <= 0) {
                    callback.endGameSuccess();
                }
            } else {
                util.println("   (SENSORS SHOW " + klingonQuadrants[i][3] + " UNITS REMAINING)");
            }
        }
        klingonsShoot(callback);
    }

    void firePhotonTorpedo(final double stardate, final double initialStardate, final double missionDuration,
                           GameCallback callback) {
        if (enterprise.getTorpedoes() <= 0) {
            util.println("ALL PHOTON TORPEDOES EXPENDED");
            return;
        }

        if (enterprise.getDeviceStatus()[Enterprise.DEVICE_PHOTON_TUBES] < 0) {
            util.println("PHOTON TUBES ARE NOT OPERATIONAL");
        }

        float c1 = util.inputFloat("PHOTON TORPEDO COURSE (1-9)");

        if (c1 == 9) {
            c1 = 1;
        }
        if (c1 < 1 && c1 >= 9) {
            util.println("ENSIGN CHEKOV REPORTS,  'INCORRECT COURSE DATA, SIR!'");
            return;
        }

        int ic1 = util.toInt(c1);
        final int[][] cardinalDirections = enterprise.getCardinalDirections();

        float x1 = cardinalDirections[ic1][1] +
                   (cardinalDirections[ic1 + 1][1] - cardinalDirections[ic1][1]) * (c1 - ic1);

        enterprise.decreaseEnergy(2);
        enterprise.decreaseTorpedoes(1);

        float x2 = cardinalDirections[ic1][2] +
                   (cardinalDirections[ic1 + 1][2] - cardinalDirections[ic1][2]) * (c1 - ic1);
        float x = enterprise.getSector()[Enterprise.COORD_X];
        float y = enterprise.getSector()[Enterprise.COORD_Y];

        util.println("TORPEDO TRACK:");

        while (true) {
            x = x + x1;
            y = y + x2;
            int x3 = Math.round(x);
            int y3 = Math.round(y);

            if (x3 < 1 || x3 > 8 || y3 < 1 || y3 > 8) {
                util.println("TORPEDO MISSED"); // 5490
                klingonsShoot(callback);
                return;
            }
            util.println("               " + x3 + "," + y3);

            if (compareMarker(quadrantMap, MARKER_EMPTY, util.toInt(x), util.toInt(y))) {
                continue;
            } else if (compareMarker(quadrantMap, MARKER_KLINGON, util.toInt(x), util.toInt(y))) {
                util.println("*** KLINGON DESTROYED ***");
                klingons = klingons - 1;
                klingonsInGalaxy = klingonsInGalaxy - 1;

                if (klingonsInGalaxy <= 0) {
                    callback.endGameSuccess();
                }

                for (int i = 1; i <= 3; i++) {
                    if (x3 == klingonQuadrants[i][1] && y3 == klingonQuadrants[i][2]) {
                        break;
                    }
                }
                int i = 3;
                klingonQuadrants[i][3] = 0;
            } else if (compareMarker(quadrantMap, MARKER_STAR, util.toInt(x), util.toInt(y))) {
                util.println("STAR AT " + x3 + "," + y3 + " ABSORBED TORPEDO ENERGY.");
                klingonsShoot(callback);

                return;
            } else if (compareMarker(quadrantMap, MARKER_STARBASE, util.toInt(x), util.toInt(y))) {
                util.println("*** STARBASE DESTROYED ***");
                starbases = starbases - 1;
                basesInGalaxy = basesInGalaxy - 1;

                if (basesInGalaxy == 0 && klingonsInGalaxy <= stardate - initialStardate - missionDuration) {
                    util.println("THAT DOES IT, CAPTAIN!!  YOU ARE HEREBY RELIEVED OF COMMAND");
                    util.println("AND SENTENCED TO 99 STARDATES AT HARD LABOR ON CYGNUS 12!!");
                    callback.endGameFail(false);
                } else {
                    util.println("STARFLEET COMMAND REVIEWING YOUR RECORD TO CONSIDER");
                    util.println("COURT MARTIAL!");
                    enterprise.setDocked(false);
                }
            }

            int[] XY = new int[2];
            XY[0] = util.toInt(x);
            XY[1] = util.toInt(y);

            insertMarker(MARKER_EMPTY, XY);
            int[] quadrantXY = new int[2];
            quadrantXY = enterprise.getQuadrant();
            final int quadrantX = quadrantXY[0];
            final int quadrantY = quadrantXY[1];

            galaxy[quadrantX][quadrantY] = klingons * 100 + starbases * 10 + stars;
            chartedGalaxy[quadrantX][quadrantY] = galaxy[quadrantX][quadrantY];
            klingonsShoot(callback);
        }
    }

    public void cumulativeGalacticRecord(final boolean cumulativeReport) {
        final int quadrantX = enterprise.getQuadrant()[Enterprise.COORD_X];
        final int quadrantY = enterprise.getQuadrant()[Enterprise.COORD_Y];

        if (cumulativeReport) {
            util.println("");
            util.println("        ");
            util.println("COMPUTER RECORD OF GALAXY FOR QUADRANT " + quadrantX + "," + quadrantY);
            util.println("");
        } else {
            util.println("                        THE GALAXY");
        }

        util.println("       1     2     3     4     5     6     7     8");
        final String rowDivider = "     ----- ----- ----- ----- ----- ----- ----- -----";
        util.println(rowDivider);

        for (int i = 1; i <= 8; i++) {
            util.print(i + "  ");
            if (cumulativeReport) {
                int y = 1;
                String quadrantName = getQuadrantName(false, i, y);
                int tabLen = util.toInt(15 - .5 * util.strLen(quadrantName));
                util.println(util.tab(tabLen) + quadrantName);
                y = 5;
                quadrantName = getQuadrantName(false, i, y);
                tabLen = util.toInt(39 - .5 * util.strLen(quadrantName));
                util.println(util.tab(tabLen) + quadrantName);
            } else {
                for (int j = 1; j <= 8; j++) {
                    util.print("   ");
                    if (chartedGalaxy[i][j] == 0) {
                        util.print("***");
                    } else {
                        util.print(util.rightStr(Integer.toString(chartedGalaxy[i][j] + 1000), 3));
                    }
                }
            }
            util.println("");
            util.println(rowDivider);
        }

        util.println("");
    }

    public void photonTorpedoData() {
        int sectorX = enterprise.getSector()[Enterprise.COORD_X];
        int sectorY = enterprise.getSector()[Enterprise.COORD_Y];

        if (klingons <= 0) {
            printNoEnemyShipsMessage();
            return;
        }

        util.println("FROM ENTERPRISE TO KLINGON BATTLE CRUISER" + ((klingons > 1) ? "S" : ""));

        for (int i = 1; i <= 3; i++) {
            if (klingonQuadrants[i][3] > 0) {
                printDirection(sectorX, sectorY, klingonQuadrants[i][1], klingonQuadrants[i][2]);
            }
        }
    }

    void directionDistanceCalculator() {
        int quadrantX = enterprise.getQuadrant()[Enterprise.COORD_X];
        int quadrantY = enterprise.getQuadrant()[Enterprise.COORD_Y];
        int sectorX = enterprise.getSector()[Enterprise.COORD_X];
        int sectorY = enterprise.getSector()[Enterprise.COORD_Y];

        util.println("DIRECTION/DISTANCE CALCULATOR:");
        util.println("YOU ARE AT QUADRANT " + quadrantX + "," + quadrantY + " SECTOR " + sectorX + "," + sectorY);
        util.print("PLEASE ENTER ");

        int[] initialCoords = util.inputCoords("  INITIAL COORDINATES (X,Y)");
        int[] finalCoords = util.inputCoords("  FINAL COORDINATES (X,Y)");

        printDirection(initialCoords[0], initialCoords[1], finalCoords[0], finalCoords[1]);
    }

    void printDirection(int from_x, int from_y, int to_x, int to_y) {
        to_y = to_y - from_y;  // delta 2
        from_y = from_x - to_x;    // delta 1

        if (to_y > 0) {
            if (from_y < 0) {
                from_x = 7;
            } else {
                from_x = 1;
                int tempA = from_y;
                from_y = to_y;
                to_y = tempA;
            }
        } else {
            if (from_y > 0) {
                from_x = 3;
            } else {
                from_x = 5;
                int tempA = from_y;
                from_y = to_y;
                to_y = tempA;
            }
        }

        from_y = Math.abs(from_y);
        to_y = Math.abs(to_y);

        if (from_y > 0 || to_y > 0) {
            if (from_y >= to_y) {
                util.println("DIRECTION = " + (from_x + to_y / from_y));
            } else {
                util.println("DIRECTION = " + (from_x + 2 - to_y / from_y));
            }
        }
        util.println("DISTANCE = " + util.round(Math.sqrt(to_y ^ 2 + from_y ^ 2), 6));
    }

    void starbaseNavData() {
        int sectorX = enterprise.getSector()[Enterprise.COORD_X];
        int sectorY = enterprise.getSector()[Enterprise.COORD_Y];

        if (starbases != 0) {
            util.println("FROM ENTERPRISE TO STARBASE:");
            printDirection(sectorX, sectorY, starbaseX, starbaseY);
        } else {
            util.println("MR. SPOCK REPORTS,  'SENSORS SHOW NO STARBASES IN THIS");
            util.println(" QUADRANT.'");
        }
    }

    void printNoEnemyShipsMessage() {
        util.println("SCIENCE OFFICER SPOCK REPORTS  'SENSORS SHOW NO ENEMY SHIPS");
        util.println("                                IN THIS QUADRANT'");
    }

    String getRegionName(final boolean regionNameOnly, final int y) {
        if (!regionNameOnly) {
            switch (y % 4) {
                case 0:
                    return " I";
                case 1:
                    return " II";
                case 2:
                    return " III";
                case 3:
                    return " IV";
            }
        }
        return "";
    }

    String getQuadrantName(final boolean regionNameOnly, final int x, final int y) {
        if (y <= 4) {
            switch (x) {
                case 1:
                    return "ANTARES" + getRegionName(regionNameOnly, y);
                case 2:
                    return "RIGEL" + getRegionName(regionNameOnly, y);
                case 3:
                    return "PROCYON" + getRegionName(regionNameOnly, y);
                case 4:
                    return "VEGA" + getRegionName(regionNameOnly, y);
                case 5:
                    return "CANOPUS" + getRegionName(regionNameOnly, y);
                case 6:
                    return "ALTAIR" + getRegionName(regionNameOnly, y);
                case 7:
                    return "SAGITTARIUS" + getRegionName(regionNameOnly, y);
                case 8:
                    return "POLLUX" + getRegionName(regionNameOnly, y);
            }
        } else {
            switch (x) {
                case 1:
                    return "SIRIUS" + getRegionName(regionNameOnly, y);
                case 2:
                    return "DENEB" + getRegionName(regionNameOnly, y);
                case 3:
                    return "CAPELLA" + getRegionName(regionNameOnly, y);
                case 4:
                    return "BETELGEUSE" + getRegionName(regionNameOnly, y);
                case 5:
                    return "ALDEBARAN" + getRegionName(regionNameOnly, y);
                case 6:
                    return "REGULUS" + getRegionName(regionNameOnly, y);
                case 7:
                    return "ARCTURUS" + getRegionName(regionNameOnly, y);
                case 8:
                    return "SPICA" + getRegionName(regionNameOnly, y);
            }
        }
        return "UNKNOWN - ERROR";
    }

    void insertMarker(final String marker, final int[] xy) {
        int x = util.toInt(xy[0]);
        int y = util.toInt(xy[1]);
        final int pos = y * 3 + x * 24 + 1;

        if (marker.length() != 3) {
            util.println("ERROR");
            //System.err.println("ERROR");
            //System.exit(-1);
        }

        if (pos == 1) {
            quadrantMap = marker + util.rightStr(quadrantMap, 189);
        }

        if (pos == 190) {
            quadrantMap = util.leftStr(quadrantMap, 189) + marker;
        }

        quadrantMap = util.leftStr(quadrantMap, (pos - 1)) + marker + util.rightStr(quadrantMap, (190 - pos));
    }

    /**
     * Finds random empty coordinates in a quadrant.
     *
     * @param quadrantMap
     * @return an array with a pair of coordinates x, y
     */
    int[] findEmptyPlaceInQuadrant(final String quadrantMap) {
        final int x = util.fnr();
        final int y = util.fnr();

        if (!compareMarker(quadrantMap, MARKER_EMPTY, x, y)) {
            return findEmptyPlaceInQuadrant(quadrantMap);
        }

        return new int[]{x, y};
    }

    boolean compareMarker(final String quadrantMap, final String marker, final int x, final int y) {
        final int markerRegion = (y - 1) * 3 + (x - 1) * 24 + 1;

        if (util.midStr(quadrantMap, markerRegion, 3).equals(marker)) {
            return true;
        }

        return false;
    }

}
