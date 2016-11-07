using System.Windows;


namespace Ch08WeakEvent
{
    //Esta clase maneja los weak events
    class NewCarWeakEventManager : WeakEventManager
    {
        //Primero la implementación de Singleton
        public static NewCarWeakEventManager Instance
        {
            get
            {
                var _instance = GetCurrentManager(typeof(NewCarWeakEventManager)) as NewCarWeakEventManager;
                if (_instance == null)
                {
                    _instance = new NewCarWeakEventManager();
                    SetCurrentManager(typeof(NewCarWeakEventManager), _instance);
                }
                return _instance;
            }
        }

        public static void AddListener(object source, IWeakEventListener listener)
        {
            Instance.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(object source, IWeakEventListener listener)
        {
            Instance.ProtectedRemoveListener(source, listener);
        }

        void CarDealer_NewCar(object sender, NewCarEventArgs e)
        {
            DeliverEvent(sender, e);
        }
        protected override void StartListening(object source)
        {
            (source as CarDealer).NewCarEvent += CarDealer_NewCar;

        }

        protected override void StopListening(object source)
        {
            (source as CarDealer).NewCarEvent -= CarDealer_NewCar;
        }



    }
}
