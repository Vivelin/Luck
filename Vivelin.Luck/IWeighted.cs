using System;

namespace Vivelin.Luck
{
    /// <summary>
    /// Provides a mechanism for influencing the chance an element will be
    /// randomly selected from a list.
    /// </summary>
    public interface IWeighted
    {
        /// <summary>
        /// Gets a value that determines the relative weight of this instance
        /// versus other elements in a list.
        /// </summary>
        double Weight { get; }
    }
}