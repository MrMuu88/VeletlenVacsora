using Xamarin.Forms;
using VeletlenVacsora.DependecyServices;
using Android.Util;

[assembly: Dependency(typeof(VeletlenVacsora.Droid.Loger))]
namespace VeletlenVacsora.Droid {
	public class Loger :ILog {
		const string tag = "VeletlenVacsora";

		public void MakeLog(string Message, LogType logType = LogType.Info) {
			switch (logType) {
				case LogType.Info:
					Log.Info(tag, Message);
					break;
				case LogType.Warn:
					Log.Warn(tag, Message);
					break;
				case LogType.Error:
					Log.Error(tag, Message);
					break;
				default:
					Log.Error(tag, $"Unkonwn Logtype: {logType}");
					Log.Info(tag, Message);
					break;
			}
		}
	}
}