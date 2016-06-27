﻿using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.SoundMachine;
using Yupi.Emulator.Game.SoundMachine.Songs;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Music
{
	public class JukeboxRemoveSongMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (session.GetHabbo().CurrentRoom == null)
				return;

			Yupi.Messages.Rooms currentRoom = session.GetHabbo().CurrentRoom;

			if (!currentRoom.GotMusicController())
				return;

			SoundMachineManager roomMusicController = currentRoom.GetRoomMusicController();

			SongItem songItem = roomMusicController.RemoveDisk(message.GetInteger());

			if (songItem == null)
				return;

			songItem.RemoveFromDatabase();

			session.GetHabbo()
				.GetInventoryComponent()
				.AddNewItem(songItem.ItemId, songItem.BaseItem.Name, songItem.ExtraData, 0u, false, true, 0, 0,
					songItem.SongCode);
			session.GetHabbo().GetInventoryComponent().UpdateItems(false);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				queryReactor.RunFastQuery(
					$"UPDATE items_rooms SET user_id='{session.GetHabbo().Id}' WHERE id='{songItem.ItemId}' LIMIT 1;");

			router.GetComposer<SongsLibraryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ().SongDisks);
			router.GetComposer<JukeboxPlaylistMessageComposer> ().Compose (session, roomMusicController);
		}
	}
}

