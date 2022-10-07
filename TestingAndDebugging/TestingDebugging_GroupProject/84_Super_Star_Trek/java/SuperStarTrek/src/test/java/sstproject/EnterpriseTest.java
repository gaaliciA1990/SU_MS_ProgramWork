package sstproject;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import sstproject.Enterprise;
import sstproject.GameCallback;
import sstproject.Util;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

/**
 * Author: Alicia Garcia, Mustafa Abuthuraya, Sanchita Jain
 * Version: 1.0
 * Date: 4/20/2022 12:285
 * **********************************************************************
 * **********************************************************************
 * **********************************************************************
 * ********************  READ ME!   **************************************
 * THE TESTS BELOW ARE PLACEHOLDER AND SHOULD BE USED AS A GUIDE TO IMPLEMENT
 * YOUR TESTS AND DETERMINING WHAT TO TEST. PLEASE BE SURE TO CLAIM YOUR SECTION
 * OF CODE YOU'RE TESTING AND PUSH YOUR CHANGES IMMEDIATELY AND NOTIFY THE TEAM
 * **********************************************************************
 * **********************************************************************
 * **********************************************************************
 */

class EnterpriseTest {
    Util util;

    @BeforeEach
    void enterpriseSetUp() {
        this.util = mock(Util.class);
    }

    /**
     * OWNER: ALICIA
     * Verify that the shields are not up when the enterprise is first starting
     */
    @Test
    void getShields_value_is_zero_atGame_start() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        int expectedShield = 0;

        // ACT

