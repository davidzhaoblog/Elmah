using Xamarin.Forms;

namespace Framework.Xaml
{
    public class ProgressBarVM: _ViewModelBase
    {
        private Color m_ProgressColor;
        public Color ProgressColor
        {
            get { return m_ProgressColor; }
            set
            {
                Set(nameof(ProgressColor), ref m_ProgressColor, value);
            }
        }

        private double m_Progress;
        public double Progress
        {
            get { return m_Progress; }
            set
            {
                Set(nameof(Progress), ref m_Progress, value);
            }
        }

        private double m_Scale;
        public double Scale
        {
            get { return m_Scale; }
            set
            {
                Set(nameof(Scale), ref m_Scale, value);
            }
        }

        public void Initialization(Color progressColor, double scale)
        {
            ProgressColor = progressColor;
            Scale = scale;
        }

        public void Go(double progress)
        {
            Progress = progress;
        }
        public void Forward()
        {
            Progress += 1;
        }
        public void Backward()
        {
            Progress -= 1;
        }
    }
}

