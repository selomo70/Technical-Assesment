using DTO.BusinessObjects;
using Entities.Data;
using Entities.Models;
using Haldan.Logic.BusinessLogic;
using MvvmValidation;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Haldan.Wpf.Events.CustomEvent;


namespace Haldan.Wpf.ViewModels
{

    public class UserControlViewModel : BindableBase, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HaldanContext _context;
        private bool canEdit;
        private IEventAggregator eventAggregator;
        private ApplicationUser currentUser;
        private SubscriptionToken disableEditingToken;
        private readonly IApplicationUserLogic _applicationUserLogic;
        private SubscriptionToken setLogedInUser;
        private bool isNewUserAccount;
        private bool isResettingPassword;
        public UserControlViewModel(HaldanContext context, HttpClient httpClient, bool isNewUserAccount, bool isResettingPassword, IApplicationUserLogic applicationUserLogi)
        {
            this._context = context;
            this._applicationUserLogic = applicationUserLogi;
            this._httpClient=httpClient;
            this.isNewUserAccount = isNewUserAccount;
            this.isResettingPassword=isResettingPassword;

        }
        public UserControlViewModel() { }
        #region Properties
        protected ValidationHelper Validator { get; private set; }

        public int SelectedInex { get; set; }

        public Visibility DisplayPasswordButton { get; set; }
        public Visibility DisplayPasswords { get; set; }
        public Visibility SuperAdmin { get; set; }
        public Visibility DisplayErrors { get; set; }

        private MvvmValidation.ValidationResult validationResultSet { get; set; }

        public ObservableCollection<AccessType> AccessTypes { get; }
        public ObservableCollection<ApplicationUser> ApplicationUsers { get; set; }
        public ApplicationUser CurrentUser
        {
            get { return this.currentUser; }

            set
            {
                this.currentUser = value;
                this.CanEdit = false;

                if (this.currentUser != null && this.currentUser.Id != default(int))
                {
                    this.CanExecuteEnableEditing = true;
                    this.enableDisableControls();
                }

                this.DisplayErrors = Visibility.Hidden;

                if (this.currentUser != null && this.currentUser.AccessLevelId == default(int))
                {
                    this.currentUser.AccessLevelId = AccessType.ReadOnly;
                }

                if (this.currentUser != null)
                {
                    this.setAcceslLevel(this.currentUser.AccessLevelId);
                }
            }
        }
        public Views.AddUserControl AddUserControlObject { get; set; }
        public bool CanEdit
        {
            get
            {
                if (this.CurrentUser == null || this.CurrentUser.Id == default(int))
                {
                    return true;

                }

                return this.canEdit;
            }
            set
            {
                this.canEdit = value;
            }
        }
        #endregion
        public DelegateCommand<ApplicationUser> SaveOperatorCommand
        {
            get
            {
                return new DelegateCommand<ApplicationUser>(this.saveUser);
            }
        }
        public DelegateCommand<object> SetAccessType
        {
            get
            {
                return new DelegateCommand<object>(this.setAcceslLevel);
            }
        }
        private void setPassword(object obj)
        {
            if (this.isResettingPassword || this.isNewUserAccount)
            {
                var passwords = (object[])obj;
                this.CurrentUser.Password = ((PasswordBox)passwords[0]).Password;
                this.ConfrimPassword = true;
            }
        }
        private void UnSubscribeToEvent()
        {
            this.eventAggregator.GetEvent<DisableEditingEvent>().Unsubscribe(this.disableEditingToken);
        }
        private void enableDisableControls()
        {
            if (!this.CanEdit)
            {
                this.DisplayPasswordButton = Visibility.Collapsed;
                this.DisplayPasswords = Visibility.Collapsed;
            }
            else
            {
                this.DisplayPasswordButton = Visibility.Visible;
                this.DisplayPasswords = Visibility.Collapsed;
            }
        }

        public bool CanExecuteEnableEditing { get; set; }
        public bool ConfrimPassword { get; set; }
        private bool canExecuteEnableEditing(bool enable)
        {
            return CanExecuteEnableEditing;
        }

