using System;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Rooms.Chat
{
    /// <summary>
    ///     Class UserSaysArgs.
    /// </summary>
    public class UserSaysArgs : EventArgs
    {
        /// <summary>
        ///     The message
        /// </summary>
     public readonly string Message;

        /// <summary>
        ///     The user
        /// </summary>
     public readonly RoomUser User;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSaysArgs" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        public UserSaysArgs(RoomUser user, string message)
        {
            User = user;
            Message = message;
        }
    }
}