package sstproject;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.Locale;
import java.util.Random;
import java.util.stream.IntStream;

/**
 * SUPER STARTREK - MAY 16,1978
 * ****        **** STAR TREK ****        ****
 * **** SIMULATION OF A MISSION OF THE STARSHIP ENTERPRISE,
 * **** AS SEEN ON THE STAR TREK TV SHOW.
 * **** ORIGINAL PROGRAM BY MIKE MAYFIELD, MODIFIED VERSION
 * **** PUBLISHED IN DEC'S "101 BASIC GAMES", BY DAVE AHL.
 * **** MODIFICATIONS TO THE LATTER (PLUS DEBUGGING) BY BOB
 * *** LEEDOM - APRIL & DECEMBER 1974,
 * *** WITH A LITTLE HELP FROM HIS FRIENDS . . .
 * <p>
 * Ported to Java in Jan-Mar 2022 by
 * Taciano Dreckmann Perez (taciano.perez@gmail.com)
 */
public class SuperStarTrekGame implements GameCallback {

    public Util util;

    // commands
    static final int COMMAND_NAV = 1;
    static final int COMMAND_SRS = 2;
    static final int COMMAND_LRS = 3;
    static final int COMMAND_PHA = 4;
    static final int COMMAND_TOR = 5;
    static final int COMMAND_SHE = 6;
    static final int COMMAND_DAM = 7;
    static final int COMMAND_COM = 8;
    static final int COMMAND_XXX = 9;

    // computer commands
    static final int COMPUTER_COMMAND_CUMULATIVE_GALACTIC_RECORD = 1;
    static final int COMPUTER_COMMAND_STATUS_REPORT = 2;
    static final int COMPUTER_COMMAND_PHOTON_TORPEDO_DATA = 3;
    static final int COMPUTER_COMMAND_STARBASE_NAV_DATA = 4;
    static final int COMPUTER_COMMAND_DIR_DIST_CALC = 5;
    static final int COMPUTER_COMMAND_GALAXY_MAP = 6;

    // other constants
    static final String COMMANDS = "NAVSRSLRSPHATORSHEDAMCOMXXX";

    // game state
    final GalaxyMap galaxyMap;

    double stardate;
    int missionDuration;    // T9 (mission duration in stardates)
    boolean restart = false;

    // initial values
    final double initialStardate;

    public static void main(String[] args) {
        Util util = new Util(new Random(), new BufferedReader(new InputStreamReader(System.in)));
        Enterprise enterprise = new Enterprise(util);

        final SuperStarTrekGame game = new SuperStarTrekGame(util, enterprise);
        printBanner(util);
        while (true) {
            game.orders();
            game.enterNewQuadrant();
            game.restart = false;
            game.commandLoop();
        }
    }

    public SuperStarTrekGame(Util util, Enterprise enterprise) {
        this.util = util;
        this.galaxyMap = new GalaxyMap(util, enterprise);
        this.missionDuration = Math.max((25 + util.toInt(util.random() * 10)), galaxyMap.getKlingonsInGalaxy() + 1);
        this.stardate = util.toInt(util.random() * 20 + 20);
        this.initialStardate = stardate;
    }

    static void printBanner(Util util) {
        IntStream.range(1, 10).forEach(i -> {
            util.println("");
        });
        util.println(
                "                ,------*------,\n" +
                "-------------   '---  ------'\n" +
                " '-------- --'      / /\n" +
                "     ,---' '-------/ /--,\n" +
                "     '----------------'\n" +
                "\n" +
                "THE USS ENTERPRISE --- NCC-1701\n" +
                "\n"
        );
    }

    void orders() {
        util.println("YOUR ORDERS ARE AS FOLLOWS:\n" +
                     "     DESTROY THE " +
                     galaxyMap.getKlingonsInGalaxy() +
                     " KLINGON WARSHIP" +
                     ((galaxyMap.getKlingonsInGalaxy() == 1) ? "" : "S") +
                     " WHICH HAVE INVADED\n" +
                     "   THE GALAXY BEFORE THEY CAN ATTACK FEDERATION HEADQUARTERS\n" +
                     "   ON STARDATE " +
                     initialStardate +
                     missionDuration +
                     "  THIS GIVES YOU " +
                     missionDuration +
                     " DAYS.  THERE " +
                     ((galaxyMap.getBasesInGalaxy() == 1) ? "IS" : "ARE") +
                     "\n" +
                     "  " +
                     galaxyMap.getBasesInGalaxy() +
                     " STARBASE" +
                     ((galaxyMap.getBasesInGalaxy() == 1) ? "" : "S") +
                     " IN THE GALAXY FOR RESUPPLYING YOUR SHIP");
    }

