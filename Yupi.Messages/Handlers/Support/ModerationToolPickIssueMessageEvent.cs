﻿// ---------------------------------------------------------------------------------
// <copyright file="ModerationToolPickIssueMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Support
{
    using System;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ModerationToolPickIssueMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<SupportTicket> TicketRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolPickIssueMessageEvent()
        {
            TicketRepository = DependencyFactory.Resolve<IRepository<SupportTicket>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            message.GetInteger(); // TODO Unused
            int ticketId = message.GetInteger();

            SupportTicket ticket = TicketRepository.Find(ticketId);

            if (ticket == null || ticket.Status != TicketStatus.Closed)
            {
                return;
            }

            ticket.Pick(session.Info);

            TicketRepository.Save(ticket);

            foreach (Habbo staff in ClientManager.GetByPermission("handle_cfh"))
            {
                staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, ticket);
            }
        }

        #endregion Methods
    }
}