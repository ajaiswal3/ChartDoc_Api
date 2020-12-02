﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChartDoc.Radius
{
	public class TunnelMediumTypeAttribute : RadiusAttribute
	{
		private const byte TUNNEL_TYPE_LENGTH = 6;
		private const byte TUNNEL_TAG_INDEX = 2;
		private const int TUNNEL_TYPE_VALUE_INDEX = 3;
		private const int TUNNEL_TYPE_VALUE_LENGTH = 3;

		public byte Tag { get; private set; }
		public TunnelMediumType TunnelMediumType { get; private set; }

		public TunnelMediumTypeAttribute(byte tag, TunnelMediumType tunnelMediumType)
			: base(RadiusAttributeType.TUNNEL_TYPE)
		{
			Tag = tag;
			TunnelMediumType = tunnelMediumType;
			Data = Utils.IntTo3Byte((int)tunnelMediumType);

			Length = TUNNEL_TYPE_LENGTH;
			RawData = new byte[Length];

			RawData[0] = (byte)Type;
			RawData[1] = Length;

			RawData[TUNNEL_TAG_INDEX] = ((tag & 0xFF) == 0) ? (byte)0x00 : tag;

			Array.Copy(Utils.IntTo3Byte((int)tunnelMediumType), 0, RawData, TUNNEL_TYPE_VALUE_INDEX, TUNNEL_TYPE_VALUE_LENGTH);
		}
	}
}
