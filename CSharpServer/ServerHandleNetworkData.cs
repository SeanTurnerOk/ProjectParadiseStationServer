using System;
using System.Collections.Generic;
using Bindings;

namespace CSharpServer
{
    class ServerHandleNetworkData
    {
        private delegate void Packet_(int index, byte[] data);
        private static Dictionary<int, Packet_> Packets;

        public static void InitializeNetworkPackages()
        {
            Console.WriteLine("Initialize Network Packages.");
            Packets = new Dictionary<int, Packet_>
            {
                {(int) NetworkPackets.ClientPackets.CThankYou, HandleThankYou }
            };
        }
        private static void HandleConnectionOK(byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInteger();
            string msg = buffer.ReadString();
            buffer.Dispose();
            //ADD CODE TO EXECUTE HERE
            Console.WriteLine(msg);
        }
        public static void HandleNetworkInformation(int index, byte[] data)
        {
            int packetNum;
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            packetNum = buffer.ReadInteger();
            buffer.Dispose();
            if (Packets.TryGetValue(packetNum, out Packet_ Packet))
            {
                Packet.Invoke(index, data);
            }

        }
        private static void HandleThankYou(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInteger();
            string msg = buffer.ReadString();
            buffer.Dispose();
            //Add Code to execute here
            Console.WriteLine(msg);
        }
    }
}
