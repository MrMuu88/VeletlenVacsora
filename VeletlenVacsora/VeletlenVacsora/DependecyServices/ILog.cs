namespace VeletlenVacsora.DependecyServices {
	public interface ILog {
		void MakeLog(string Message, LogType logType = LogType.Info);
	}

	public enum LogType {Info,Warn,Error}
}
