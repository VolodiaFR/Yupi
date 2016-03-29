﻿/**
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

using System;

namespace Yupi.Net
{
	public class ServerSettings : IServerSettings
	{
		public string IP { get; set; }

		public int Port { get; set; }

		public int MaxWorkingThreads { get; set; } = 0;

		public int MinWorkingThreads { get; set; } = 0;

		public int MinIOThreads { get; set; } = 0;

		public int MaxIOThreads { get; set; } = 0;

		public int BufferSize { get; set; } = 4096;

		public int Backlog { get; set; } = 100;

		public int MaxConnections { get; set; } = 1000;
	}
}

