package Q3;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ValueSource;

import static org.junit.jupiter.api.Assertions.*;

/**
 * Author: Alicia Garcia
 * Version: 1.0
 * Date: 4/14/2022 18:35
 */

class BoundedQueueTest {
    /**
     * Test an exception is thrown when an invalid capacity is passed to constructor
     *
     * @param capacity
     */
    @ParameterizedTest
    @ValueSource(ints = {-1, -50, Integer.MIN_VALUE})
    public void boundedQueue_constructor_negativeCapacity_throwsException(int capacity) {
        //when

        //do
        Exception exception = assertThrows(IllegalArgumentException.class, () -> {
            BoundedQueue boundedQueue = new BoundedQueue(capacity);
        });

        //assert
        String expectedMessage = "Q3.BoundedQueue.constructor";

        assertEquals(expectedMessage, exception.getMessage());
    }

    /**
     * Test that the queue is created when a valid capacity is passed to constructor
     *
     * @param capacity
     */
    @ParameterizedTest
    @ValueSource(ints = {0, 1265, 956231})
    public void boundedQueue_constructor_goodCapacity_valid(int capacity) {
        //when

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);

        //assert - if we reach here, we can assume that the queue was created successfully
        assertTrue(boundedQueue.isEmpty());
    }

    /**
     * Test that exception is thrown when a null object is added to the Queue
     */
    @Test
    public void boundedQueue_enqueue_objectIsNull_throwsException() {
        //when
        int capacity = 10;

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);

        Exception exception = assertThrows(NullPointerException.class, () -> {
            boundedQueue.enQueue(null);
        });

        //assert
        String expectedMessage = "Q3.BoundedQueue.enQueue";

        assertEquals(expectedMessage, exception.getMessage());
    }

    /**
     * Test that exceptions are thrown when the queue is full and new element
     * is attempted to be added
     *
     * @param capacity
     */
    @ParameterizedTest
    @ValueSource(ints = {0, 1, 10, 60, 3215685})
    public void boundedQueue_enqueue_queueIsFull_throwsException(int capacity) {
        //when

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);
        for (int i = 0; i < capacity; i++) {
            boundedQueue.enQueue(new Object());
        }

        Exception exception = assertThrows(IllegalStateException.class, () -> {
            boundedQueue.enQueue(new Object());
        });

        //assert
        String expectedMessage = "Q3.BoundedQueue.enQueue";

        assertEquals(expectedMessage, exception.getMessage());
    }

    /**
     * Test that when a queue is full on enqueue, we can successfully deQueue and add a
     * new element to the queue.
     * capacity = 0 will fail, so not included
     *
     * @param capacity
     */
    @ParameterizedTest
    @ValueSource(ints = {1, 10, 60, 3215685})
    public void boundedQueue_enqueue_queueIsFull_dequeAfter_shouldSucceed(int capacity) {
        //when

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);
        for (int i = 0; i < capacity; i++) {
            boundedQueue.enQueue(new Object());
        }

        Exception exception = assertThrows(IllegalStateException.class, () -> {
            boundedQueue.enQueue(new Object());
        });

        boundedQueue.deQueue();
        boundedQueue.enQueue(new Object());

        //assert
    }

    /**
     * Test that, regardless of capacity set, an empty queue will throw an excpetion
     * when deQueue is attempted
     *
     * @param capacity
     */
    @ParameterizedTest
    @ValueSource(ints = {0, 10, 60, 3215685})
    public void boundedQueue_dequeue_emptyQueue_throwsExceptions(int capacity) {
        //when

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);

        Exception exception = assertThrows(IllegalStateException.class, () -> {
            boundedQueue.deQueue();
        });

        String expectedMessage = "Q3.BoundedQueue.deQueue";

        //assert
        assertEquals(expectedMessage, exception.getMessage());
    }

    /**
     * Test that we are deQueueing based on FIFO logic
     */
    @Test
    public void boundedQueue_dequeueFIFO_confirmsOldestElementRemoved() {
        //when
        int capacity = 3;
        Object object1 = new Object();
        Object object2 = new Object();
        Object object3 = new Object();

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);
        boundedQueue.enQueue(object1);
        boundedQueue.enQueue(object2);
        boundedQueue.enQueue(object3);

        Object dequeuedObject = boundedQueue.deQueue();

        //assert
        assertEquals(object1, dequeuedObject);
    }

    /**
     * Test isEmpty returns true when queue is empty
     */
    @Test
    public void boundedQueue_isEmpty_returnsTrue() {
        //when
        int capacity = 3;

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);

        //assert
        assertTrue(boundedQueue.isEmpty());
    }

    /**
     * Test isEmpty returns false when queue is contains objects
     */
    @ParameterizedTest
    @ValueSource(ints = {1, 35, 125, 3215685})
    public void boundedQueue_isEmpty_whenQueueIsPopulated_returnsFalse(int capacity) {
        //when

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);
        for (int i = 0; i < capacity; i++) {
            boundedQueue.enQueue(new Object());
        }

        //assert
        assertFalse(boundedQueue.isEmpty());
    }

    /**
     * Test isFull returns true when queue is full
     *
     * @param capacity
     */
    @ParameterizedTest
    @ValueSource(ints = {0, 1, 35, 125, 3215685})
    public void boundedQueue_isFull_returnTrue(int capacity) {
        //when

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);
        for (int i = 0; i < capacity; i++) {
            boundedQueue.enQueue(new Object());
        }

        //assert
        assertTrue(boundedQueue.isFull());
    }

    /**
     * Test isFull returns false when queue is not completely full
     *
     * @param capacity
     */
    @ParameterizedTest
    @ValueSource(ints = {1, 35, 125, 3215685})
    public void boundedQueue_isFull_queueHasSpace_returnFalse(int capacity) {
        //when

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);
        for (int i = 0; i < capacity - 1; i++) {
            boundedQueue.enQueue(new Object());
        }

        //assert
        assertFalse(boundedQueue.isFull());
    }

    @Test
    public void boundedQueue_toString_printsExpectedString() {
        //when
        int capacity = 3;
        Integer object1 = 5;
        Integer object2 = 12;
        Integer object3 = 8;
        String expectedResult = "[5, 12, 8]";

        //do
        BoundedQueue boundedQueue = new BoundedQueue(capacity);
        boundedQueue.enQueue(object1);
        boundedQueue.enQueue(object2);
        boundedQueue.enQueue(object3);

        //assert
        assertEquals(expectedResult, boundedQueue.toString());
    }

}