    void commandLoop() {
        while (!this.restart) {
            checkShipEnergy();
            String cmdStr = "";
            while ("".equals(cmdStr)) {
                cmdStr = util.inputStr("COMMAND");
            }
            boolean foundCommand = false;

            for (int i = 1; i <= 9; i++) {
                if (util.leftStr(cmdStr.toUpperCase(Locale.ROOT), 3).equals(util.midStr(COMMANDS, 3 * i - 2, 3))) {
                    switch (i) {
                        case COMMAND_NAV -> {
                            navigation();
                            foundCommand = true;
                        }
                        case COMMAND_SRS -> {
                            shortRangeSensorScan();
                            foundCommand = true;
                        }
                        case COMMAND_LRS -> {
                            longRangeSensorScan();
                            foundCommand = true;
                        }
                        case COMMAND_PHA -> {
                            firePhasers();
                            foundCommand = true;
                        }
                        case COMMAND_TOR -> {
                            firePhotonTorpedo();
                            foundCommand = true;
                        }
                        case COMMAND_SHE -> {
                            shieldControl();
                            foundCommand = true;
                        }
                        case COMMAND_DAM -> {
                            galaxyMap.getEnterprise().damageControl(this);
                            foundCommand = true;
                        }
                        case COMMAND_COM -> {
                            libraryComputer();
                            foundCommand = true;
                        }
                        case COMMAND_XXX -> {
                            endGameFail(false);
                            foundCommand = true;
                        }
                        default -> {
                            printCommandOptions();
                            foundCommand = true;
                        }
                    }
                }
            }
            if (!foundCommand) {
                printCommandOptions();
            }
        }
    }

    void checkShipEnergy() {
        final Enterprise enterprise = galaxyMap.getEnterprise();
        if (enterprise.getTotalEnergy() < 10 &&
            (enterprise.getEnergy() <= 10 || enterprise.getDeviceStatus()[Enterprise.DEVICE_SHIELD_CONTROL] != 0)) {
            util.println("\n** FATAL ERROR **   YOU'VE JUST STRANDED YOUR SHIP IN ");
            util.println("SPACE");
            util.println("YOU HAVE INSUFFICIENT MANEUVERING ENERGY,");
            util.println(" AND SHIELD CONTROL");
            util.println("IS PRESENTLY INCAPABLE OF CROSS");
            util.println("-CIRCUITING TO ENGINE ROOM!!");
            endGameFail(false);
        }
    }

    void printCommandOptions() {
        util.println("ENTER ONE OF THE FOLLOWING:");
        util.println("  NAV  (TO SET COURSE)");
        util.println("  SRS  (FOR SHORT RANGE SENSOR SCAN)");
        util.println("  LRS  (FOR LONG RANGE SENSOR SCAN)");
        util.println("  PHA  (TO FIRE PHASERS)");
        util.println("  TOR  (TO FIRE PHOTON TORPEDOES)");
        util.println("  SHE  (TO RAISE OR LOWER SHIELDS)");
        util.println("  DAM  (FOR DAMAGE CONTROL REPORTS)");
        util.println("  COM  (TO CALL ON LIBRARY-COMPUTER)");
        util.println("  XXX  (TO RESIGN YOUR COMMAND)\n");
    }

