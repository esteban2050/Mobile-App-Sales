namespace Sales.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<Ilocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<Ilocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string InternetSettings
        {
            get { return Resource.InternetSettings; }
        }

        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }        
    }
}
