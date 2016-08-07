﻿using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
	public class UserUpdateNameInRoomMessageComposer : Yupi.Messages.Contracts.UserUpdateNameInRoomMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender room, UserInfo habbo, string newName)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (habbo.Id);
				message.AppendInteger (habbo.CurrentRoom.RoomId);
				message.AppendString (newName);
				room.Send (message);
			}
		}
	}
}
