﻿using System;
namespace Bifrost.Events
{
	/// <summary>
	/// Represents the versioning for an <see cref="EventSource"/>
	/// </summary>
    public struct EventSourceVersion : IComparable<EventSourceVersion>
    {
        const float SEQUENCE_DIVISOR = 10000;

		/// <summary>
		/// Zero/null version
		/// </summary>
        public static readonly EventSourceVersion Zero = new EventSourceVersion {Commit = 0, Sequence = 0};

		/// <summary>
		/// Initializes a new instance of <see cref="EventSourceVersion"/>
		/// </summary>
		/// <param name="commit">Commit part of the version (major)</param>
		/// <param name="sequence">Sequence part of the version, within the commit (minor) </param>
        public EventSourceVersion(int commit, int sequence) : this()
        {
            Commit = commit;
            Sequence = sequence;
        }

		/// <summary>
		/// Gets the commit number of the version
		/// </summary>
        public int Commit { get; set; }

		/// <summary>
		/// Gets the sequence number of the version
		/// </summary>
        public int Sequence { get; set; }


		/// <summary>
		/// Increase the commit number and return a new version
		/// </summary>
		/// <returns><see cref="EventSourceVersion"/> with the new version</returns>
        public EventSourceVersion NextCommit()
        {
            var nextCommit = new EventSourceVersion {Commit = Commit + 1, Sequence = 0};
            return nextCommit;
        }

		/// <summary>
		/// Increase the sequence number and return a new version
		/// </summary>
		/// <returns><see cref="EventSourceVersion"/> with the new version</returns>
        public EventSourceVersion NextSequence()
        {
            var nextSequence = new EventSourceVersion { Commit = Commit, Sequence = Sequence+1 };
            return nextSequence;
        }


		/// <summary>
		/// Decrease the commit number and return a new version
		/// </summary>
		/// <returns><see cref="EventSourceVersion"/> with the new version</returns>
        public EventSourceVersion PreviousCommit()
        {
            var previousCommit = new EventSourceVersion { Commit = Commit - 1, Sequence = 0 };
            return previousCommit;
        }


		/// <summary>
		/// Compare this version with another version
		/// </summary>
		/// <param name="other">The other version to compare to</param>
		/// <returns>
        /// Less than zero - this instance is less than the other version
        /// Zero - this instance is equal to the other version
        /// Greater than zero - this instance is greater than the other version
        /// </returns>
        public int CompareTo(EventSourceVersion other)
        {
		    var current = Combine();
		    var otherVersion = other.Combine();
		    return current.CompareTo(otherVersion);
        }

        /// <summary>
        /// Combines the Major / Minor number of Commit and Sequence into a single floating point number
        /// where the Commit is before the decimal place and Sequence is after.
        /// </summary>
        /// <returns></returns>
        public float Combine()
        {
            var majorNumber = (float) Commit;
            var minorNumber = ((float)Sequence / SEQUENCE_DIVISOR);
            var versionAsFloat = majorNumber + minorNumber;
            return versionAsFloat;
        }
    }
}