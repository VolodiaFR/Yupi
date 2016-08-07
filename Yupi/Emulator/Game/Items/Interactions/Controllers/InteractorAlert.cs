using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorAlert : FurniInteractorModel
    {
        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.ExtraData = "0";
            item.UpdateNeeded = true;
        }

        public override void OnRemove(GameClient session, RoomItem item) => item.ExtraData = "0";

        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            if (item.ExtraData != "0")
                return;

            item.ExtraData = "1";
            item.UpdateState(false, true);
            item.ReqUpdate(4, true);
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            if (item.ExtraData != "0")
                return;

            item.ExtraData = "1";
            item.UpdateState(false, true);
            item.ReqUpdate(4, true);
        }
    }
}