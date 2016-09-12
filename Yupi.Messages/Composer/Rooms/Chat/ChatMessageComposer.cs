﻿using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Util;
using Yupi.Messages.Encoders;


namespace Yupi.Messages.Chat
{
	public class ChatMessageComposer : Yupi.Messages.Contracts.ChatMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, ChatMessage msg, int count = -1)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.Append (msg, count);
				session.Send (message);
			}
		}
	}
}

