﻿using System;

using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.User
{
	public class RelationshipsGetMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public RelationshipsGetMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int userId = message.GetInteger();

			UserInfo user = UserRepository.FindBy (userId);

			if (user == null)
				return;

			router.GetComposer<RelationshipMessageComposer> ().Compose (session, user);
		}
	}
}

