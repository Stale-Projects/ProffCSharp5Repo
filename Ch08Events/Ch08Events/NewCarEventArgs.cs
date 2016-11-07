using System;

namespace Ch08Events
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
