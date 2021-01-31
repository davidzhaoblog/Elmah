using System;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public class Countdown : BindableObject
    {
        TimeSpan _remainTime;

        public event Action Completed;
        public event Action Ticked;

        public DateTime EndDate { get; set; }

        public TimeSpan RemainTime
        {
            get { return _remainTime; }

            private set
            {
                _remainTime = value;
                OnPropertyChanged();
            }
        }

        public void Start(DateTime endTime, int tickSeconds = 1)
        {
            EndDate = endTime;
            Start(tickSeconds);
        }

        public void Start(int duration, int tickSeconds = 1)
        {
            EndDate = DateTime.Now.AddSeconds(duration);
            Start(tickSeconds);
        }

        private void Start(int tickSeconds = 1)
        {
            Device.StartTimer(TimeSpan.FromSeconds(tickSeconds), () =>
            {
                RemainTime = (EndDate - DateTime.Now);

                var ticked = RemainTime.TotalSeconds > 1;

                if (ticked)
                {
                    Ticked?.Invoke();
                }
                else
                {
                    Completed?.Invoke();
                }

                return ticked;
            });
        }
    }
}

