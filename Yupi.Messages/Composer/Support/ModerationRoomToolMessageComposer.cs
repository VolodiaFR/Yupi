﻿using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms.Data.Models;

namespace Yupi.Messages.Support
{
	public class ModerationRoomToolMessageComposer : AbstractComposer<Yupi.Messages.Rooms>
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISender session, Yupi.Messages.Rooms room, uint roomId)
		{
			RoomData data;

			if (room == null) {
				data = Yupi.GetGame ().GetRoomManager ().GenerateNullableRoomData (roomId);
			} else {
				data = room.RoomData;
			}

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(data.Id);
				message.AppendInteger(data.UsersNow);

				if (room != null)
					message.AppendBool(room.GetRoomUserManager().GetRoomUserByHabbo(data.Owner) != null);
				else
					message.AppendBool(false);

				message.AppendInteger(room?.RoomData.OwnerId ?? 0);
				message.AppendString(data.Owner);
				message.AppendBool(room != null);
				message.AppendString(data.Name);
				message.AppendString(data.Description);
				message.AppendInteger(data.TagCount);

				foreach (string current in data.Tags)
					message.AppendString(current);

				message.AppendBool(false);

				session.Send (message);
			}
		}
	}
}
