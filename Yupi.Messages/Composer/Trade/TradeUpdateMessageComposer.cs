﻿// ---------------------------------------------------------------------------------
// <copyright file="TradeUpdateMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Trade
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class TradeUpdateMessageComposer : Yupi.Messages.Contracts.TradeUpdateMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, TradeUser first, TradeUser second)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                Serialize(first, message);
                Serialize(second, message);
                session.Send(message);
            }
        }

        private void Serialize(TradeUser user, ServerMessage message)
        {
            message.AppendInteger(user.User.Id);
            message.AppendInteger(user.OfferedItems.Count);

            foreach (UserItem current in user.OfferedItems)
            {
                message.AppendInteger(current.Id);
                /*
                message.AppendString(current.BaseItem.Type.ToString().ToLower());
                message.AppendInteger(current.Id);
                message.AppendInteger(current.BaseItem.SpriteId);
                message.AppendInteger(0);
                message.AppendBool(true);
                message.AppendInteger(0);
                message.AppendString(string.Empty);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);

                if (current.BaseItem.Type == 's')
                    message.AppendInteger(0);
                    */
                throw new NotImplementedException();
            }
        }

        #endregion Methods
    }
}