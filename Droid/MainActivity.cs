
using Android.App;
using Android.Bluetooth;
using Android.Content.PM;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace TestBth.Droid
{
	[Activity (Label = "TestBth.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
        private PageSyncSPPBtn mp;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            AppSyncSPPBth a;
			LoadApplication (a = new AppSyncSPPBth());
            mp = ((PageSyncSPPBtn)a.MainPage);
            mp.ButtonSend.Clicked += ButtonSend_Clicked;
            

        }

        private void ButtonSend_Clicked(object sender, System.EventArgs e1)
        {
            int timeout = DependencyService.Get<ISyncSPPBth>().TimeOut;
            try
            {
                byte[] msg = System.Text.ASCIIEncoding.ASCII.GetBytes(mp.MessageToDevice.Text.ToString() + "\r\n");
                DependencyService.Get<ISyncSPPBth>().Write(msg, 0, msg.Length);
                ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": write \"" + mp.MessageToDevice.Text.ToString() + "\"");
            }
            catch (TimeoutException e)
            {
                ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortTimeString() + ": " +  e.ToString());
                return;
            }
            catch (InvalidOperationException e)
            {
                ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortTimeString() + ": " + e.ToString());
                return;
            }
            catch (IOException e)
            {
                ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortTimeString() + ": " + e.ToString());
                return;
            }
            catch
            {
                ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + DateTime.Now.ToShortTimeString() + ": " + "error");
                return;
            }

            List<byte> buf = new List<byte>();
            bool flag_timeout = false;
            StringBuilder result = new StringBuilder("");
            do
            {
                byte b = 0x00;
                try
                {
                    b = DependencyService.Get<ISyncSPPBth>().ReadByte();
                }
                catch (TimeoutException te)
                {
                    flag_timeout = true;
                }
                catch (InvalidOperationException ioe)
                {
                    ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": " +  ioe.ToString());
                    break;
                }
                catch (EndOfStreamException ese)
                {
                    ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": " + ese.ToString());
                    break;
                }
                catch (IOException e)
                {
                    ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": " + e.ToString());
                    break;
                }
                catch
                {
                    ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": " + "error");
                    break;
                }
                DependencyService.Get<ISyncSPPBth>().TimeOut = 300;
                if(!flag_timeout)
                    buf.Add(b);
                else
                {
                    byte[] bytes = buf.ToArray();
                    string ans = ASCIIEncoding.ASCII.GetString(bytes, 0, bytes.Length);
                    ans = ans.Trim();
                    if (ans != "")
                    {
                        result.Append(ans);
                        result.Append("\r\n");
                    }
                    ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": read \"" + result.ToString() +"\"");
                    try
                    {
                        DependencyService.Get<ISyncSPPBth>().TimeOut = timeout;
                    }
                    catch (IOException e)
                    {
                        ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": " + e.ToString());
                    }
                    catch
                    {
                        ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": error");
                    }
                    return;
                }
            } while (true);
            try
            {
                DependencyService.Get<ISyncSPPBth>().DiscardOutBuffer();
                DependencyService.Get<ISyncSPPBth>().DiscardInBuffer();
            }
            catch (IOException)
            {

            }

            catch (InvalidOperationException)
            {

            }
            catch
            {

            }
            try
            {
                DependencyService.Get<ISyncSPPBth>().TimeOut = timeout;
            }
            catch (IOException)
            {

            }
            catch
            {

            }
            if(result.Length >0)
                ((ObservableCollection<string>)mp.MsgLog.ItemsSource).Insert(0, DateTime.Now.ToShortTimeString() + ": " + result.ToString());
        }
    }
}

