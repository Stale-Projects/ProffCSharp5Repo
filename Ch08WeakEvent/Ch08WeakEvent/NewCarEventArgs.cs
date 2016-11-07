using System;

namespace Ch08WeakEvent
{
    class NewCarEventArgs : EventArgs
    {
        public string CarName { get; private set; }

        public NewCarEventArgs(string carName)
        {
            CarName = carName;
        }
    }
}
