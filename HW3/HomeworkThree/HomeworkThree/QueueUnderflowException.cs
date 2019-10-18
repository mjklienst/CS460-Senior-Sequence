using System;

namespace HomeworkThree
{


/**
 * A custom unchecked exception to represent situations where 
 * an illegal operation was performed on an empty queue.
 */
public class QueueUnderflowException : SystemException
    {
  public QueueUnderflowException()
{
    base();
}

public QueueUnderflowException(String message)
{
    base(message);
}
}

}
