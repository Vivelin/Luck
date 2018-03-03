using System;

namespace Vivelin.Luck
{
    /// <summary>
    /// Represents the error that occurs when unreachable code is reached.
    /// </summary>
    public class UnreachableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnreachableException"/> class.
        /// </summary>
        public UnreachableException() : this(Exceptions.ReachedUnreachableCode) { }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="UnreachableException"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnreachableException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="UnreachableException"></see> class with a specified error
        /// message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null
        /// reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public UnreachableException(string message, Exception innerException) : base(message, innerException) { }
    }
}