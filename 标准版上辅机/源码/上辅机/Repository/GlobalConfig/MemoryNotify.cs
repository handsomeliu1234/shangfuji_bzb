using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Repository.GlobalConfig
{
    public class MemoryNotify : INotifyPropertyChanged
    {
        public int Number
        {
            get; set;
        }

        public int Address
        {
            get; set;
        }

        /// <summary>
        /// 称量重量的地址
        /// </summary>
        public int DataAddress
        {
            get; set;
        }

        /// <summary>
        /// 实际车数的地址
        /// </summary>
        public int BatchAddress
        {
            get; set;
        }

        public string DevicePartCode
        {
            get; set;
        }

        public double DanWei
        {
            get; set;
        }

        private int value;

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}