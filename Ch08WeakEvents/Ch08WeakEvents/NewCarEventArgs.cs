using System;

namespace Ch08WeakEvents
{
    class NewCarEventArgs : EventArgs
    {
        public string CarName { get; private set; }

        public NewCarEventArgs(string carName)
        {
            this.CarName = carName;
        }
    }
}
