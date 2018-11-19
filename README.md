# TestBth Sync
Xamarin Test to connect a Bluetooth device (for Android OS) with a SPP interface.
## Implement
int TimeOut { get; set; }// timeout
bool Open(string name);// connect to name device
void DiscardOutBuffer();// flush
void DiscardInBuffer();// flush
void Write(byte[] b, int v, int length);// send to device
byte ReadByte();// read from device
void Close();// disconnect
ObservableCollection<string> PairedDevices();// get list of devices
## Build
Xamarin 3.3.0.912540 in VS2017
##Author
© 2018 Maksim Bartosh mksm.brtsh@gmail.com
Base on https://github.com/mksmbrtsh/TestBth