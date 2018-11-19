# TestBth Sync
[![](https://upload.wikimedia.org/wikipedia/ru/d/d6/Sberbank.svg)](https://www.sberbank.ru/ru/person)  
Russian bank card: 4817 7601 3803 3687

Xamarin Test to connect a Bluetooth device (for Android OS) with a SPP interface in sync mode. Timeout is implement.
## Implement
* int TimeOut { get; set; }
* bool Open(string name);
* void DiscardOutBuffer();
* void DiscardInBuffer();
* void Write(byte[] b, int v, int length);
* byte ReadByte();
* void Close();
* ObservableCollection<string> PairedDevices();
## Build
Xamarin 3.3.0.912540 in VS2017
##Author
© 2018 Maksim Bartosh mksm.brtsh@gmail.com
Base on https://github.com/mksmbrtsh/TestBth
 ## Screenshots
![Screen1](https://raw.githubusercontent.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/master/photo_2018-11-19_15-46-25.jpg)<br>
![Screen2](https://github.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/blob/master/photo_2018-11-19_15-46-28.jpg?raw=true)<br>
![Screen3](https://github.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/blob/master/photo_2018-11-19_15-46-32.jpg?raw=true)<br>
![Screen4](https://github.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/blob/master/photo_2018-11-19_15-46-36.jpg?raw=true)<br>
![Screen5](https://github.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/blob/master/photo_2018-11-19_15-46-39.jpg?raw=true)<br>
![Screen6](https://github.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/blob/master/photo_2018-11-19_15-46-43.jpg?raw=true)<br>
![Screen7](https://github.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/blob/master/photo_2018-11-19_15-46-48.jpg?raw=true)<br>
![Screen8](https://github.com/mksmbrtsh/Sync-SPP-in-BT-for-Xamarin/blob/master/photo_2018-11-19_15-46-51.jpg?raw=true)<br>
