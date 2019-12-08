using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Image
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    
    public partial class MainPage : ContentPage
    {
        string _fileName,_filePath;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnPicSelect(object sender, EventArgs e)
        {
            try
            {
                var crossFilePicker = Plugin.FilePicker.CrossFilePicker.Current;
                var myResult = await crossFilePicker.PickFile();
                if (!string.IsNullOrEmpty(myResult.FileName)) //Just the file name, it doesn't has the path
                {
                    _filePath = myResult.FilePath;
                    _fileName = myResult.FileName;
                    if (File.Exists(_filePath))
                    {
                        pic_path.Text = File.ReadAllText(_filePath);
                    }
                    /*foreach (byte b in myResult.DataArray) //Empty array
                        b.ToString();*/
                }
            }
            catch (InvalidOperationException ex)
            {
                ex.ToString(); //"Only one operation can be active at a time"
            }
        }
        private async void OnSendPic(object sender, EventArgs e)
        {
            string desc = pic_desc.Text;

            //variable
            var url = "http://ravi-s-mishra.herokuapp.com/event/addevent/";

            try
            {
                var upfilebytes = File.ReadAllBytes(_filePath);

                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                StringContent Description = new StringContent(desc);
                content.Add(baContent, "EventPic", _fileName);
                content.Add(Description,"Description");

                Console.WriteLine("DEEPAK   " + _fileName);
                Console.WriteLine("DEEPAK   " + desc);
                Console.WriteLine("DEEPAK   " + Description);
                Console.WriteLine("DEEPAK   " + baContent);
                Console.WriteLine("DEEPAK   " + _filePath);





                //upload MultipartFormDataContent content async and store response in response var
                var response =
                    await client.PostAsync(url, content);

                //read response result as a string async into json var
                var responsestr = response.Content.ReadAsStringAsync().Result;

                //debug
                Debug.WriteLine(responsestr);
                DisplayAlert("SUCCESS", "Picture Added", "OK");

            }
            catch (Exception W)
            {
                //debug
                Debug.WriteLine("Exception Caught: " + W.ToString());

                return;
            }
        }
    }
    
}
