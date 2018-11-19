using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using PropertyChanged;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TestBth
{
//	[ImplementPropertyChanged]
	public class PageSyncSPPBtnViewModel : INotifyPropertyChanged
	{

		public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> ListOfBarcodes { get; set; } = new ObservableCollection<string>();
		public string SelectedBthDevice { get; set; } = "";
		bool _isConnected { get; set; } = false;

		private bool _isSelectedBthDevice { get {
				if (string.IsNullOrEmpty(SelectedBthDevice)) return false; return true;
			} 
		}

		public bool IsConnectEnabled { get {
				if (_isSelectedBthDevice == false)
					return false;
				return !_isConnected;
			} 
		}
		
		public bool IsDisconnectEnabled { 
			get {
				if (_isSelectedBthDevice == false)
					return false;
				return _isConnected;
			}
		}

		public bool IsPickerEnabled { 
			get {
				return !_isConnected;
			}
		}

		public PageSyncSPPBtnViewModel()
		{

			this.ConnectCommand = new Command(() => {
			
				// Try to connect to a bth device
				DependencyService.Get<ISyncSPPBth>().Open(SelectedBthDevice);
				_isConnected = true;
			});

			this.DisconnectCommand = new Command(() => { 

				// Disconnect from bth device
				DependencyService.Get<ISyncSPPBth>().Close();
				_isConnected = false;
			});

            try
            {
				// At startup, I load all paired devices
				ListOfDevices = DependencyService.Get<ISyncSPPBth>().PairedDevices();
			}
			catch (Exception ex)
			{
				Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
			}
		}

		public ICommand ConnectCommand { get; protected set;}
		public ICommand DisconnectCommand { get; protected set;}
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
