using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XFValidationApp1.Models;
using XFValidationApp1.Validations;

namespace XFValidationApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        private ValidatableObject<string> itemText = new ValidatableObject<string>();

        public ValidatableObject<string> ItemText
        {
            get { return itemText; }
            set
            {
                itemText = value;
                OnPropertyChanged("ItemText");
            }
        }


        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };
            itemText.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Item Text cannot be null."
            });
            itemText.Validate();
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private bool ValidateItemText()
        {
            return itemText.Validate();
        }

        public ICommand ValidateItemTextCommand => new Command(() => ValidateItemText());
    }
}