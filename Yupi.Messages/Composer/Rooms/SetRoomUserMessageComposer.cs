﻿// ---------------------------------------------------------------------------------
// <copyright file="SetRoomUserMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Yupi.Messages.Encoders;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class SetRoomUserMessageComposer : Yupi.Messages.Contracts.SetRoomUserMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, IList<RoomEntity> users)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(users.Count);

                foreach (RoomEntity user in users)
                {
                    Serialize(message, user);
                }

                room.Send(message);
            }
        }

        public override void Compose(Yupi.Protocol.ISender room, RoomEntity user)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                Serialize(message, user);
                room.Send(message);
            }
        }

        // TODO Refactor
        private void Serialize(ServerMessage messageBuffer, RoomEntity entity)
        {
            if (entity is UserEntity)
            {
                Serialize(messageBuffer, (UserEntity) entity);
            }
            else if (entity is PetEntity)
            {
                Serialize(messageBuffer, (PetEntity) entity);
            }
            else if (entity is BotEntity)
            {
                Serialize(messageBuffer, (BotEntity) entity);
            }
        }

        private void Serialize(ServerMessage messageBuffer, UserEntity user)
        {
            Group group = user.UserInfo.FavouriteGroup;

            messageBuffer.AppendInteger(user.UserInfo.Id);
            messageBuffer.AppendString(user.UserInfo.Name);
            messageBuffer.AppendString(user.UserInfo.Motto);
            messageBuffer.AppendString(user.UserInfo.Look);
            messageBuffer.AppendInteger(user.Id);
            messageBuffer.Append(user.Position);
            messageBuffer.AppendInteger(0); // loc12
            messageBuffer.AppendInteger(1); // loc13 (= Type)

            // if loc13 == 1 {
            messageBuffer.AppendString(user.UserInfo.Gender); // _loc14_.§_-5YY§
            messageBuffer.AppendInteger(user.UserInfo.FavouriteGroup?.Id ?? 0); // _loc14_.§_-1tV§
            messageBuffer.AppendInteger(0); // _loc14_.§_-1xu§
            messageBuffer.AppendString(user.UserInfo.FavouriteGroup?.Name ?? string.Empty); // _loc14_.groupName
            messageBuffer.AppendString(""); // loc15
            messageBuffer.AppendInteger(user.UserInfo.Wallet.AchievementPoints); // _loc14_.§_-5DP§
            messageBuffer.AppendBool(false); // _loc14_.§_-32l§
            // }
        }

        private void Serialize(ServerMessage messageBuffer, PetEntity pet)
        {
            messageBuffer.AppendInteger(pet.Id);
            messageBuffer.AppendString(pet.Info.Name);
            messageBuffer.AppendString(pet.Info.Motto);
            /*
            if (pet.Info.Type == "pet_monster")
                messageBuffer.AppendString (pet.Info.MoplaBreed.PlantData);
            else if (pet.Info.HaveSaddle == Convert.ToBoolean (2))
                messageBuffer.AppendString (string.Concat (pet.Info.Look.ToLower (), " 3 4 10 0 2 ", pet.Info.PetHair, " ",
                    pet.Info.HairDye, " 3 ", pet.Info.PetHair, " ", pet.Info.HairDye));
            else if (pet.Info.HaveSaddle == Convert.ToBoolean (1))
                messageBuffer.AppendString (string.Concat (pet.Info.Look.ToLower (), " 3 2 ", pet.Info.PetHair, " ",
                    pet.Info.HairDye, " 3 ", pet.Info.PetHair, " ", pet.Info.HairDye, " 4 9 0"));
            else
                messageBuffer.AppendString (string.Concat (pet.Info.Look.ToLower (), " 2 2 ", pet.Info.PetHair, " ",
                    pet.Info.HairDye, " 3 ", pet.Info.PetHair, " ", pet.Info.HairDye));

            messageBuffer.AppendInteger (pet.Id);
            messageBuffer.AppendInteger (pet.Position.X);
            messageBuffer.AppendInteger (pet.Position.Y);
            messageBuffer.AppendString (pet.Position.Z.ToString (CultureInfo.InvariantCulture));
            messageBuffer.AppendInteger (0);
            messageBuffer.AppendInteger (pet.Type);
            messageBuffer.AppendInteger (pet.Info.RaceId);
            messageBuffer.AppendInteger (pet.Info.Owner.Id);
            messageBuffer.AppendString (pet.Info.Owner.Name);
            messageBuffer.AppendInteger (pet.Info.Type == "pet_monster" ? 0 : 1);
            messageBuffer.AppendBool (pet.Info.HaveSaddle);
            messageBuffer.AppendBool (pet.RidingHorse);
            messageBuffer.AppendInteger (0);
            messageBuffer.AppendInteger (pet.Info.Type == "pet_monster" ? 1 : 0);
            messageBuffer.AppendString (pet.Info.Type == "pet_monster" ? pet.Info.MoplaBreed.GrowStatus : "");
            */
            throw new NotImplementedException();
        }

        private void Serialize(ServerMessage messageBuffer, BotEntity bot)
        {
            messageBuffer.AppendInteger(bot.Id);
            messageBuffer.AppendString(bot.Info.Name);
            messageBuffer.AppendString(bot.Info.Motto);
            messageBuffer.AppendString(bot.Info.Look.Look);
            messageBuffer.AppendInteger(bot.Id);
            messageBuffer.Append(bot.Position);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger((int) bot.Type);
            messageBuffer.AppendChar((char)bot.Info.Look.Gender);
            messageBuffer.AppendInteger(bot.Info.Owner.Id);
            messageBuffer.AppendString(bot.Info.Owner.Name);
            messageBuffer.AppendInteger(5);
            messageBuffer.AppendShort(1);
            messageBuffer.AppendShort(2);
            messageBuffer.AppendShort(3);
            messageBuffer.AppendShort(4);
            messageBuffer.AppendShort(5);
        }

        #endregion Methods
    }
}