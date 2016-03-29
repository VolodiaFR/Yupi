/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System.Net;
using DotNetty.Transport.Channels;

namespace Yupi.Net.DotNettyImpl
{
	// TODO Delete
    /// <summary>
    ///     Class ConnectionActor.
    /// </summary>
    public class ConnectionActorDELETE
	{
        /// <summary>
        ///     Connection Id
        /// </summary>
        public string ConnectionId;

        /// <summary>
        ///     IP Address
        /// </summary>
        public string IpAddress;

        /// <summary>
        ///    Connection Channel
        /// </summary>
        public IChannel ConnectionChannel;


        /// <summary>
        ///     Count of Same IP Connection.
        /// </summary>
        internal int SameHandledCount;

		public ConnectionActorDELETE(IChannel context)
        {
            ConnectionChannel = context;

            ConnectionId = context.Id.ToString();

            IpAddress = (context.RemoteAddress as IPEndPoint)?.Address.ToString();

            SameHandledCount = 0;
        }

        public void Close()
        {
            ConnectionChannel?.Flush();
        }

        public void CompleteClose()
        {
            ConnectionChannel?.CloseAsync();

            Close();
        }
    }
}