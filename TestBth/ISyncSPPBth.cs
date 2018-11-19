using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestBth
{
	public interface ISyncSPPBth
	{
        int TimeOut { get; set; }// timeout
        bool Open(string name);// connect to name device
        void DiscardOutBuffer();// flush
        void DiscardInBuffer();// flush
        void Write(byte[] b, int v, int length);// send to device
        byte ReadByte();// read from device
        void Close();// disconnect
		ObservableCollection<string> PairedDevices();// get list of devices
	}
}