        private void configureValidationRules()
        {
            
            this.Validator.AddRule(
                     () => this.CurrentUser.AccessLevelId,
                     () => RuleResult.Assert(!string.IsNullOrEmpty(this.CurrentUser.AccessLevelId.ToString()),
                     "Access Level is a required field"));
            this.Validator.AddRule(
                     () => this.CurrentUser.FirstName,
                     () => RuleResult.Assert(!string.IsNullOrEmpty(this.CurrentUser.FirstName),
                     "First Name is a required field"));
            this.Validator.AddRule(
                () => this.CurrentUser.LastName,
                () => RuleResult.Assert(!string.IsNullOrEmpty(this.CurrentUser.LastName),
                "Surname is a required field"));

            if (this.DisplayPasswords == Visibility.Visible)
            {
                this.Validator.AddRule(
                        () => this.CurrentUser.Password,
                        () => RuleResult.Assert(!string.IsNullOrWhiteSpace(this.CurrentUser.Password),
                        "Please provide Password"));

                this.Validator.AddRule(
                        () => this.ConfrimPassword,
                        () => RuleResult.Assert(!this.ConfrimPassword,
                        "Please confirm Password"));

            
            }



            this.Validator.AddRule(
                    () => this.CurrentUser.Username,
                    () => RuleResult.Assert(!string.IsNullOrWhiteSpace(this.CurrentUser.Username),
                    "User Name is a required field"));
        }

        private void saveUser(ApplicationUser obj)
        {
            this.Validator = new ValidationHelper();
            this.configureValidationRules();
            this.setPassword(obj);
            this.validationResultSet = this.Validator.ValidateAll();
            //bool userNameExists =

               string query = "SELECT * FROM [Users] WHERE [Users].[Username]= " + obj.Username;
             var result= DAL.executeQuery(query);

            //if (string.IsNullOrEmpty(queryCheck)) {

            if (result > 0 && isNewUserAccount)
            {
                MessageBox.Show("User name is already being used. Please use a different user name!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            this.validationResultSet = this.Validator.ValidateAll();

            if (this.validationResultSet.IsValid)
            {
                this.DisplayErrors = Visibility.Hidden;
               bool saved= _applicationUserLogic.SaveUser(obj);
                
               
                if (saved)
                {

                    MessageBox.Show("User Details saved successfully!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    if (!this.ApplicationUsers.Contains(this.CurrentUser))
                    {
                        this.ApplicationUsers.Add(this.CurrentUser);
                    }
                    this.CanEdit = false;
                    _applicationUserLogic.PostRegUser(obj);
                }
                return;
            }
            else
            {
                this.DisplayErrors = Visibility.Visible;
                this.ValidationErrorsString = this.validationResultSet.ToString();
            }

        }

        public string ValidationErrorsString { get; private set; }
        private void resetPage()
        {
            this.isNewUserAccount = true;
            this.DisplayPasswords = Visibility.Visible;
            this.DisplayPasswordButton = Visibility.Collapsed;
            this.CurrentUser = new ApplicationUser();
            this.AddUserControlObject.txbConfrimPassword.Password = string.Empty;
            this.AddUserControlObject.txbPassword.Password = string.Empty;
            this.CanExecuteEnableEditing = false;
            return;
        }


        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
        public bool IsReadOnly { get; set; }

        private void setAcceslLevel(object accessType)
        {
            this.CurrentUser.AccessLevelId = (AccessType)accessType;

            this.IsAdmin = false;
            this.IsUser = false;
            this.IsReadOnly = false;

            switch ((AccessType)accessType)
            {
               
                case AccessType.Admin:
                    this.IsAdmin = true;
                    break;
                case AccessType.User:
                    this.IsUser = true;
                    break;
                case AccessType.ReadOnly:
                    this.IsReadOnly = true;
                    break;
                default:
                    break;
            }
        }

        public void Dispose()
        {
            this.UnSubscribeToEvent();
        }

    }
}
