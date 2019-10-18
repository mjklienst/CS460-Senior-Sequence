﻿using System;

namespace HomeworkThree
{
    /**
     * A FIFO queue interface.
     */
    public interface QueueInterface<T>
    {
        /**
         * Add an element to the rear of the queue
         * 
         * @return the element that was enqueued
         */
        T push(T element);

        /**
         * Remove and return the front element.
         * 
         * @throws Thrown if the queue is empty
         */
        T pop() throws QueueUnderflowException;

        /**
         * Return but don't remove the front element.
         * 
         * @throws Thrown if the queue is empty
         */
        T peek() throws QueueUnderflowException;

        /**
         * Test if the queue is empty
         * 
         * @return true if the queue is empty; otherwise false
         */
        boolean isEmpty();
    }

}