    void navigation() {
        float course = util.toInt(util.inputFloat("COURSE (0-9)"));

        if (course == 9) {
            course = 1;
        }

        if (course < 1 || course >= 9) {
            util.println("   LT. SULU REPORTS, 'INCORRECT COURSE DATA, SIR!'");
            return;
        }

        final Enterprise enterprise = galaxyMap.getEnterprise();
        final double[] deviceStatus = enterprise.getDeviceStatus();
        util.println("WARP FACTOR (0-" + ((deviceStatus[Enterprise.DEVICE_WARP_ENGINES] < 0) ? "0.2" : "8") + ")");
        float warp = util.inputFloat("");

        if (deviceStatus[Enterprise.DEVICE_WARP_ENGINES] < 0 && warp > .2) {
            util.println("WARP ENGINES ARE DAMAGED.  MAXIMUM SPEED = WARP 0.2");
            return;
        }

        if (warp == 0) {
            return;
        }

        if (warp > 0 && warp <= 8) {
            int n = util.toInt(warp * 8);
            if (enterprise.getEnergy() - n >= 0) {
                galaxyMap.klingonsMoveAndFire(this);
                repairDamagedDevices(course, warp, n);
                galaxyMap.moveEnterprise(course, warp, n, stardate, initialStardate, missionDuration, this);
            } else {
                util.println("ENGINEERING REPORTS   'INSUFFICIENT ENERGY AVAILABLE");
                util.println("                       FOR MANEUVERING AT WARP " + warp + "!'");
                if (enterprise.getShields() < n - enterprise.getEnergy() ||
                    deviceStatus[Enterprise.DEVICE_SHIELD_CONTROL] < 0) {
                    return;
                }
                util.println("DEFLECTOR CONTROL ROOM ACKNOWLEDGES " + enterprise.getShields() + " UNITS OF ENERGY");
                util.println("                         PRESENTLY DEPLOYED TO SHIELDS.");
            }
        } else {
            util.println("   CHIEF ENGINEER SCOTT REPORTS 'THE ENGINES WON'T TAKE");
            util.println(" WARP " + warp + "!'");
        }
    }

    void repairDamagedDevices(final float course, final float warp, final int N) {
        final Enterprise enterprise = galaxyMap.getEnterprise();

        // repair damaged devices and print damage report
        enterprise.repairDamagedDevices(warp);

        if (util.random() > .2) {
            return;  // 80% chance no damage nor repair
        }

        int randomDevice = util.fnr();    // random device
        final double[] deviceStatus = enterprise.getDeviceStatus();

        if (util.random() >= .6) {   // 40% chance of repair of random device
            enterprise.setDeviceStatus(randomDevice, deviceStatus[randomDevice] + util.random() * 3 + 1);
            util.println("DAMAGE CONTROL REPORT:  " +
                         Enterprise.printDeviceName(randomDevice) +
                         " STATE OF REPAIR IMPROVED\n");
        } else {            // 60% chance of damage of random device
            enterprise.setDeviceStatus(randomDevice, deviceStatus[randomDevice] - (util.random() * 5 + 1));
            util.println("DAMAGE CONTROL REPORT:  " + Enterprise.printDeviceName(randomDevice) + " DAMAGED");
        }
    }

    void longRangeSensorScan() {
        // LONG RANGE SENSOR SCAN CODE
        galaxyMap.longRangeSensorScan();
    }

    void firePhasers() {
        galaxyMap.firePhasers(this);
    }

    void firePhotonTorpedo() {
        galaxyMap.firePhotonTorpedo(stardate, initialStardate, missionDuration, this);
    }

    void shieldControl() {
        galaxyMap.getEnterprise().shieldControl();
    }

    void shortRangeSensorScan() {
        // SHORT RANGE SENSOR SCAN & STARTUP SUBROUTINE
        galaxyMap.shortRangeSensorScan(stardate);
    }

