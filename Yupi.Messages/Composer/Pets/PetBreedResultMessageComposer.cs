﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
	public class PetBreedResultMessageComposer : Yupi.Messages.Contracts.PetBreedResultMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int petId, int randomValue)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (petId);
				message.AppendInteger (randomValue);
				session.Send (message);
			}
		}
	}
}
