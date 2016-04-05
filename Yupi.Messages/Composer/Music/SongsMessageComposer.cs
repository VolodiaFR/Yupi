﻿using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.SoundMachine.Songs;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
	public class SongsMessageComposer : AbstractComposer<List<SongData>>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, List<SongData> songs)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (songs.Count);


				foreach (SongData current in songs)
				{
					message.AppendInteger(current.Id);
					message.AppendString(current.CodeName);
					message.AppendString(current.Name);
					message.AppendString(current.Data);
					message.AppendInteger(current.LengthMiliseconds);
					message.AppendString(current.Artist);
				}

				session.Send (message);
			}
		}
	}
}

