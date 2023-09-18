using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Globalization;
namespace QuasarFramework.Systems
{
    public class TimeSync : ModSystem
    {

    }

    public class InternetTimeSync
    {
        private const int syncInterval = 60;
        private const int sleepTime = 1000;
        private const int tickSleepTime = 1000;

        private bool _cancel;   

        private DateTime _current;

        private DateTime _lastSynced;

        private DateTime _lastTickSync;

        public event ValueChangedEventHandler TimeChanged;

        private object _timeLock = new();

        private Task _syncMe;

        private Task _tickTask;

        public InternetTimeSync()
        {
            _syncMe = new Task(SyncAction);
            _tickTask = new Task(TickAction);
        }

        private void TickAction()
        {

        }

        private void SyncAction()
        {
            while (!_cancel)
            {
                DateTime now = DateTime.Now;

                double elapsed = (now - _lastSynced).TotalSeconds;

                if (elapsed > syncInterval)
                {
                    SetTime(GetTime.GetCurrentUTC());
                    _lastTickSync = _lastSynced = now;
                }

                Thread.Sleep(sleepTime);
            }
        }

        private void SetTime(DateTime time)
        {
            lock (_timeLock)
                _current = time;

            if (TimeChanged != null)
                TimeChanged.BeginInvoke(this, new ValueChangedEventArgs(_current), null, null);
        }
    }

    public class GetTime
    {
        private const int Iteration = 1000;

        private static readonly string[] serverList = new string[]
        {
            "64.90.182.55",
            "96.47.67.105",
            "206.246.122.250"
        };

        private static string GetTimeString(string serverName)
        {
            string result = string.Empty;

            try
            {
                TcpClient client = new();
                client.Connect(serverName, 13);

                Stream stream = client.GetStream();
                int Buffer = 100;
                byte[] buffer = new byte[Buffer];
                int readCount;

                while ((readCount = stream.Read(buffer, 0, Buffer)) > 0)
                    result += Encoding.ASCII.GetString(buffer, 0, readCount);

                stream.Dispose();
                client.Close();
            }

            catch
            {
                result = null;
            }

            return result;
        }

        private static DateTime Parser(string dateTimeString)
        {
            try
            {
                dateTimeString = dateTimeString.Trim();

                string[] dateTimeParts = dateTimeString.Split(' ');

                DateTime result = 
                    (dateTimeParts[5] == "0") ? 
                    DateTime.ParseExact(dateTimeParts[1] + ' ' + dateTimeParts[2], "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) : 
                    DateTime.MinValue;

                return result;
            }

            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime GetCurrentUTC()
        {
            string dateTime = null;

            int index = 0;

            int iteration = 0;

            DateTime typedTime = DateTime.MinValue;

            do
            {
                do
                {
                    dateTime = GetTimeString(serverList[index]);
                    index++;
                    iteration++;

                    if (index >= serverList.Length)
                        index = 0;
                }

                while (string.IsNullOrEmpty(dateTime) && iteration < Iteration);

                typedTime = Parser(dateTime);
            }
            while (typedTime == DateTime.MinValue && iteration < Iteration);

            return typedTime;
        }
    }

    public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs args);
    public class ValueChangedEventArgs : EventArgs
    {
        private DateTime _value;

        public DateTime Value
        {
            get
            {
                return _value;
            }
        }

        public ValueChangedEventArgs(DateTime Value)
        {
            _value = Value;
        }
    }
}