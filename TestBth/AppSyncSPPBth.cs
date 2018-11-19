using System;

using Xamarin.Forms;

namespace TestBth
{
	public class AppSyncSPPBth : Application
	{
		public AppSyncSPPBth()
		{
			MainPage = new PageSyncSPPBtn();
		}

		protected override void OnStart ()
		{
		}

		protected override void OnSleep ()
		{
			MessagingCenter.Send<AppSyncSPPBth>(this, "Sleep"); // When app sleep, send a message so I can "Cancel" the connection
		}

		protected override void OnResume ()
		{
			MessagingCenter.Send<AppSyncSPPBth>(this, "Resume"); // When app resume, send a message so I can "Resume" the connection
		}
	}
}

