using System;
using Android.Bluetooth;
using Java.Util;
using System.Threading.Tasks;
using Java.IO;
using TestBth.Droid;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Runtime;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency (typeof (aSyncSPPBth))]
namespace TestBth.Droid
{
	public class aSyncSPPBth : ISyncSPPBth
	{
        Stopwatch s = new Stopwatch();
        private CancellationTokenSource _ct { get; set; }
        public bool IsOpen { get { return BthSocket.IsConnected; } }
        private int timeout;
        public int TimeOut { get => timeout; set => timeout = value; }

        BluetoothSocket BthSocket = null;

        public aSyncSPPBth()
        {
            TimeOut = 5000;
        }

        public bool Open(string name)
        {
            BluetoothDevice device = null;
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;

            adapter = BluetoothAdapter.DefaultAdapter;

            if (adapter == null)
                return false;

            if (!adapter.IsEnabled)
                return false;
            foreach (var bd in adapter.BondedDevices)
            {
                System.Diagnostics.Debug.WriteLine("Paired devices found: " + bd.Name.ToUpper());
                if (bd.Name.ToUpper().IndexOf(name.ToUpper()) >= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Found " + bd.Name + ". Try to connect with it!");
                    device = bd;
                    break;
                }
            }

            if (device == null)
                return false;
            else
            {
                UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");
                if ((int)Android.OS.Build.VERSION.SdkInt >= 10) // Gingerbread 2.3.3 2.3.4
                    BthSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);
                else
                    BthSocket = device.CreateRfcommSocketToServiceRecord(uuid);
                if (BthSocket == null)
                    return false;
                try
                {
                    BthSocket.Connect();
                }
                catch
                {
                    return false;
                }
                return true;
            }


        }

        public void Close()
        {
            if (BthSocket != null)
                BthSocket.Close();
        }

        public ObservableCollection<string> PairedDevices()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            ObservableCollection<string> devices = new ObservableCollection<string>();

            foreach (var bd in adapter.BondedDevices)
                devices.Add(bd.Name);

            return devices;
        }

        public void DiscardOutBuffer()
        {
            BthSocket.OutputStream.Flush();
        }

        public void DiscardInBuffer()
        {
            BthSocket.InputStream.Flush();
        }

        public void Write(byte[] b, int v, int length)
        {
            BthSocket.OutputStream.Write(b, v, length);
        }
        
        public byte ReadByte()
        {
            s.Restart();
            do
            {
                if (BthSocket.InputStream.CanRead && (BthSocket.InputStream as InputStreamInvoker).BaseInputStream.Available() > 0)
                    return (byte)BthSocket.InputStream.ReadByte();
                if (s.ElapsedMilliseconds > timeout)
                {
                    throw new TimeoutException();
                }
                Thread.Sleep(100);

            } while (true);
        }
    }
}