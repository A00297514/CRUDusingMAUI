using AuthenticationApp.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace AuthenticationApp
{
    public partial class MainPage : ContentPage
    {
        private readonly FirebaseClient _firebaseClient;
        public ObservableCollection<Hotel> MyHotelList { get; set; } = new ObservableCollection<Hotel>();
        int count = 0;
        

        public MainPage(FirebaseClient firebaseClient)
        {
            InitializeComponent();
            BindingContext = this;
            _firebaseClient = firebaseClient;
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
           await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            var result = _firebaseClient.Child("Hotel").AsObservable<Hotel>().Subscribe((item) =>
            {
                if (item.Object != null)
                {
                    MyHotelList.Add(item.Object);
                }
            });
        }
        private void OnSave(object sender, EventArgs e)
        {
            _firebaseClient.Child("Hotel").PostAsync(new Hotel
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,

            });

            NameEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
        }
    }

}