        // ASSERT
        assertEquals(expectedShield, enterprise.getShields());
    }

    /**
     * OWNER: ALICIA
     * Verify the shields increased when control command given to increase
     */
    @Test
    void shieldControl_user_increases_shield_by_500_returns_new_shield_value() {
        int userInput = 500;

        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        // ACT
        enterprise.shieldControl();

        // ASSERT
        assertEquals(userInput, enterprise.getShields());
    }

    /**
     * OWNER: ALICIA
     * Verify the shields do not change when control command is a negative number
     */
    @Test
    void shieldControl_user_increases_shield_by_negative_returns_unchangedMessage() {
        int userInput = -500;

        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        // ACT
        enterprise.shieldControl();

        // ASSERT
        verify(util).println("<SHIELDS UNCHANGED>");
        assertEquals(0, enterprise.getShields());
    }

    /**
     * OWNER: ALICIA
     * Verify the shields do not change when control command is a the same as the current
     * value of the shields (BAD DESIGN!!)
     */
    @Test
    void shieldControl_user_increases_shield_by_currentValueOfShield_returns_unchangedMessage() {
        int userInput = 500;

        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        // ACT
        enterprise.shieldControl();

        //repeat the user input of the same value
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        // ACT
        enterprise.shieldControl();

        // ASSERT
        verify(util).println("<SHIELDS UNCHANGED>");
        assertEquals(userInput, enterprise.getShields());
    }

    /**
     * OWNER: ALICIA
     * Verify the shields do not change when control command is greater than available energy (always 3000)
     */
    @Test
    void shieldControl_user_increases_shield_by_more_than_energy_returns_snarkyMessage_and_unchanged() {
        int userInput = 50000;

        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        // ACT
        enterprise.shieldControl();

        // ASSERT
        verify(util).println("SHIELD CONTROL REPORTS  'THIS IS NOT THE FEDERATION TREASURY.'");
        verify(util).println("<SHIELDS UNCHANGED>");
        assertNotEquals(userInput, enterprise.getShields());
    }

    /**
     * OWNER: ALICIA
     * Verify shields are dropped to after energy added when method is called.
     */
    @Test
    void dropShields_validate_shield_drops_to_zero() {
        int userInput = 500;

        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        // ACT
        enterprise.shieldControl();
        enterprise.dropShields();

        // ASSERT
        assertEquals(0, enterprise.getShields());
    }

    /**
     * OWNER: ALICIA
     * Verify the shields will decrease when the enterprise takes damage
     */
    @Test
    void sufferHitPoints_will_reduce_shields_when_damage_taken() {
        // ARRANGE
        int userShieldInput = 500;
        int shipHitDamage = 300;

        // ACT
        Enterprise enterprise = new Enterprise(util);
        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userShieldInput);
        when(util.toInt((float) userShieldInput)).thenReturn(userShieldInput);

        enterprise.shieldControl();
        enterprise.sufferHitPoints(shipHitDamage);

        // ASSERT
        assertEquals((userShieldInput - shipHitDamage), enterprise.getShields());
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void damageControl_all_system_operable_message_displayed_when_deviceDamage_equals_zero() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        enterprise.deviceStatus[Enterprise.DEVICE_DAMAGE_CONTROL] = 0;

        // ACT
        enterprise.damageControl(mock(GameCallback.class));

        // ASSERT
        verify(util).println("ALL SYSTEMS OPERABLE! NO DAMAGE TO REPORT!");
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void damageControl_verify_damage_report_messagePrinted_deviceDamage_greater_than_zero() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        enterprise.deviceStatus[Enterprise.DEVICE_DAMAGE_CONTROL] = 6;

        // ACT
        enterprise.damageControl(mock(GameCallback.class));

        // ASSERT
        verify(util).println("\nDEVICE STATE OF REPAIR");
        verify(util, atLeast(8)).print(any()); //When hit, we know we've gone through the for loop
    }

    /**
     * OWNER: ALICIA
     * <p>
     * Validate we a exited from the damageControl method with a return statement when we aren't docked
     */
    @Test
    void damageControl_not_docked_error_message_displayed() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        enterprise.deviceStatus[Enterprise.DEVICE_DAMAGE_CONTROL] = 6;
        enterprise.docked = false;

        // ACT
        enterprise.damageControl(mock(GameCallback.class));

        // ASSERT
        verify(util).println("DEVICE NOT DOCKED!");
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void damageControl_docked_deltaToRepair_increments_by_ten_percent() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        enterprise.deviceStatus[Enterprise.DEVICE_DAMAGE_CONTROL] = -6;
        enterprise.docked = true;
        when(util.inputStr(any())).thenReturn("Y");


        // ACT
        enterprise.damageControl(mock(GameCallback.class));

        // ASSERT - deltaToRepair can't be accessed, so if we hit the print message, we know there was an increment
        // of .1
        verify(util, atLeastOnce()).println("DELTA REPAIR INCREASED FOR " + any());
    }

    /**
     * OWNER: ALICIA
     * <p>
     * This tests that when the deltaToRepair is greater than one, we reset it to 0.9 and the incrementStartDate is
     * only incremented by 1.0.
     */
    @Test
    void damageControl_deltaToRepair_greater_than_one_verify_startDate_incremented_by_one() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        // set 5 devices to negative so we increment deltaToRepair to 0.5
        enterprise.deviceStatus[1] = -5;
        enterprise.deviceStatus[2] = -5;
        enterprise.deviceStatus[3] = -5;
        enterprise.deviceStatus[4] = -5;
        enterprise.deviceStatus[5] = -5;

        GameCallback callbackTest = mock(GameCallback.class);
        enterprise.docked = true;

        when(util.inputStr(any())).thenReturn("Y");
        when(util.random()).thenReturn(1f); // set our random number to 1

        // ACT
        enterprise.randomRepairCost();
        enterprise.damageControl(callbackTest);

        // ASSERT
        verify(callbackTest).incrementStardate(1.0);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void randomRepairCost_returns_half_of_the_random_value() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        when(util.random()).thenReturn(6f); // set our random number
        float expectedRepairCost = 3;

        // ACT
        enterprise.randomRepairCost();

        // ASSERT
        assertEquals(expectedRepairCost, enterprise.repairCost);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void repairDamagedDevices_verify_device_status_greater_than_zero_damage_repaired() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        enterprise.deviceStatus[1] = -1;
        float warp = 2;

        // ACT
        enterprise.repairDamagedDevices(warp);

        // ASSERT - if we hit the first print statement for repair, we know the repairs were completed
        verify(util).println("DAMAGE CONTROL REPORT:  ");
        assertNotEquals(-.1, enterprise.deviceStatus[1]); // device status is >= 0, should not be reset
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void repairDamagedDevices_verify_device_status_negative_value_reset() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        enterprise.deviceStatus[1] = -1.05;
        float warp = 1;

        // ACT
        enterprise.repairDamagedDevices(warp);

        // ASSERT - if we hit the first print statement for repair, we know the repairs were completed
        assertEquals(-.1, enterprise.deviceStatus[1]);

    }

    /**
     * OWNER: MUSTAFA
     * Verify that getEnergy returns the initial energy equal to 3000
     */
    @Test
    public void getEnergy_must_return_3000_energy() {
        //ARRANGE
        Enterprise enterprise = new Enterprise(util);

        //ACT

        //ASSERT
        assertEquals(3000, enterprise.getEnergy());
    }

    /**
     * OWNER: MUSTAFA
     * Verify that replenishSupplies will replenish energy and torpedoes to 3000 and 10
     */
    @Test
    void replenishSupplies_must_return_3000_energy_and_10_torpedoes() {
        //ARRANGE
        Enterprise enterprise = new Enterprise(util);

//        //initial energy = 3000
//        energy = enterprise.energy;
//        //initial torpedoes = 10
//        torpedoes = enterprise.torpedoes;

        //ACT
        // energy left = 3000-50 = 2950
        enterprise.decreaseEnergy(50.0);

        // torpedoes left = 10-3 = 7
        enterprise.decreaseTorpedoes(3);

        //replenishSupplies:
        //energy left = 3000
        //torpedoes left = 10
        enterprise.replenishSupplies();

        //ASSERT
        assertEquals(3000, enterprise.energy);
        assertEquals(10, enterprise.torpedoes);
    }

    /**
     * OWNER: MUSTAFA
     * Verify that decreaseEnergy will reduce energy from 3000 to 2500
     */
    @Test
    void decreaseEnergy_will_reduce_energy_from_initialEnergy_to_2500() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        // ACT
        enterprise.decreaseEnergy(500.0);

        // ASSERT
        assertEquals(2500, enterprise.getEnergy());
    }

    /**
     * OWNER: MUSTAFA
     * Verify that decreaseTorpedoes will reduce torpedoes from 10 to 7
     */
    @Test
    void decreaseTorpedoes_will_reduce_energy_from_initialTorpedoes_to_7() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        // ACT
        enterprise.decreaseTorpedoes(3);

        // ASSERT
        assertEquals(7, enterprise.getTorpedoes());
    }


    /**
     * OWNER: Sanchita
     * As the shield value is 0 the total energy returned is 300
     */
    @Test
    void getTotalEnergy_must_return_3000_energy() {
        // ARRANGE
        int energy = 3000;
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(energy, enterprise.getTotalEnergy());
    }

    /**
     * OWNER: Sanchita
     * validate that the initial energy is set to 3000
     */
    @Test
    void getInitialEnergy_must_return_3000_energy() {
        // ARRANGE
        int energy = 3000;
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(energy, enterprise.getInitialEnergy());
    }

    /**
     * OWNER: MUSTAFA
     */
    @Test
    void getTorpedoes_must_return_10_torpedoes() {
        //ARRANGE
        Enterprise enterprise = new Enterprise(util);

        //ACT

        //ASSERT
        assertEquals(10, enterprise.getTorpedoes());
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void getDeviceStatus_returns_the_correct_array() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        enterprise.deviceStatus[3] = 100;

        // ACT
        double[] test = enterprise.getDeviceStatus();

        // ASSERT
        assertEquals(enterprise.deviceStatus, test);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void getCardinalDirections_returns_initial_value_negative_one() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        int expectedValue = -1;

        // ACT
        int[][] testDirections = enterprise.getCardinalDirections();

        // ASSERT - directs at 5,2 are set to -1, verify we return that
        assertEquals(expectedValue, testDirections[5][2]);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void setDeviceStatus_verify_device_set_to_ten() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        int testDevice = 1;
        double testStatus = 10;

        // ACT
        enterprise.setDeviceStatus(testDevice, testStatus);

        // ASSERT
        assertEquals(testStatus, enterprise.deviceStatus[1]);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void setDocked_to_true_validate_changed_from_initial_bool() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);

        // ACT
        enterprise.setDocked(true);
        boolean enterpriseDocked = enterprise.isDocked();

        // ASSERT
        assertTrue(enterpriseDocked);
    }

    /**
     * OWNER: ALICIA
     */
    @Test
    void setQuadrant_updates_quadrant_values() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        int x = 5;
        int y = 3;
        int[] testCoords = new int[2];

        // ACT
        testCoords[0] = x;
        testCoords[1] = y;

        enterprise.setQuadrant(x, y);

        // ASSERT
        assertEquals(testCoords[0], enterprise.getQuadrant()[0]);
        assertEquals(testCoords[1], enterprise.getQuadrant()[1]);
    }


    /**
     * OWNER: ALICIA
     */
    @Test
    void setSector() {
        // ARRANGE
        Enterprise enterprise = new Enterprise(util);
        int x = 1;
        int y = 7;
        int[] testCoords = new int[2];

        // ACT
        testCoords[0] = x;
        testCoords[1] = y;

        enterprise.setSector(x, y);

        // ASSERT
        assertEquals(testCoords[0], enterprise.getSector()[0]);
        assertEquals(testCoords[1], enterprise.getSector()[1]);
    }

    /**
     * OWNER: MUSTAFA
     * Verify that we print
     * "WARP ENGINES SHUT DOWN AT "
     * and "SECTOR " + sectorX + "," + sectorY + " DUE TO BAD NAVIGATION"
     * when sectorX = 5 and sectorY = 3
     */

    @Test
    void moveShip_verify_that_warp_engine_shutsDown_due_to_bad_navigation_when_sectorX_is_equalTo_5_and_sectorY_is_equalTo_3() {

        //ARRANGE PARAMETERS
        Enterprise enterprise = new Enterprise(util);

        int course = 1;
        int n = 24; // n = warp * 8, passed through mulitple classes
        double stardate = 28;
        int missionDuration = 25;
        String quadrantMap = "                                                                                                                             >!<                                                               ";

        int sectorX = 5;
        int sectorY = 3;

        int quadrantX = 6;
        int quadrantY = 4;
        int initialQuadrantX = quadrantX;
        int initialQuadrantY = quadrantY;
        int x1 = 0;
        int x2 = 1;

        when(util.toInt(course)).thenCallRealMethod(); // line 159 in sstproject.Enterprise class

        sectorX = sectorX + x1; // line 170 sectorX updates
        sectorY = sectorY + x2;// line 171 sectorY updates

        when(util.toInt(sectorX)).thenCallRealMethod(); // line 247 in sstproject.Enterprise class
        when(util.toInt(sectorY)).thenCallRealMethod(); // line 247 in sstproject.Enterprise class

        when(util.toInt(sectorX - x1)).thenCallRealMethod(); // line 249 in sstproject.Enterprise class
        when(util.toInt(sectorY - x2)).thenCallRealMethod(); // line 250 in sstproject.Enterprise class

        //reset sectorX and sectorY for ACT so that the logic above is applied as intended
        sectorX = initialQuadrantX;
        sectorY = initialQuadrantY;

        // ACT
        enterprise.setSector(sectorX, sectorY);
        enterprise.setQuadrant(quadrantX, quadrantY);

        int[] sector = enterprise.moveShip(course, n, quadrantMap, stardate, stardate, missionDuration,
                mock(GameCallback.class));

        // ASSERT
        verify(util).println("WARP ENGINES SHUT DOWN AT ");
        verify(util).println("SECTOR " + enterprise.getSector()[0] + "," + enterprise.getSector()[1] + " DUE TO BAD NAVIGATION");
        assertArrayEquals(enterprise.getSector(), sector);

    }

    /**
     * OWNER: MUSTAFA
     * Verify that we hit an edge when sectorX = -1 and quadrantY = -20
     */

    @Test
    void moveShip_verify_that_ships_hitsEdge_when_sectorX_is_equalTo_negative1_and_quadrantY_is_equalTo_negative20() {
        Enterprise enterprise = new Enterprise(util);

        // ARRANGE

        int course = 1;
        int n = 24;
        double stardate = 28;
        int missionDuration = 25;
        String quadrantMap = "                                                                                                                              >!<                                                               ";

        int sectorX = -1;
        int sectorY = 3;

        int quadrantX = 6;
        int quadrantY = -20;

        int initialQuadrantX = quadrantX;
        int initialQuadrantY = quadrantY;
        int initialSectorX = sectorX;
        int initialSectorY = sectorY;
        int x1 = 0;
        int x2 = 1;
        float x = sectorX;
        float y = sectorY;

        when(util.toInt(course)).thenCallRealMethod(); // line 159 in sstproject.Enterprise class

        sectorX = sectorX + x1; // line 170 sectorX updates
        sectorY = sectorY + x2;// line 171 sectorY updates

        //prepare x and y for line 176 and 177
        x = 8 * quadrantX + x + n * x1;
        y = 8 * quadrantY + y + n * x2;

        when(util.toInt(x/8)).thenCallRealMethod(); // line 176 in sstproject.Enterprise class
        when(util.toInt(y/8)).thenCallRealMethod(); // line 177 in sstproject.Enterprise class

        quadrantX = util.toInt(x/8);
        quadrantY = util.toInt(y/8);

        when(util.toInt(x - quadrantX * 8)).thenCallRealMethod(); // line 178 in sstproject.Enterprise class
        when(util.toInt(y - quadrantY * 8)).thenCallRealMethod(); // line 179 in sstproject.Enterprise class

        //reset sectorX and sectorY for ACT so that the logic above is applied as intended
        sectorX = initialSectorX;
        sectorY = initialSectorY;

        //reset quadrantX and quadrantY for ACT so that the logic above is applied as intended
        quadrantX = initialQuadrantX;
        quadrantY = initialQuadrantY;

        // ACT
        enterprise.setSector(sectorX, sectorY);
        enterprise.setQuadrant(quadrantX, quadrantY);
        int[] sector = enterprise.moveShip(course, n, quadrantMap, stardate, stardate, missionDuration,
                mock(GameCallback.class));

        // ASSERT
        verify(util).println("LT. UHURA REPORTS MESSAGE FROM STARFLEET COMMAND:");
        verify(util).println("  'PERMISSION TO ATTEMPT CROSSING OF GALACTIC PERIMETER");
        verify(util).println("  IS HEREBY *DENIED*.  SHUT DOWN YOUR ENGINES.'");
        verify(util).println("CHIEF ENGINEER SCOTT REPORTS  'WARP ENGINES SHUT DOWN");
        verify(util).println("  AT SECTOR " +
                enterprise.getSector()[0]+
                "," +
                enterprise.getSector()[1] +
                " OF QUADRANT " +
                enterprise.getQuadrant()[0] +
                "," +
                enterprise.getQuadrant()[1] +
                ".'");
        assertArrayEquals(enterprise.getSector(), sector);
    }

    /**
     * OWNER: MUSTAFA
     * Verify that we hit an edge when sectorX = -1 and quadrantX = -20
     */

    @Test
    void moveShip_verify_that_ship_hitsEdge_when_sectorX_is_equalTo_negative1_and_quadrantX_is_equalTo_negative20() {
        Enterprise enterprise = new Enterprise(util);

        // ARRANGE

        int course = 1;
        int n = 24;
        double stardate = 28;
        int missionDuration = 25;
        String quadrantMap = "                                                                                                                              >!<                                                               ";

        int sectorX = -1;
        int sectorY = 3;

        int quadrantX = -20;
        int quadrantY = 4;

        int initialQuadrantX = quadrantX;
        int initialQuadrantY = quadrantY;
        int initialSectorX = sectorX;
        int initialSectorY = sectorY;
        int x1 = 0;
        int x2 = 1;
        float x = sectorX;
        float y = sectorY;

        when(util.toInt(course)).thenCallRealMethod(); // line 159 in sstproject.Enterprise class

        sectorX = sectorX + x1; // line 170 sectorX updates
        sectorY = sectorY + x2;// line 171 sectorY updates

        //prepare x and y for line 176 and 177
        x = 8 * quadrantX + x + n * x1;
        y = 8 * quadrantY + y + n * x2;

        when(util.toInt(x/8)).thenCallRealMethod(); // line 176 in sstproject.Enterprise class
        when(util.toInt(y/8)).thenCallRealMethod(); // line 177 in sstproject.Enterprise class

        quadrantX = util.toInt(x/8);
        quadrantY = util.toInt(y/8);

        when(util.toInt(x - quadrantX * 8)).thenCallRealMethod(); // line 178 in sstproject.Enterprise class
        when(util.toInt(y - quadrantY * 8)).thenCallRealMethod(); // line 179 in sstproject.Enterprise class

        //reset sectorX and sectorY for ACT so that the logic above is applied as intended
        sectorX = initialSectorX;
        sectorY = initialSectorY;

        //reset quadrantX and quadrantY for ACT so that the logic above is applied as intended
        quadrantX = initialQuadrantX;
        quadrantY = initialQuadrantY;

        // ACT
        enterprise.setSector(sectorX, sectorY);
        enterprise.setQuadrant(quadrantX, quadrantY);
        int[] sector = enterprise.moveShip(course, n, quadrantMap, stardate, stardate, missionDuration,
                mock(GameCallback.class));

        // ASSERT
        verify(util).println("LT. UHURA REPORTS MESSAGE FROM STARFLEET COMMAND:");
        verify(util).println("  'PERMISSION TO ATTEMPT CROSSING OF GALACTIC PERIMETER");
        verify(util).println("  IS HEREBY *DENIED*.  SHUT DOWN YOUR ENGINES.'");
        verify(util).println("CHIEF ENGINEER SCOTT REPORTS  'WARP ENGINES SHUT DOWN");
        verify(util).println("  AT SECTOR " +
                enterprise.getSector()[0]+
                "," +
                enterprise.getSector()[1] +
                " OF QUADRANT " +
                enterprise.getQuadrant()[0] +
                "," +
                enterprise.getQuadrant()[1] +
                ".'");
        assertArrayEquals(enterprise.getSector(), sector);
    }

    /**
     * OWNER: MUSTAFA
     * Verify that we hit an edge when quadrantX = 0, quadrantY = 0, quadrantX = 0, and sectorY = -10
     */

    @Test
    void moveShip_verify_that_ship_hitsEdge_when_sectorX_and_quadrantX_and_quadrantY_are_equalTo_zero_sectorY_equalTo_negative10() {
        Enterprise enterprise = new Enterprise(util);

        // ARRANGE

        int course = 1;
        int n = 10;
        double stardate = 28;
        int missionDuration = 25;
        String quadrantMap = "                                                                                                                              >!<                                                               ";

        int sectorX = 0;
        int sectorY = -10;

        int quadrantX = 0;
        int quadrantY = 0;

        int initialQuadrantX = quadrantX;
        int initialQuadrantY = quadrantY;
        int initialSectorX = sectorX;
        int initialSectorY = sectorY;
        int x1 = 0;
        int x2 = 1;
        float x = sectorX;
        float y = sectorY;

        when(util.toInt(course)).thenCallRealMethod(); // line 159 in sstproject.Enterprise class

        sectorX = sectorX + x1; // line 170 sectorX updates
        sectorY = sectorY + x2;// line 171 sectorY updates

        //prepare x and y for line 176 and 177
        x = 8 * quadrantX + x + n * x1;
        y = 8 * quadrantY + y + n * x2;

        when(util.toInt(x/8)).thenCallRealMethod(); // line 176 in sstproject.Enterprise class
        when(util.toInt(y/8)).thenCallRealMethod(); // line 177 in sstproject.Enterprise class

        quadrantX = util.toInt(x/8);
        quadrantY = util.toInt(y/8);

        when(util.toInt(x - quadrantX * 8)).thenCallRealMethod(); // line 178 in sstproject.Enterprise class
        when(util.toInt(y - quadrantY * 8)).thenCallRealMethod(); // line 179 in sstproject.Enterprise class

        //reset sectorX and sectorY for ACT so that the logic above is applied as intended
        sectorX = initialSectorX;
        sectorY = initialSectorY;

        //reset quadrantX and quadrantY for ACT so that the logic above is applied as intended
        quadrantX = initialQuadrantX;
        quadrantY = initialQuadrantY;

        // ACT
        enterprise.setSector(sectorX, sectorY);
        enterprise.setQuadrant(quadrantX, quadrantY);
        int[] sector = enterprise.moveShip(course, n, quadrantMap, stardate, stardate, missionDuration,
                mock(GameCallback.class));

        // ASSERT
        verify(util).println("LT. UHURA REPORTS MESSAGE FROM STARFLEET COMMAND:");
        verify(util).println("  'PERMISSION TO ATTEMPT CROSSING OF GALACTIC PERIMETER");
        verify(util).println("  IS HEREBY *DENIED*.  SHUT DOWN YOUR ENGINES.'");
        verify(util).println("CHIEF ENGINEER SCOTT REPORTS  'WARP ENGINES SHUT DOWN");
        verify(util).println("  AT SECTOR " +
                enterprise.getSector()[0]+
                "," +
                enterprise.getSector()[1] +
                " OF QUADRANT " +
                enterprise.getQuadrant()[0] +
                "," +
                enterprise.getQuadrant()[1] +
                ".'");
        assertArrayEquals(enterprise.getSector(), sector);
    }

    /**
     * OWNER: MUSTAFA
     * Verify that the ship hits and edge to set endGameCall back to false when sectorY, quadrantX = -10 and missionDuration= 0
     */

    @Test
    void moveShip_verify_that_ship_hitsEdge_when_sectorY_and_quadrantX_are_negative10_and_missionDuration_is_zero() {
        Enterprise enterprise = new Enterprise(util);

        // ARRANGE

        int course = 1;
        int n = 10;
        double stardate = 28;
        int missionDuration = -10;
        String quadrantMap = "                                                                                                                              >!<                                                               ";

        int sectorX = 4;
        int sectorY = -1;

        int quadrantX = -10;
        int quadrantY = 4;

        int initialQuadrantX = quadrantX;
        int initialQuadrantY = quadrantY;
        int initialSectorX = sectorX;
        int initialSectorY = sectorY;
        int x1 = 0;
        int x2 = 1;
        float x = sectorX;
        float y = sectorY;

        when(util.toInt(course)).thenCallRealMethod(); // line 159 in sstproject.Enterprise class

        sectorX = sectorX + x1; // line 170 sectorX updates
        sectorY = sectorY + x2;// line 171 sectorY updates

        //prepare x and y for line 176 and 177
        x = 8 * quadrantX + x + n * x1;
        y = 8 * quadrantY + y + n * x2;

        when(util.toInt(x/8)).thenCallRealMethod(); // line 176 in sstproject.Enterprise class
        when(util.toInt(y/8)).thenCallRealMethod(); // line 177 in sstproject.Enterprise class

        quadrantX = util.toInt(x/8);
        quadrantY = util.toInt(y/8);

        when(util.toInt(x - quadrantX * 8)).thenCallRealMethod(); // line 178 in sstproject.Enterprise class
        when(util.toInt(y - quadrantY * 8)).thenCallRealMethod(); // line 179 in sstproject.Enterprise class

        //reset sectorX and sectorY for ACT so that the logic above is applied as intended
        sectorX = initialSectorX;
        sectorY = initialSectorY;

        //reset quadrantX and quadrantY for ACT so that the logic above is applied as intended
        quadrantX = initialQuadrantX;
        quadrantY = initialQuadrantY;

        // ACT
        enterprise.setSector(sectorX, sectorY);
        enterprise.setQuadrant(quadrantX, quadrantY);
        int[] sector = enterprise.moveShip(course, n, quadrantMap, stardate, stardate, missionDuration,
                mock(GameCallback.class));

        // ASSERT
        verify(util).println("LT. UHURA REPORTS MESSAGE FROM STARFLEET COMMAND:");
        verify(util).println("  'PERMISSION TO ATTEMPT CROSSING OF GALACTIC PERIMETER");
        verify(util).println("  IS HEREBY *DENIED*.  SHUT DOWN YOUR ENGINES.'");
        verify(util).println("CHIEF ENGINEER SCOTT REPORTS  'WARP ENGINES SHUT DOWN");
        verify(util).println("  AT SECTOR " +
                enterprise.getSector()[0]+
                "," +
                enterprise.getSector()[1] +
                " OF QUADRANT " +
                enterprise.getQuadrant()[0] +
                "," +
                enterprise.getQuadrant()[1] +
                ".'");
        assertArrayEquals(enterprise.getSector(), sector);
    }

    /**
     * OWNER: MUSTAFA
     * Verify that the ship hits and edge when sectorX and sectorY = 9, and quadrantX and quadrantY = 8
     */

    @Test
    void moveShip_verify_that_ship_hitsEdge_when_sectorX_and_sectorY_are_10_and_quadrantX_and_quadrantY_are_8() {
        Enterprise enterprise = new Enterprise(util);

        // ARRANGE

        int course = 0;
        int n = 1;
        double stardate = 28;
        int missionDuration = 20;
        String quadrantMap = "                                                                                                                              >!<                                                               ";

        int sectorX = 10;
        int sectorY = 10;

        int quadrantX = 8;
        int quadrantY = 8;

        int initialQuadrantX = quadrantX;
        int initialQuadrantY = quadrantY;
        int initialSectorX = sectorX;
        int initialSectorY = sectorY;
        int x1 = 0;
        int x2 = 1;
        float x = sectorX;
        float y = sectorY;

        when(util.toInt(course)).thenCallRealMethod(); // line 159 in sstproject.Enterprise class

        sectorX = sectorX + x1; // line 170 sectorX updates
        sectorY = sectorY + x2;// line 171 sectorY updates

        //prepare x and y for line 176 and 177
        x = 8 * quadrantX + x + n * x1;
        y = 8 * quadrantY + y + n * x2;

        when(util.toInt(x/8)).thenCallRealMethod(); // line 176 in sstproject.Enterprise class
        when(util.toInt(y/8)).thenCallRealMethod(); // line 177 in sstproject.Enterprise class

        quadrantX = util.toInt(x/8);
        quadrantY = util.toInt(y/8);

        when(util.toInt(x - quadrantX * 8)).thenCallRealMethod(); // line 178 in sstproject.Enterprise class
        when(util.toInt(y - quadrantY * 8)).thenCallRealMethod(); // line 179 in sstproject.Enterprise class

        sectorX = 8;
        sectorY = 8;

        when(util.toInt(sectorX)).thenCallRealMethod(); // line 257 in sstproject.Enterprise class
        when(util.toInt(sectorY)).thenCallRealMethod(); // line 258 in sstproject.Enterprise class

        //reset sectorX and sectorY for ACT so that the logic above is applied as intended
        sectorX = initialSectorX;
        sectorY = initialSectorY;

        //reset quadrantX and quadrantY for ACT so that the logic above is applied as intended
        quadrantX = initialQuadrantX;
        quadrantY = initialQuadrantY;

        // ACT
        enterprise.setSector(sectorX, sectorY);
        enterprise.setQuadrant(quadrantX, quadrantY);
        int[] sector = enterprise.moveShip(course, n, quadrantMap, stardate, stardate, missionDuration,
                mock(GameCallback.class));

        // ASSERT
        verify(util).println("LT. UHURA REPORTS MESSAGE FROM STARFLEET COMMAND:");
        verify(util).println("  'PERMISSION TO ATTEMPT CROSSING OF GALACTIC PERIMETER");
        verify(util).println("  IS HEREBY *DENIED*.  SHUT DOWN YOUR ENGINES.'");
        verify(util).println("CHIEF ENGINEER SCOTT REPORTS  'WARP ENGINES SHUT DOWN");
        verify(util).println("  AT SECTOR " +
                enterprise.getSector()[0]+
                "," +
                enterprise.getSector()[1] +
                " OF QUADRANT " +
                enterprise.getQuadrant()[0] +
                "," +
                enterprise.getQuadrant()[1] +
                ".'");
        assertArrayEquals(enterprise.getSector(), sector);
    }

    /**
     * OWNER: Sanchita
     * to ensure the value of shields is 0 when energy goes below 0 and shields goes below 0
     */
    @Test
    void maneuverEnergySR_shield_value_decreased_when_energy_becomes_less_than_zero() {
        // ARRANGE
        int expectedShield = 490;
        int energy = 0;
        int N = 2500;
        int userInput = 1000;
        Enterprise enterprise = new Enterprise(util);
        //ACT
        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        enterprise.shieldControl();
        assertEquals(userInput, enterprise.getShields());

        enterprise.maneuverEnergySR(N);

        //ASSERT
        verify(util).println("SHIELD CONTROL SUPPLIES ENERGY TO COMPLETE THE MANEUVER.");
        assertEquals(energy, enterprise.getEnergy());
        assertEquals(expectedShield, enterprise.getShields());
    }

    /**
     * OWNER: Sanchita
     * to ensure energy becomes 0 when energy is below zero and shield is appropriately reduced
     */
    @Test
    void maneuverEnergySR_shield_value_zero_when_energy_becomes_less_than_zero_and_shield_zero() {
        // ARRANGE
        int expectedShield = 0;
        int energy = 0;
        int N = 4500;
        int userInput = 1000;
        Enterprise enterprise = new Enterprise(util);

        //ACT
        // create the mock environment for user input commands
        when(util.inputFloat(any())).thenReturn((float) userInput);
        when(util.toInt((float) userInput)).thenReturn(userInput);

        enterprise.shieldControl();
        assertEquals(userInput, enterprise.getShields());

        enterprise.maneuverEnergySR(N);

        //ASSERT
        verify(util).println("SHIELD CONTROL SUPPLIES ENERGY TO COMPLETE THE MANEUVER.");
        assertEquals(energy, enterprise.getEnergy());
        assertEquals(expectedShield, enterprise.getShields());
    }

    /**
     * OWNER: Sanchita
     * to ensure energy is getting reduced when maneuverEnergySR method is called
     */
    @Test
    void maneuverEnergySR_calculate_energy_when_value_of_n_is_500() {
        // ARRANGE
        int energy = 2490;
        int N = 500;
        Enterprise enterprise = new Enterprise(util);

        //ACT
        enterprise.maneuverEnergySR(N);

        //ASSERT
        assertEquals(energy, enterprise.getEnergy());
    }

    /**
     * OWNER: Sanchita
     * to ensure energy becomes 0 when energy is below zero
     */
    @Test
    void maneuverEnergySR_zero_energy_when_value_of_energy_goes_below_0() {
        // ARRANGE
        int energy = 0;
        int N = 3500;
        Enterprise enterprise = new Enterprise(util);

        //ACT
        enterprise.maneuverEnergySR(N);

        //ASSERT
        assertEquals(energy, enterprise.getEnergy());
    }

    /**
     * OWNER: Sanchita
     * to ensure the print device name method is printing the device name according to device number
     */
    @Test
    void printDeviceName_for_device_warp_engines_and_input_is_zero() {
        // ARRANGE
        String deviceName = "WARP ENGINES";
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(deviceName, enterprise.printDeviceName(0));
    }

    /**
     * OWNER: Sanchita
     * to ensure the print device name method is printing the device name according to device number
     */
    @Test
    void printDeviceName_for_device_short_range_sensors_and_input_is_one() {
        // ARRANGE
        String deviceName = "SHORT RANGE SENSORS";
        Enterprise enterprise = new Enterprise(util);

        //ACT

        //ASSERT
        assertEquals(deviceName, enterprise.printDeviceName(1));
    }

    /**
     * OWNER: Sanchita
     * to ensure the print device name method is printing the device name according to device number
     */
    @Test
    void printDeviceName_for_device_long_range_sensors_and_input_is_two() {
        // ARRANGE
        String deviceName = "LONG RANGE SENSORS";
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(deviceName, enterprise.printDeviceName(2));
    }

    /**
     * OWNER: Sanchita
     * to ensure the print device name method is printing the device name according to device number
     */
    @Test
    void printDeviceName_for_device_phaser_control_and_input_is_three() {
        // ARRANGE
        String deviceName = "PHASER CONTROL";
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(deviceName, enterprise.printDeviceName(3));
    }

    /**
     * OWNER: Sanchita
     * to ensure the print device name method is printing the device name according to device number
     */
    @Test
    void printDeviceName_for_device_photon_tubes_and_input_is_four() {
        // ARRANGE
        String deviceName = "PHOTON TUBES";
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(deviceName, enterprise.printDeviceName(4));
    }

    /**
     * OWNER: Sanchita
     * to ensure the print device name method is printing the device name according to device number
     */
    @Test
    void printDeviceName_for_device_damage_control_and_input_is_five() {
        // ARRANGE
        String deviceName = "DAMAGE CONTROL";
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(deviceName, enterprise.printDeviceName(5));
    }

    /**
     * OWNER: Sanchita
     * to ensure the print device name method is printing the device name according to device number
     */
    @Test
    void printDeviceName_for_invalid_input() {
        // ARRANGE
        String deviceName = "";
        Enterprise enterprise = new Enterprise(util);
        //ACT

        //ASSERT
        assertEquals(deviceName, enterprise.printDeviceName(9));
    }

}
