﻿using System;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorDeleteSavedSearchEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int searchId = request.GetInteger();

			if (!session.GetHabbo().NavigatorLogs.ContainsKey(searchId))
				return;

			session.GetHabbo().NavigatorLogs.Remove(searchId);

			NavigatorSavedSearchesComposer.Compose(session);
		}
	}
}
