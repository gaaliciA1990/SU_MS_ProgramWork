package sstproject;

/**
 * Interface for decoupling inversion of control from sstproject.GalaxyMap and sstproject.Enterprise towards the game class.
 */
public interface GameCallback {
    void enterNewQuadrant();
    void incrementStardate(double increment);
    void endGameSuccess();
    void endGameFail(boolean enterpriseDestroyed);
}
