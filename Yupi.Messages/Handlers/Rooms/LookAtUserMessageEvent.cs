﻿using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Game.Pathfinding;

namespace Yupi.Messages.Rooms
{
	public class LookAtUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			roomUserByHabbo.UnIdle();

			int x = request.GetInteger();
			int y = request.GetInteger();

			if (x == roomUserByHabbo.X && y == roomUserByHabbo.Y)
				return;

			int rotation = PathFinder.CalculateRotation(roomUserByHabbo.X, roomUserByHabbo.Y, x, y);

			roomUserByHabbo.SetRot(rotation, false);
			roomUserByHabbo.UpdateNeeded = true;

			if (!roomUserByHabbo.RidingHorse)
				return;

			RoomUser roomUserByVirtualId = session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(roomUserByHabbo.HorseId));

			roomUserByVirtualId.SetRot(rotation, false);
			roomUserByVirtualId.UpdateNeeded = true;
		}
	}
}