    void libraryComputer() {
        // REM LIBRARY COMPUTER CODE
        if (galaxyMap.getEnterprise().getDeviceStatus()[Enterprise.DEVICE_LIBRARY_COMPUTER] < 0) {
            util.println("COMPUTER DISABLED");
            return;
        }
        while (true) {
            final float commandInput = util.inputFloat("COMPUTER ACTIVE AND AWAITING COMMAND");
            if (commandInput < 0) {
                return;
            }
            util.println("");
            int command = util.toInt(commandInput) + 1;
            if (command >= COMPUTER_COMMAND_CUMULATIVE_GALACTIC_RECORD && command <= COMPUTER_COMMAND_GALAXY_MAP) {
                switch (command) {
                    case COMPUTER_COMMAND_CUMULATIVE_GALACTIC_RECORD:
                        galaxyMap.cumulativeGalacticRecord(true);
                        return;
                    case COMPUTER_COMMAND_STATUS_REPORT:
                        statusReport();
                        return;
                    case COMPUTER_COMMAND_PHOTON_TORPEDO_DATA:
                        galaxyMap.photonTorpedoData();
                        return;
                    case COMPUTER_COMMAND_STARBASE_NAV_DATA:
                        galaxyMap.starbaseNavData();
                        return;
                    case COMPUTER_COMMAND_DIR_DIST_CALC:
                        galaxyMap.directionDistanceCalculator();
                        return;
                    case COMPUTER_COMMAND_GALAXY_MAP:
                        galaxyMap.cumulativeGalacticRecord(false);
                        return;
                }
            } else {
                // invalid command
                util.println("FUNCTIONS AVAILABLE FROM LIBRARY-COMPUTER:");
                util.println("   0 = CUMULATIVE GALACTIC RECORD");
                util.println("   1 = STATUS REPORT");
                util.println("   2 = PHOTON TORPEDO DATA");
                util.println("   3 = STARBASE NAV DATA");
                util.println("   4 = DIRECTION/DISTANCE CALCULATOR");
                util.println("   5 = GALAXY 'REGION NAME' MAP");
                util.println("");
            }
        }
    }

    void statusReport() {
        util.println("   STATUS REPORT:");
        util.println("KLINGON" + ((galaxyMap.getKlingonsInGalaxy() > 1) ? "S" : "") +
                     " LEFT: " +
                     galaxyMap.getKlingonsInGalaxy());
        util.println("MISSION MUST BE COMPLETED IN " +
                     .1 * util.toInt((initialStardate + missionDuration - stardate) * 10) +
                     " STARDATES");

        if (galaxyMap.getBasesInGalaxy() >= 1) {
            util.println("THE FEDERATION IS MAINTAINING " +
                         galaxyMap.getBasesInGalaxy() +
                         " STARBASE" +
                         ((galaxyMap.getBasesInGalaxy() > 1) ? "S" : "") +
                         " IN THE GALAXY");
        } else {
            util.println("YOUR STUPIDITY HAS LEFT YOU ON YOUR OWN IN");
            util.println("  THE GALAXY -- YOU HAVE NO STARBASES LEFT!");
        }

        galaxyMap.getEnterprise().damageControl(this);
    }

    @Override
    public void enterNewQuadrant() {
        galaxyMap.newQuadrant(stardate, initialStardate);
        shortRangeSensorScan();
    }

    @Override
    public void incrementStardate(double increment) {
        this.stardate += increment;
    }

    @Override
    public void endGameFail(final boolean enterpriseDestroyed) {    // 6220
        if (enterpriseDestroyed) {
            util.println("\nTHE ENTERPRISE HAS BEEN DESTROYED.  THEN FEDERATION ");
            util.println("WILL BE CONQUERED");
        }
        util.println("\nIT IS STARDATE " + stardate);
        util.println("THERE WERE " + galaxyMap.getKlingonsInGalaxy() + " KLINGON BATTLE CRUISERS LEFT AT");
        util.println("THE END OF YOUR MISSION.");
        repeatGame();
    }

    @Override
    public void endGameSuccess() {
        util.println("CONGRATULATION, CAPTAIN!  THE LAST KLINGON BATTLE CRUISER");
        util.println("MENACING THE FEDERATION HAS BEEN DESTROYED.\n");
        util.println("YOUR EFFICIENCY RATING IS " +
                     (Math.sqrt(1000 * (galaxyMap.getRemainingKlingons() / (stardate - initialStardate)))));
        repeatGame();
    }

    void repeatGame() {
        util.println("\n");
        if (galaxyMap.getBasesInGalaxy() != 0) {
            util.println("THE FEDERATION IS IN NEED OF A NEW STARSHIP COMMANDER");
            util.println("FOR A SIMILAR MISSION -- IF THERE IS A VOLUNTEER,");
            final String reply = util.inputStr("LET HIM STEP FORWARD AND ENTER 'AYE'");
            if ("AYE".equals(reply)) {
                this.restart = true;
            } else {
                System.exit(0);
            }
        }
    }

}
