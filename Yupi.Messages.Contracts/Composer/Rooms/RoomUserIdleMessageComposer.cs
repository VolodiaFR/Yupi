using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomUserIdleMessageComposer : AbstractComposer<int, bool>
	{
		public override void Compose(Yupi.Protocol.ISender room, int entityId, bool isAsleep)
		{
		 // Do nothing by default.
		}
	}
}
