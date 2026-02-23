namespace SharpIpp.Protocol.Models
{
    public enum JobHoldUntil
    {
        Unsupported,

        /// <summary>
        ///     'no-hold': immediately, if there are not other reasons to hold the job
        /// </summary>
        NoHold,

        /// <summary>
        ///     'indefinite':  - the job is held indefinitely, until a client performs a Release-Job (section 3.3.6)
        /// </summary>
        Indefinite,

        /// <summary>
        ///     'day-time': during the day
        /// </summary>
        DayTime,

        /// <summary>
        ///     'evening': evening
        /// </summary>
        Evening,

        /// <summary>
        ///     'night': night
        /// </summary>
        Night,

        /// <summary>
        ///     'weekend': weekend
        /// </summary>
        Weekend,

        /// <summary>
        ///     'second-shift': second-shift (after close of business)
        /// </summary>
        SecondShift,

        /// <summary>
        ///     'third-shift': third-shift (after midnight)
        /// </summary>
        ThirdShift,
    }
}
