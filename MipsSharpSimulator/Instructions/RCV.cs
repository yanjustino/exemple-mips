﻿using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MipsSharpSimulator
{
	public class RCV : Instruction
	{
		public RCV (string label, string line):base(label, line)
		{
		}

		public override void Process ()
		{
			var valorIp = Convert.ToInt32(RegisterRepository.Current.Get (Parameters [1]));
			var ip = NetworkIdsRepository.Current.Get (valorIp);

			while (true) {
				
				if (SocketMessageRepository.Current.HasValue (ip)) {
					var value = SocketMessageRepository.Current.Get (ip);
					var size = Convert.ToInt32 (Parameters [2]);
					var address = Convert.ToInt32 (RegisterRepository.Current.Get (Parameters [3]));

					DataSegmentRepository.Current.SetByte (address, value, size);

					SocketMessageRepository.Current.Remove (ip);
					break;
				}

			}
		}
	}
}
