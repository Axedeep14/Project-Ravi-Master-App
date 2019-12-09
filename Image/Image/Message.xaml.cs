using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Image
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        public class ItemClass
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Contact { get; set; }
            public string Age { get; set; }
            public string Text { get; set; }

        }
        public Message()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            LoadData();
        }

        public async void LoadData()
        {
            var content="";
            HttpClient client = new HttpClient();
            var RestURL = "http://ravi-s-mishra.herokuapp.com/getmessage";
            client.BaseAddress = new Uri(RestURL);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(RestURL);
            content = await response.Content.ReadAsStringAsync();
            var Items = JsonConvert.DeserializeObject<List<ItemClass>>(content);
            Console.WriteLine("deeepak  1        " + response) ;
            Console.WriteLine("deeepak  2         " + content);
            Console.WriteLine("deeepak 3          " + Items);
            ListView1.ItemsSource = Items;
        }

        private async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            LoadData();
        }
    }  
}