using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RoomEnterErrorMessageComposer : AbstractComposer<RoomEnterErrorMessageComposer.Error>
	{
		public enum Error {
			ROOM_FULL = 1,
			UNKNOWN = 4 // what does this mean?
		}

		public override void Compose(Yupi.Protocol.ISender session, Error error)
		{
		 // Do nothing by default.
		}
	}
}