using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace TestBth
{
    public class PageSyncSPPBtn : ContentPage
    {
        public Button ButtonSend { get; private set; }
        public Entry MessageToDevice { get; private set; }
        public ListView MsgLog { get; private set; }

        public PageSyncSPPBtn()
        {

            this.BindingContext = new PageSyncSPPBtnViewModel();

            Picker pickerBluetoothDevices = new Picker() { Title = "Select a bth device" };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SetBinding(Picker.SelectedItemProperty, "SelectedBthDevice");
            pickerBluetoothDevices.SetBinding(VisualElement.IsEnabledProperty, "IsPickerEnabled");

            MessageToDevice = new Entry() { Keyboard = Keyboard.Text, Placeholder = "" };
            MessageToDevice.SetBinding(Entry.TextProperty, "MessageToDevice");

            Button buttonConnect = new Button() { Text = "Connect" };
            buttonConnect.SetBinding(Button.CommandProperty, "ConnectCommand");
            buttonConnect.SetBinding(VisualElement.IsEnabledProperty, "IsConnectEnabled");

            Button buttonDisconnect = new Button() { Text = "Disconnect" };
            buttonDisconnect.SetBinding(Button.CommandProperty, "DisconnectCommand");
            buttonDisconnect.SetBinding(VisualElement.IsEnabledProperty, "IsDisconnectEnabled");
            ButtonSend = new Button() { Text = "Send" };
            buttonDisconnect.SetBinding(VisualElement.IsEnabledProperty, "IsDisconnectEnabled");
            StackLayout slButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { buttonDisconnect, buttonConnect, ButtonSend } };

            MsgLog = new ListView();
            MsgLog.SetBinding(ListView.ItemsSourceProperty, "ListOfAnswers");
            MsgLog.ItemTemplate = new DataTemplate(typeof(TextCell));
            MsgLog.ItemTemplate.SetBinding(TextCell.TextProperty, ".");
            MsgLog.ItemsSource = new ObservableCollection<string>();
            StackLayout sl = new StackLayout { Children = { pickerBluetoothDevices, MessageToDevice, slButtons, MsgLog }, Padding = new Thickness(0, 0, 0, 0) };
            Content = sl;
        }
    }
}

