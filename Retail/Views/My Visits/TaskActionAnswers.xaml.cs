using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Views.MyVisits.TaskSummary;
using Xamarin.Forms;

namespace Retail.Views.MyVisits
{
    public partial class TaskActionAnswers : ContentView
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            //Content.BindingContext = this;
        }

        public TaskActionAnswers()
        {
            InitializeComponent();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Padding = new Thickness(0, 10, 0, 10);
                    break;
                case Device.Android:
                    Padding = new Thickness(0, 10, 0, 10);
                    break;
                case Device.UWP:
                    Padding = new Thickness(0);
                    break;
            }
        }


        public static readonly BindableProperty QuestionDataListProperty =
            BindableProperty.Create(
                nameof(QuestionDataList),
                typeof(ObservableCollection<TaskQuestionAnswerToSubmitResponse>),
                typeof(TaskActionAnswers),
                defaultValue: null,
                defaultBindingMode: BindingMode.Default,
                propertyChanged: QuestionDataListChangedProperty);

        public static readonly BindableProperty QIdProperty =
            BindableProperty.Create(
                nameof(QId),
                typeof(long),
                typeof(TaskActionAnswers),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: QIdChangedProperty);

        private static void QuestionDataListChangedProperty(BindableObject bindable,
            object oldValue, object newValue)
        {
            var viewControl = (TaskActionAnswers)bindable;
            var list = (ObservableCollection<TaskQuestionAnswerToSubmitResponse>)newValue;
            if (viewControl != null)
            {
                viewControl.QuestionDataList1 = list;
            }
        }

        private static void QIdChangedProperty(BindableObject bindable,
            object oldValue, object newValue)
        {
            var viewControl = (TaskActionAnswers)bindable;
            var id = (long)newValue;
            if (viewControl != null)
            {
                viewControl.QId1 = id;
                viewControl.DataBindTypeBasis();
            }
        }

        public ObservableCollection<TaskQuestionAnswerToSubmitResponse> QuestionDataList
        {
            get => (ObservableCollection<TaskQuestionAnswerToSubmitResponse>)GetValue(QuestionDataListProperty);
            set => SetValue(QuestionDataListProperty, value);
        }

        public long QId
        {
            get => (long)GetValue(QIdProperty);
            set => SetValue(QIdProperty, value);
        }

        public ObservableCollection<TaskQuestionAnswerToSubmitResponse> QuestionDataList1 { get; set; }
        public long QId1 { get; set; }

        public void DataBindTypeBasis()
        {
            if (QId1 < 1 || QId1 > QuestionDataList1.Count)
                return;

            //BackgroundColor = Color.PowderBlue;

            switch (QuestionDataList1[(int)QId1 - 1].AnswerTypeId)
            {
                case (int)AnswerTypeEnum.Text:
                    DataTextCell();
                    break;
                case (int)AnswerTypeEnum.Sign:
                    DataDisplaySignatureCell();
                    break;
                case (int)AnswerTypeEnum.YesNo:
                    //DataYesNoSwitchCell();
                    DataYesNoCheckboxCell();
                    break;
                case (int)AnswerTypeEnum.Number:
                    DataNumberCell();
                    break;
                case (int)AnswerTypeEnum.Date:
                    DataDateCell();
                    break;
                case (int)AnswerTypeEnum.Price:
                    DataPriceCell();
                    break;
                case (int)AnswerTypeEnum.Selection:
                    DataSelectionCell();
                    break;
                case (int)AnswerTypeEnum.Image:
                    DataDisplayPhotoCell();
                    break;
                case (int)AnswerTypeEnum.Video:
                    DataDisplayVideoCell();
                    break;
                case (int)AnswerTypeEnum.Barcode:
                    DataDisplayBarCodeCell();
                    break;
                case (int)AnswerTypeEnum.QRcode:
                    DataDisplayQRCodeCell();
                    break;
            }

        }

        #region number entry
        Entry entryNumber;
        Button leftButtonNumber, rightButtonNumber;
        Editor SrNoEntry;

        public void DataNumberCell()
        {
            entryNumber = new Entry()
            {
                Placeholder = "Enter number",
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 500,
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor= (Color)Application.Current.Resources["GrayShade"],
                FontFamily= "calibribold",FontSize=15,
                FontAttributes = FontAttributes.Bold
            };

            leftButtonNumber = new Button()
            {
                Text = " - ",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                TextColor = Color.White,
                BackgroundColor = (Color)Application.Current.Resources["BlueColor"],
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 200,
                FontFamily = "calibribold",
                FontAttributes = FontAttributes.Bold

            };

            rightButtonNumber = new Button()
            {
                Text = " + ",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                TextColor = Color.White,
                BackgroundColor = (Color)Application.Current.Resources["BlueColor"],
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 200,
                FontFamily = "calibribold",
                FontAttributes = FontAttributes.Bold

            };

            entryNumber.SetBinding(Entry.TextProperty, "AnswerNumber");

            #region commonbindings
            SrNoEntry = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,                
            };
            SrNoEntry.SetBinding(Editor.TextProperty, "SrNo");
            SrNoEntry.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            entryNumber.BindingContext = QuestionDataList1[(int)QId1 - 1];

            var grid = new Grid
            {
                //WidthRequest = 500,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor=Color.White,
                Margin = new Thickness(2,2),
                ColumnDefinitions = {
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions = {
                    new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                    //new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(leftButtonNumber, 0, 0);
            grid.Children.Add(entryNumber, 1, 0);
            grid.Children.Add(rightButtonNumber, 4, 0);

            Grid.SetColumnSpan(entryNumber, 3);
            //Grid.SetRowSpan(entryNumber, 2);

            entryNumber.Unfocused += EntryNumber_Unfocused;
            entryNumber.TextChanged += EntryNumber_TextChanged;
            leftButtonNumber.Clicked += LeftButtonNumber_Clicked;
            rightButtonNumber.Clicked += RightButtonNumber_Clicked;

            this.SubRoot.Content = grid;
        }

        private void EntryNumber_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoEntry.Text) - 1]);
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        private void EntryNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoEntry.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;
        }

        private void RightButtonNumber_Clicked(object sender, EventArgs e)
        {
            try
            {
                Int32 answerNumber = Convert.ToInt32(entryNumber.Text);
                if (int.TryParse(answerNumber.ToString(), out int answer))
                {
                    if (answer > 0)
                    {
                        answer = answer + 1;
                    } else
                    {
                        answer = 1;
                    }
                }
                entryNumber.Text = answer.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void LeftButtonNumber_Clicked(object sender, EventArgs e)
        {
            try
            {
                Int32 answerNumber = Convert.ToInt32(entryNumber.Text);
                if (int.TryParse(answerNumber.ToString(), out int answer))
                {
                    if (answer > 0)
                    {
                        answer = answer - 1;
                    }
                }
                entryNumber.Text = answer.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region text editor
        Editor editorText, SrNoText;

        public void DataTextCell()
        {
            editorText = new Editor()
            {
                Placeholder = "Enter text",
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                HeightRequest = 100,
                WidthRequest = 500,
                Keyboard = Keyboard.Chat,
                FontFamily = "calibribold",FontSize=15,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["GrayShade"],

            };

            editorText.SetBinding(Editor.TextProperty, "AnswerText");

            #region commonbindings
            SrNoText = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,FontAttributes=FontAttributes.Bold
            };
            SrNoText.SetBinding(Editor.TextProperty, "SrNo");
            SrNoText.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            editorText.BindingContext = QuestionDataList1[(int)QId1 - 1];
            editorText.Unfocused += EditorText_Unfocused;
            editorText.TextChanged += EditorText_TextChanged;

            var grid = new Grid
            {
                //WidthRequest = 500,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Margin = new Thickness(2, 2),
                ColumnDefinitions = {
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions = {
                    new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                    //new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(editorText, 0, 0);

            //Grid.SetColumnSpan(editorText, 3);
            //Grid.SetRowSpan(editorText, 2);

            this.SubRoot.Content = grid;
        }

        private void EditorText_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoText.Text) - 1]);
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        private void EditorText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoText.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;
        }
        #endregion

        #region price entry
        Entry entryPrice;
        Button leftButtonPrice;
        Button rightButtonPrice;
        Editor SrNoPrice;

        public void DataPriceCell()
        {
            entryPrice = new Entry()
            {
                Placeholder = "Enter price",
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 500,
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["GrayShade"],

            };

            leftButtonPrice = new Button()
            {
                Text = " - ",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                TextColor = Color.White,
                BackgroundColor = (Color)Application.Current.Resources["BlueColor"],
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 200,
                FontFamily = "calibribold",                
                FontAttributes = FontAttributes.Bold,                
            };

            rightButtonPrice = new Button()
            {
                Text = " + ",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                TextColor = Color.White,
                BackgroundColor = (Color)Application.Current.Resources["BlueColor"],
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 200,
                FontFamily = "calibribold",                
                FontAttributes = FontAttributes.Bold,
                
            };

            entryPrice.SetBinding(Entry.TextProperty, "AnswerPrice");

            #region commonbindings
            SrNoPrice = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                
            };
            SrNoPrice.SetBinding(Editor.TextProperty, "SrNo");
            SrNoPrice.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            entryPrice.BindingContext = QuestionDataList1[(int)QId1 - 1];

            var grid = new Grid
            {
                //WidthRequest = 500,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Margin = new Thickness(2, 2),
                ColumnDefinitions = {
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions = {
                    new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                    //new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(leftButtonPrice, 0, 0);
            grid.Children.Add(entryPrice, 1, 0);
            grid.Children.Add(rightButtonPrice, 4, 0);

            Grid.SetColumnSpan(entryPrice, 3);
            //Grid.SetRowSpan(entryNumber, 2);
            leftButtonPrice.Clicked += LeftButtonPrice_Clicked;
            rightButtonPrice.Clicked += RightButtonPrice_Clicked;
            entryPrice.TextChanged += EntryPrice_TextChanged;
            entryPrice.Unfocused += EntryPrice_Unfocused;

            this.SubRoot.Content = grid;
        }

        private void EntryPrice_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoPrice.Text) - 1]);
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        private void EntryPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoPrice.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;

        }

        private void RightButtonPrice_Clicked(object sender, EventArgs e)
        {
            try
            {
                Int32 answerPrice = Convert.ToInt32(entryPrice.Text);
                if (int.TryParse(answerPrice.ToString(), out int answer))
                {
                    if (answer > 0)
                    {
                        answer = answer + 1;
                    }
                    else
                    {
                        answer = 1;
                    }
                }
                entryPrice.Text = answer.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void LeftButtonPrice_Clicked(object sender, EventArgs e)
        {
            try
            {
                Int32 answerPrice = Convert.ToInt32(entryPrice.Text);
                if (int.TryParse(answerPrice.ToString(), out int answer))
                {
                    if (answer > 0)
                    {
                        answer = answer - 1;
                    }
                }
                entryPrice.Text = answer.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region yes no switch
        Xamarin.Forms.Switch switchYesNo;
        Label switchYesNoLabel;
        Editor SrNoSwitch;

        public void DataYesNoSwitchCell()
        {
            switchYesNo = new Xamarin.Forms.Switch()
            {
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                OnColor = (Color)Application.Current.Resources["BlueColor"],
                
            };
            switchYesNoLabel = new Label()
            {
                Text = "",
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = (Color)Application.Current.Resources["GrayShade"],
                FontFamily = "calibribold",
                FontAttributes = FontAttributes.Bold
            };

            switchYesNo.SetBinding(Xamarin.Forms.Switch.IsToggledProperty, "AnswerYesNo", mode: BindingMode.TwoWay);//IsMandatory

            #region commonbindings
            SrNoSwitch = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                
            };
            SrNoSwitch.SetBinding(Editor.TextProperty, "SrNo");
            SrNoSwitch.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            switchYesNo.BindingContext = QuestionDataList1[(int)QId1 - 1];

            switchYesNoLabel.Text = switchYesNo.IsToggled ? "Yes" : "No";

            var grid = new Grid
            {
                //WidthRequest = 500,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.White,
                Margin = new Thickness(10,2),
                ColumnDefinitions = {
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions = {
                    new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                    //new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(switchYesNo, 0, 0);
            grid.Children.Add(switchYesNoLabel, 1, 0);

            //Grid.SetColumnSpan(switchYesNo, 3);
            //Grid.SetRowSpan(entryNumber, 2);
            switchYesNo.Toggled += SwitchYesNo_Toggled;

            this.SubRoot.Content = grid;
        }

        private void SwitchYesNo_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                string stateName = e.Value ? "Yes" : "No";
                switchYesNoLabel.Text = stateName;

                TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoSwitch.Text) - 1]);
                TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
                TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
                TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }
        #endregion

        #region PrjCheckBox
        CheckBox ChkYes, ChkNo;
        Label ChkYesLabel, ChkNoLabel;
        //Prajakta
        public void DataYesNoCheckboxCell()
        {
            ChkYes = new CheckBox()
            {
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,

            };

            ChkNo = new CheckBox()
            {
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            ChkYesLabel = new Label()
            {
                Text = "",
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = (Color)Application.Current.Resources["GrayShade"],
                FontFamily = "calibribold",
                FontAttributes = FontAttributes.Bold
            };

            ChkNoLabel = new Label()
            {
                Text = "",
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = (Color)Application.Current.Resources["GrayShade"],
                FontFamily = "calibribold",
                FontAttributes = FontAttributes.Bold
            };

            ChkYes.SetBinding(Xamarin.Forms.CheckBox.IsCheckedProperty, "AnswerYesNo", mode: BindingMode.TwoWay);
            ChkNo.SetBinding(Xamarin.Forms.CheckBox.IsCheckedProperty, "AnswerNo", mode: BindingMode.TwoWay);

            #region commonbindings
            SrNoSwitch = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,                
            };
            SrNoSwitch.SetBinding(Editor.TextProperty, "SrNo");
            SrNoSwitch.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            ChkYes.BindingContext = QuestionDataList1[(int)QId1 - 1];
            ChkNo.BindingContext = QuestionDataList1[(int)QId1 - 1];

            ChkYesLabel.Text = "Yes";
            ChkNoLabel.Text = "No";

            var grid = new Grid
            {
                //WidthRequest = 500,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.White,
                Margin = new Thickness(10, 2),
                ColumnDefinitions = {
                    new ColumnDefinition { Width=new GridLength(2.5, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(2.5, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(2.5, GridUnitType.Star) },
                    new ColumnDefinition { Width=new GridLength(2.5, GridUnitType.Star) },
                },
                RowDefinitions = {
                    new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                    //new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(ChkYes, 0, 0);
            grid.Children.Add(ChkYesLabel, 1, 0);
            grid.Children.Add(ChkNo, 2, 0);
            grid.Children.Add(ChkNoLabel, 3, 0);

            ChkYes.CheckedChanged += ChkYes_CheckedChanged;
            ChkNo.CheckedChanged += ChkNo_CheckedChanged;

            this.SubRoot.Content = grid;
        }

        public bool? IsCheckedYesNo { get; set; }

        //Prajakta
        private void ChkNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoSwitch.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;

            if (ChkNo.IsChecked == true)
            {
                TaskQuestionAnswer1.AnswerYesNo = false;
                ChkYes.IsChecked = false;
            }
            else
            {
                TaskQuestionAnswer1.AnswerYesNo = true;
                ChkYes.IsChecked = true;
            }

            MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
        }

        private void ChkYes_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoSwitch.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;

            if (ChkYes.IsChecked == true)
            {
                TaskQuestionAnswer1.AnswerYesNo = true;
                ChkNo.IsChecked = false;
            }
            else
            {
                TaskQuestionAnswer1.AnswerYesNo = false;
                ChkNo.IsChecked = true;
            }

            MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
        }

        #endregion


        #region date picker
        DatePicker dateDatePicker;
        Editor SrNoDate;

        public void DataDateCell()
        {
            dateDatePicker = new DatePicker()
            {
                BackgroundColor = Color.White,
                Margin = new Thickness(1),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Format = "dd,MMM,yyyy",
                WidthRequest = 500,
                HeightRequest = 50,
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                TextColor = (Color)Application.Current.Resources["GrayShade"],
            };

            dateDatePicker.SetBinding(DatePicker.DateProperty, "AnswerDate", mode: BindingMode.TwoWay);//IsMandatory

            #region commonbindings
            SrNoDate = new Editor()
            {
            };
            SrNoDate.SetBinding(Editor.TextProperty, "SrNo");
            SrNoDate.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            dateDatePicker.BindingContext = QuestionDataList1[(int)QId1 - 1];
            dateDatePicker.DateSelected += DateDatePicker_DateSelected;

            var grid = new Grid
            {
                //WidthRequest = 500,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.White,
                Margin = new Thickness(2, 2),
                ColumnDefinitions = {
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions = {
                    new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                    //new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(dateDatePicker, 0, 0);

            //Grid.SetColumnSpan(dateDatePicker, 3);
            //Grid.SetRowSpan(dateDatePicker, 2);

            this.SubRoot.Content = grid;
        }

        private void DateDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoDate.Text) - 1]);
                TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
                TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
                TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;
                TaskQuestionAnswer1.AnswerDate = dateDatePicker.Date;
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }

        }
        #endregion

        #region Image file
        ImageButton rightArrowImage;
        Label fileCountImage;
        Editor SrNoPhoto;

        public void DataDisplayPhotoCell()
        {
            rightArrowImage = new ImageButton()
            {
                Source = "dbforwardarrow.png",
                //Margin = new Thickness(10, -45, 0, 10),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = 30,
                WidthRequest = 30,
                BackgroundColor = Color.White,

            };
            fileCountImage = new Label()
            {
                Text = "0 photo added",
                TextColor = Color.Gray,
                BackgroundColor = Color.Transparent,
                HeightRequest = 40,
                Margin = new Thickness(10, 0, 0, 10),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                
            };

            #region commonbindings
            SrNoPhoto = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                
            };
            SrNoPhoto.SetBinding(Editor.TextProperty, "SrNo");
            SrNoPhoto.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            if (QuestionDataList1[(int)QId1 - 1].AnswerUploadedFiles != null
                && QuestionDataList1[(int)QId1 - 1].AnswerUploadedFiles.Count > 0)
            {
                fileCountImage.Text = string.Format("{0} photo added", QuestionDataList1[(int)QId1 - 1].AnswerUploadedFiles.Count.ToString());
            }

            var grid = new Grid();

            grid.Children.Add(rightArrowImage);
            grid.Children.Add(fileCountImage);

            rightArrowImage.Clicked += RightArrowImage_Clicked;

            this.SubRoot.Content = grid;
        }

        private void RightArrowImage_Clicked(object sender, EventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoPhoto.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;

            if (TaskQuestionAnswer1.AnswerUploadedFiles == null)
                TaskQuestionAnswer1.AnswerUploadedFiles = new ObservableCollection<AnswerUploadedFileResponse>();

            ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles = new ObservableCollection<AnswerUploadedFileResponse>(TaskQuestionAnswer1.AnswerUploadedFiles);
            Navigation.PushAsync(new TakeDisplayPhoto(AnswerUploadedFiles, TaskQuestionAnswer1));
        }
        #endregion

        #region Video file
        ImageButton rightArrowVideo;
        Label fileCountVideo;
        Editor SrNoVideo;

        public void DataDisplayVideoCell()
        {
            rightArrowVideo = new ImageButton()
            {
                Source = "dbforwardarrow.png",
                //Margin = new Thickness(10, -45, 0, 10),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = 30,
                WidthRequest = 30,
                BackgroundColor = Color.White,
            };
            fileCountVideo = new Label()
            {
                Text = "0 video added",
                TextColor = Color.Gray,
                BackgroundColor = Color.Transparent,
                HeightRequest = 40,
                Margin = new Thickness(10, 0, 0, 10),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
              
            };

            #region commonbindings
            SrNoVideo = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                
            };
            SrNoVideo.SetBinding(Editor.TextProperty, "SrNo");
            SrNoVideo.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion


            if (QuestionDataList1[(int)QId1 - 1].AnswerUploadedFiles != null
                && QuestionDataList1[(int)QId1 - 1].AnswerUploadedFiles.Count > 0)
            {
                fileCountVideo.Text = string.Format("{0} video added", QuestionDataList1[(int)QId1 - 1].AnswerUploadedFiles.Count.ToString());
            }


            var grid = new Grid();
            grid.Children.Add(fileCountVideo);
            grid.Children.Add(rightArrowVideo);

            rightArrowVideo.Clicked += RightArrowVideo_Clicked;

            this.SubRoot.Content = grid;
        }

        private void RightArrowVideo_Clicked(object sender, EventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoVideo.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;

            if (TaskQuestionAnswer1.AnswerUploadedFiles == null)
                TaskQuestionAnswer1.AnswerUploadedFiles = new ObservableCollection<AnswerUploadedFileResponse>();

            ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles = new ObservableCollection<AnswerUploadedFileResponse>(TaskQuestionAnswer1.AnswerUploadedFiles);
            Navigation.PushAsync(new TakeDisplayVideo(AnswerUploadedFiles, TaskQuestionAnswer1));
        }
        #endregion

        #region Signature file
        ImageButton rightArrowSignature;
        Label fileNameSignature;
        Editor SrNoSign;

        public void DataDisplaySignatureCell()
        {
            rightArrowSignature = new ImageButton()
            {
                Source = "dbforwardarrow.png",
                //Margin = new Thickness(10, -45, 0, 10),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = 30,
                WidthRequest = 30,
                BackgroundColor = Color.White,
            };
            fileNameSignature = new Label()
            {
                Text = "File Name",
                TextColor = Color.Gray,
                BackgroundColor = Color.Transparent,
                HeightRequest = 40,
                Margin = new Thickness(10, 0, 0, 10),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
               
            };

            #region commonbindings
            SrNoSign = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                
            };
            SrNoSign.SetBinding(Editor.TextProperty, "SrNo");
            SrNoSign.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            //fileNameSignature.SetBinding(Label.TextProperty, "AnswerBarcode");
            fileNameSignature.BindingContext = QuestionDataList1[(int)QId1 - 1];

            var grid = new Grid();

            grid.Children.Add(rightArrowSignature);
            //grid.Children.Add(fileNameSignature);

            //Grid.SetColumnSpan(dateDatePicker, 3);
            //Grid.SetRowSpan(dateDatePicker, 2);
            rightArrowSignature.Clicked += RightArrowSignature_Clicked;

            this.SubRoot.Content = grid;
        }

        private void RightArrowSignature_Clicked(object sender, EventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[Convert.ToInt32(SrNoSign.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;
            TaskQuestionAnswer1.AnswerSign = "AnswerSign";

            if (TaskQuestionAnswer1.AnswerUploadedFiles == null)
                TaskQuestionAnswer1.AnswerUploadedFiles = new ObservableCollection<AnswerUploadedFileResponse>();

            ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles = new ObservableCollection<AnswerUploadedFileResponse>(TaskQuestionAnswer1.AnswerUploadedFiles);
            Navigation.PushAsync(new ManagerSignature(AnswerUploadedFiles, TaskQuestionAnswer1));

        }
        #endregion

        #region BarCode file
        ImageButton rightArrowBarCode;
        Label fileNameBarCode;
        Editor SrNoBarcode;

        public void DataDisplayBarCodeCell()
        {
            rightArrowBarCode = new ImageButton()
            {
                Source = "dbforwardarrow.png",
                //Margin = new Thickness(10, -45, 0, 10),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = 30,
                WidthRequest = 30,
                BackgroundColor = Color.White,
            };
            fileNameBarCode = new Label()
            {
                Text = "File Name",
                TextColor = Color.Gray,
                BackgroundColor = Color.Transparent,
                HeightRequest = 40,
                Margin = new Thickness(10, 0, 0, 10),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,                
            };

            #region commonbindings
            SrNoBarcode = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,                
            };
            SrNoBarcode.SetBinding(Editor.TextProperty, "SrNo");
            SrNoBarcode.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            //fileNameBarCode.SetBinding(Label.TextProperty, "AnswerBarcode");
            fileNameBarCode.BindingContext = QuestionDataList1[(int)QId1 - 1];

            var grid = new Grid();

            grid.Children.Add(rightArrowBarCode);
            //grid.Children.Add(fileNameBarCode);

            //Grid.SetColumnSpan(dateDatePicker, 3);
            //Grid.SetRowSpan(dateDatePicker, 2);
            rightArrowBarCode.Clicked += RightArrowBarCode_Clicked;

            this.SubRoot.Content = grid;
        }

        private void RightArrowBarCode_Clicked(object sender, EventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[(int)Convert.ToInt32(SrNoBarcode.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;

            Navigation.PushAsync(new Barcode(TaskQuestionAnswer1));
        }
        #endregion

        #region QRCode file
        ImageButton rightArrowQRCode;
        Label fileNameQRCode;
        Editor SrNoQRcode;

        public void DataDisplayQRCodeCell()
        {
            rightArrowQRCode = new ImageButton()
            {
                Source = "dbforwardarrow.png",
                //Margin = new Thickness(10, -45, 0, 10),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = 30,
                WidthRequest = 30,
                BackgroundColor = Color.White,
            };
            fileNameQRCode = new Label()
            {
                Text = "File Name",
                TextColor = Color.Gray,
                BackgroundColor = Color.Transparent,
                HeightRequest = 40,
                Margin = new Thickness(10, 0, 0, 10),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,               
            };

            #region commonbindings
            SrNoQRcode = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
               
            };
            SrNoQRcode.SetBinding(Editor.TextProperty, "SrNo");
            SrNoQRcode.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            //fileNameQRCode.SetBinding(Label.TextProperty, "AnswerQrcode");

            fileNameQRCode.BindingContext = QuestionDataList1[(int)QId1 - 1];

            var grid = new Grid();

            grid.Children.Add(rightArrowQRCode);
            //grid.Children.Add(fileNameQRCode);

            //Grid.SetColumnSpan(dateDatePicker, 3);
            //Grid.SetRowSpan(dateDatePicker, 2);
            rightArrowQRCode.Clicked += RightArrowQRCode_Clicked;

            this.SubRoot.Content = grid;
        }

        private void RightArrowQRCode_Clicked(object sender, EventArgs e)
        {
            TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[(int)Convert.ToInt32(SrNoQRcode.Text) - 1]);
            TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
            TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
            TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;

            Navigation.PushAsync(new QRCode(TaskQuestionAnswer1));
        }
        #endregion

        #region Selection
        Editor SrNoSelection;

        public void DataSelectionCell()
        {
            var dataTemplate1 = new DataTemplate(typeof(ListDataSelectionCell));
            List<AnswerOptionResponse> AnswerOptions = new List<AnswerOptionResponse>(QuestionDataList1[(int)QId1 - 1].AnswerOptions);
            foreach (var item in AnswerOptions)
            {
                if (item.OptionId == QuestionDataList1[(int)QId1 - 1].AnswerSelectedOptionId)
                {
                    item.OptionSelected = true;
                }
                else
                {
                    item.OptionSelected = false;
                }
            }

            #region commonbindings
            SrNoSelection = new Editor()
            {
                FontFamily = "calibribold",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,               
            };
            SrNoSelection.SetBinding(Editor.TextProperty, "SrNo");
            SrNoSelection.BindingContext = QuestionDataList1[(int)QId1 - 1];
            #endregion

            //switchSelection.SetBinding(Switch.IsToggledProperty, "AnswerYesNo", mode: BindingMode.TwoWay);//IsMandatory
            //switchSelection.BindingContext = QuestionDataList1[(int)QId1 - 1];

            var AnswerOptionList = new ListView()
            {
                ItemsSource = AnswerOptions,
                HeightRequest = AnswerOptions.Count * 50,
                SeparatorColor = Color.LightGray,
                ItemTemplate = dataTemplate1,
                VerticalScrollBarVisibility= ScrollBarVisibility.Never
            };
            AnswerOptionList.ItemTapped += AnswerOptionList_ItemTapped;

            var grid = new Grid
            {
                //WidthRequest = 500,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = Color.White,
                Margin = new Thickness(10, 2),
                ColumnDefinitions = {
                    new ColumnDefinition { Width=new GridLength(1, GridUnitType.Star) },
                },
                RowDefinitions = {
                    new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                    //new RowDefinition { Height=new GridLength(1.0, GridUnitType.Star) },
                }
            };

            grid.Children.Add(AnswerOptionList, 0, 0);

            //Grid.SetColumnSpan(switchSelection, 3);
            //Grid.SetRowSpan(entryNumber, 2);

            this.SubRoot.Content = grid;
        }

        private void AnswerOptionList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                //throw new NotImplementedException();
                var selectedItem = (AnswerOptionResponse)e.Item;
                foreach (var item in QuestionDataList1[(int)Convert.ToInt32(SrNoSelection.Text) - 1].AnswerOptions)
                {
                    item.OptionSelected = false;
                }

                TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 = (QuestionDataList1[(int)Convert.ToInt32(SrNoSelection.Text) - 1]);
                TaskQuestionAnswer1.StatusId = (int)AnswerStatusEnum.Answered;
                TaskQuestionAnswer1.AnswerEntryDate = DateTime.Now;
                TaskQuestionAnswer1.AnswerEntryByPersonId = CommonAttribute.CustomeProfile.PersonId;
                TaskQuestionAnswer1.AnswerSelectedOptionId = selectedItem.OptionId;
                TaskQuestionAnswer1.AnswerSelectedOptionText = selectedItem.OptionName;
                selectedItem.OptionSelected = true;

                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "SelectedAnswerOption");
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }
        #endregion
    }

    #region Selection Redio
    public class ListDataSelectionCell:ViewCell
    {
        public ListDataSelectionCell()
        {
            var OptionName1 = new Label()
            {
                WidthRequest = 300,
                HeightRequest = 50,
                Margin = new Thickness(0, 5, 0, 0),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                TextColor = (Color)Application.Current.Resources["GrayShade"],
                FontFamily = "calibribold",
                FontAttributes = FontAttributes.Bold
            };
            Xamarin.Forms.Switch OptionSelected1 = new Xamarin.Forms.Switch()
            {
            };

            OptionName1.SetBinding(Label.TextProperty, new Binding("OptionName", BindingMode.TwoWay));
            //OptionSelected1.SetBinding(Switch.IsToggledProperty, new Binding("OptionSelected", BindingMode.TwoWay));
            //OptionSwitch1.SetBinding(Switch.IsToggledProperty, new Binding("OptionSelected", BindingMode.TwoWay));

            var OptionNameTapped = new TapGestureRecognizer();
            OptionNameTapped.Tapped += OptionNameTapped_Tapped;
            OptionName1.GestureRecognizers.Add(OptionNameTapped);

            //var PointSelect = new BoxView()
            //{
            //    WidthRequest = 10,
            //    HeightRequest = 10,
            //    CornerRadius = 3,
            //    Margin = new Thickness(5, 5, 5, 5),
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center,
            //    BackgroundColor = Color.Green,
            //};
            //PointSelect.SetBinding(BoxView.IsVisibleProperty, new Binding("OptionSelected", BindingMode.TwoWay));

            var OuterCircle = new Frame()
            {
                Margin = new Thickness(10, 0, 20, 0),
                BorderColor = Color.Black,
                CornerRadius = 10,
                HeightRequest = 90,
                WidthRequest = 20,
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center,
                HasShadow = false,
                Content = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = -20,
                    HeightRequest = 15,
                    WidthRequest = 15,
                    Source = "checked.png",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                }
            };

            OuterCircle.Content.SetBinding(Image.IsVisibleProperty, new Binding("OptionSelected", BindingMode.TwoWay));


            View = new StackLayout()
            {
                BackgroundColor = Color.White,
                //HeightRequest = 150,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(1), //12,8                    
                Children = { OuterCircle, OptionName1 }
            };
        }

        private void OptionNameTapped_Tapped(object sender, EventArgs e)
        {
            var answerOption = sender as AnswerOptionResponse;

        }
    }
    #endregion

}
