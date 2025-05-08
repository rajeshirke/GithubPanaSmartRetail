using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Retail.Models;
using Xamarin.Forms;

namespace Retail.Controls
{
    public partial class TitleDropdownView : ContentView
    {
        string placeholder = string.Empty;

        public TitleDropdownView()
        {
            InitializeComponent();
            dropdown.BindingContext = this;

        }


        public static BindableProperty TitleProperty =
           BindableProperty.Create(nameof(Title), typeof(string), typeof(TitleDropdownView), null, BindingMode.TwoWay);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static BindableProperty PlaceholderProperty =
           BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(TitleDropdownView), "", BindingMode.TwoWay);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty PickerItemsSourceProperty = BindableProperty.Create("PickerItemsSource", typeof(IList), typeof(TitleDropdownView), null, BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public static BindableProperty PlaceholderColorProperty =
                BindableProperty.Create(nameof(PlaceholderColor), typeof(string), typeof(TitleDropdownView), "#C1C1C1", BindingMode.TwoWay);

        public string PlaceholderColor
        {
            get { return (string)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleDropdownView)bindable;
            control.dropdown.ItemsSource = (IList)newValue;
        }
        /// <summary>
        /// Accessors
        /// </summary>
        public IList PickerItemsSource
        {
            get { return (IList)GetValue(PickerItemsSourceProperty); }
            set
            {
                SetValue(PickerItemsSourceProperty, value);
            }
        }
        public static BindableProperty ItemDisplayBindingProperty =
          BindableProperty.Create(nameof(ItemDisplayBinding), typeof(string), typeof(TitleDropdownView), null, BindingMode.TwoWay);

        public string ItemDisplayBinding
        {
            get => (string)GetValue(ItemDisplayBindingProperty);
            set => SetValue(ItemDisplayBindingProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty =
           BindableProperty.Create(nameof(SelectedItem),
               typeof(DropDownModel),
               typeof(TitleDropdownView),
               null,
               BindingMode.TwoWay, propertyChanged: OnSelectedItemPropertyChanged);

        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object value, object newValue)
        {
            var control = (TitleDropdownView)bindable;

            control.dropdown.SelectedItem = newValue as DropDownModel;
            //control.dropdown.Title = (newValue as DropDownModel).Title;
        }
        public DropDownModel SelectedItem
        {
            get
            {
                return this.GetValue(SelectedItemProperty) as DropDownModel;
            }
            set
            {

                this.SetValue(SelectedItemProperty, value);
            }
        }

        void dropdown_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var control = (Picker)sender;
            SelectedItem = control.SelectedItem as DropDownModel;
            Command?.Execute(control.SelectedItem);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        "Command",
        typeof(ICommand),
        typeof(ImageButton),
        null,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            // do something.                
        });

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(TitleDropdownView), null);

        public event EventHandler<EventArgs> Tapped;

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.InvokeOnMainThreadAsync(() => {
                if (dropdown.IsFocused)
                    dropdown.Unfocus();

                dropdown.Focus();
            });
        }        //SelectedItem
    }
}
