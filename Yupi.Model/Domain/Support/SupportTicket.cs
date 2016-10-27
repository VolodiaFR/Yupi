﻿// ---------------------------------------------------------------------------------
// <copyright file="SupportTicket.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;

    public class SupportTicket
    {
        #region Properties

        // TODO Use enum
        public virtual int Category
        {
            get; set;
        }

        public virtual TicketCloseReason CloseReason
        {
            get; protected set;
        }

        public virtual DateTime CreatedAt
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual string Message
        {
            get; set;
        }

        // TODO Should reference the chat directly!
        public virtual IList<string> ReportedChats
        {
            get; set;
        }

        public virtual UserInfo ReportedUser
        {
            get; set;
        }

        public virtual RoomData Room
        {
            get; set;
        }

        public virtual int Score
        {
            get; set;
        }

        public virtual UserInfo Sender
        {
            get; set;
        }

        public virtual UserInfo Staff
        {
            get; set;
        }

        public virtual TicketStatus Status
        {
            get; protected set;
        }

        // TODO Enum
        // type (3 or 4 for new style)
        public virtual int Type
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public SupportTicket()
        {
            Status = TicketStatus.Open;
            ReportedChats = new List<string>();
            Staff = UserInfo.None;
            CreatedAt = DateTime.Now;
        }

        #endregion Constructors

        #region Methods

        public virtual void Close(TicketCloseReason reason)
        {
            Status = TicketStatus.Closed;
            CloseReason = reason;
        }

        public virtual void Delete()
        {
            Status = TicketStatus.Closed;
            CloseReason = TicketCloseReason.Deleted;
        }

        public virtual void Pick(UserInfo moderator)
        {
            Status = TicketStatus.Picked;
            Staff = moderator;
        }

        public virtual void Release()
        {
            Status = TicketStatus.Open;
        }

        #endregion Methods

        #region Other

        /*
                /// <summary>
                ///     Serializes the specified messageBuffer.
                /// </summary>
                /// <param name="message">The messageBuffer.</param>
                /// <returns>SimpleServerMessageBuffer.</returns>
             public SimpleServerMessageBuffer Serialize(SimpleServerMessageBuffer messageBuffer)
                {
                    messageBuffer.AppendInteger(TicketId);
                    messageBuffer.AppendInteger(Status);
                    messageBuffer.AppendInteger(Type); // type (3 or 4 for new style)
                    messageBuffer.AppendInteger(Category);
                    messageBuffer.AppendInteger((Yupi.GetUnixTimeStamp() - (int) Timestamp)*1000);
                    messageBuffer.AppendInteger(Score);
                    messageBuffer.AppendInteger(1);
                    messageBuffer.AppendInteger(SenderId);
                    messageBuffer.AppendString(_senderName);
                    messageBuffer.AppendInteger(ReportedId);
                    messageBuffer.AppendString(_reportedName);
                    messageBuffer.AppendInteger(Status == TicketStatus.Picked ? ModeratorId : 0);
                    messageBuffer.AppendString(_modName);
                    messageBuffer.AppendString(Message);
                    messageBuffer.AppendInteger(0);

                    messageBuffer.AppendInteger(ReportedChats.Count);

                    foreach (string str in ReportedChats)
                    {
                        messageBuffer.AppendString(str);
                        messageBuffer.AppendInteger(-1);
                        messageBuffer.AppendInteger(-1);
                    }

                    return messageBuffer;
                }*/

        #endregion Other
    }
}