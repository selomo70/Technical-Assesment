using Entities.Models;
using GalaSoft.MvvmLight.Command;
using Haldan.Wpf.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Haldan.Wpf.ViewModels
{
    public class LoginWindowViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void closeApplication()
        {
            Environment.Exit(default(int)); //this will close app if the user cancells the login, If we don't do this the main page will be displauyed even if the use does not login and closes the login interface.
        }

        private bool _isAuthenticated;
        public bool isAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                if (value != _isAuthenticated)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged("isAuthenticated");
                }
            }
        }
        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("UserName");
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand(() => Login()); }
        }

        public void Login()
        {
            LoginResponse responseObj = new LoginResponse();
            ApplicationUser requestObj = new ApplicationUser { Username = UserName, Password = Password };
            //check username and password vs database here.
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password))
                isAuthenticated = true;


            // Posting.  
            using (var client = new System.Net.Http.HttpClient())
            {
                // Setting Base address.  
                string loginWebServiceUrl = "http://www.autoediportal.com/AutoEDI/Api/v1/TestLogin.php";

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Setting timeout.  
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                // Initialization.  
                System.Net.Http.HttpResponseMessage response = new System.Net.Http.HttpResponseMessage();

                // HTTP POST  
                //response =  client.PostAsJsonAsync("").ConfigureAwait(false);
                var Json = JsonConvert.SerializeObject(requestObj);
                System.Net.Http.HttpContent httpContent = new System.Net.Http.StringContent(Json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                response = client.PostAsJsonAsync(loginWebServiceUrl, httpContent).Result;
                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<LoginResponse>(result);
                    responseObj.message = "success";
                    responseObj.guid = Guid.NewGuid();
                    // Releasing.  
                    response.Dispose();
                }
                else
                {
                    // Reading Response.
                    responseObj.message = "error";
                    string result = response.Content.ReadAsStringAsync().Result;

                }

            }
        }

        #region INotifyPropertyChanged Methods

        public void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, args);
            }
        }



        public DelegateCommand CloseApplication
        {
            get { return new DelegateCommand(this.closeApplication); }
        }



        public Visibility ShowPassword { get; private set; }

        #endregion
    }
}
