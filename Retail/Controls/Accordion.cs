using System;
using System.Collections.Generic;
using Retail.Infrastructure.ResponseModels;
using Xamarin.Forms;

namespace Retail.Controls
{
    public class Accordion : ContentView
	{
		
		#region Private Variables
		List<AccordionSource> mDataSource;
		bool mFirstExpaned = false;
		StackLayout mMainLayout;
		#endregion

		public Accordion()
        {
			var mMainLayout = new StackLayout();
			Content = mMainLayout;
			//mMainLayout.Orientation = StackOrientation.Horizontal;
			
		}

		public Accordion(List<AccordionSource> aSource)
		{
			mDataSource = aSource;
			DataBind();
		}

		#region Properties
		public List<AccordionSource> DataSource
		{
			get { return mDataSource; }
			set { mDataSource = value; }
		}
		public bool FirstExpaned
		{
			get { return mFirstExpaned; }
			set { mFirstExpaned = value; }
		}
		#endregion
		
		public void DataBind()
		{
			
			var vMainLayout = new StackLayout();
			vMainLayout.BackgroundColor = Color.FromHex("#ECECEC");
			
			//vMainLayout.Orientation = StackOrientation.Horizontal;
			//vMainLayout.Padding = new Thickness(5, 0, 5, 0);
			var vFirst = true; string format = "{0,-15}";
			if (mDataSource != null)
			{
				foreach (var vSingleItem in mDataSource)
				{

					var vHeaderButton = new AccordionButton()
					{
						HorizontalOptions = LayoutOptions.StartAndExpand,
						Text =stringFormat(vSingleItem.Maincategory,"Left",15) + "" + stringFormat(vSingleItem.Target.ToString(),"Center", 26) + "" + stringFormat(vSingleItem.Achieved.ToString(),"Center", 8) + "" + stringFormat(vSingleItem.Balance.ToString(),"Center", 14),
						//Text =string.Format(format, vSingleItem.Maincategory,vSingleItem.Target,vSingleItem.Achieved,vSingleItem.Balance), //vSingleItem.Maincategory+ "                          " + vSingleItem.Target+ "             " + vSingleItem.Achieved+ "             " + vSingleItem.Balance,						
						TextColor = vSingleItem.HeaderTextColor,
						BackgroundColor = Color.FromHex("#ECECEC"),
						MaincategorySalesTargetSummaries = vSingleItem.MaincategorySalesTargetSummaries,

					};

                    //var targetbutton = new TargetButton()
                    //{
                    //    HorizontalOptions = LayoutOptions.StartAndExpand,
                    //    Text = vSingleItem.Target.ToString(),
                    //    TextColor = vSingleItem.HeaderTextColor,
                    //    BackgroundColor = Color.FromHex("#ECECEC"),
                    //    MaincategorySalesTargetSummaries = vSingleItem.MaincategorySalesTargetSummaries,
                    //};

                    //var achievedbutton = new AchievedButton()
                    //{
                    //    HorizontalOptions = LayoutOptions.StartAndExpand,
                    //    Text = vSingleItem.Achieved.ToString(),
                    //    TextColor = vSingleItem.HeaderTextColor,
                    //    BackgroundColor = Color.FromHex("#ECECEC"),
                    //    MaincategorySalesTargetSummaries = vSingleItem.MaincategorySalesTargetSummaries,
                    //};

                    //var balancebutton = new BalanceButton()
                    //{
                    //    HorizontalOptions = LayoutOptions.StartAndExpand,
                    //    Text = vSingleItem.Balance.ToString(),
                    //    TextColor = vSingleItem.HeaderTextColor,
                    //    BackgroundColor = Color.FromHex("#ECECEC"),
                    //    MaincategorySalesTargetSummaries = vSingleItem.MaincategorySalesTargetSummaries,
                    //};


                    var vAccordionContent = new ContentView()
					{
						Content = vSingleItem.ContentItems,
						IsVisible = false
					};
					if (vFirst)
					{
						vHeaderButton.Expand = mFirstExpaned;
						vAccordionContent.IsVisible = mFirstExpaned;
						vFirst = false;
					}
					vHeaderButton.AssosiatedContent = vAccordionContent;
                    //targetbutton.AssosiatedContent = vAccordionContent;
                    //achievedbutton.AssosiatedContent = vAccordionContent;
                    //balancebutton.AssosiatedContent = vAccordionContent;
                    vHeaderButton.Clicked += OnAccordionButtonClicked;
                    //targetbutton.Clicked += OnAccordionButtonClicked;
                    //achievedbutton.Clicked += OnAccordionButtonClicked;
                    //balancebutton.Clicked += OnAccordionButtonClicked;
                    vMainLayout.Children.Add(vHeaderButton);
                    //vMainLayout.Children.Add(targetbutton);
                    //vMainLayout.Children.Add(achievedbutton);
                    //vMainLayout.Children.Add(balancebutton);

                    vMainLayout.Children.Add(vAccordionContent);
					//vMainLayout.Orientation = StackOrientation.Horizontal;
				}
			}
			
			mMainLayout = vMainLayout;
			Content = mMainLayout;
		}

		public string StringCentering(string s, int desiredLength)
		{
			if (s.Length >= desiredLength) return s;
			int firstpad = (s.Length + desiredLength) / 2;
			return s.PadLeft(firstpad).PadRight(desiredLength);
		}

		public string stringFormat(string str, string align, int length)
		{
			if (align == "Right")
				str = String.Format("{0," + length + "}", str);
			else if (align == "Center")
				str = String.Format("{0,-" + length + "}", String.Format("{0," + ((length + str.Length) / 2).ToString() + "}", str));
			else
				str = String.Format("{0,-" + length + "}", str);
			return str + " ";

		}

		//private void Balancebutton_Clicked(object sender, EventArgs e)
		//{
		//    foreach (var vChildItem in mMainLayout.Children)
		//    {
		//        if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
		//        if (vChildItem.GetType() == typeof(BalanceButton))
		//        {
		//            var vButton = (BalanceButton)vChildItem;
		//            vButton.Expand = false;
		//        }
		//    }
		//    var vSenderButton = (BalanceButton)sender;

		//    if (vSenderButton.Expand)
		//    {
		//        vSenderButton.Expand = false;
		//    }
		//    else vSenderButton.Expand = true;
		//    vSenderButton.AssosiatedContent.IsVisible = vSenderButton.Expand;
		//}

		//private void Targetbutton_Clicked(object sender, EventArgs e)
		//{
		//    foreach (var vChildItem in mMainLayout.Children)
		//    {
		//        if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
		//        if (vChildItem.GetType() == typeof(TargetButton))
		//        {
		//            var vButton = (TargetButton)vChildItem;
		//            vButton.Expand = false;
		//        }
		//    }
		//    var vSenderButton = (TargetButton)sender;

		//    if (vSenderButton.Expand)
		//    {
		//        vSenderButton.Expand = false;
		//    }
		//    else vSenderButton.Expand = true;
		//    vSenderButton.AssosiatedContent.IsVisible = vSenderButton.Expand;
		//}

		//private void Achievedbutton_Clicked(object sender, EventArgs e)
		//{
		//    foreach (var vChildItem in mMainLayout.Children)
		//    {
		//        if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
		//        if (vChildItem.GetType() == typeof(AchievedButton))
		//        {
		//            var vButton = (AchievedButton)vChildItem;
		//            vButton.Expand = false;
		//        }
		//    }
		//    var vSenderButton = (AchievedButton)sender;

		//    if (vSenderButton.Expand)
		//    {
		//        vSenderButton.Expand = false;
		//    }
		//    else vSenderButton.Expand = true;
		//    vSenderButton.AssosiatedContent.IsVisible = vSenderButton.Expand;
		//}

		public void OnAccordionButtonClicked(object sender, EventArgs args)
		{
			foreach (var vChildItem in mMainLayout.Children)
			{
				if (vChildItem.GetType() == typeof(ContentView)) vChildItem.IsVisible = false;
				if (vChildItem.GetType() == typeof(AccordionButton))
				{
					var vButton = (AccordionButton)vChildItem;
					//vButton.VerticalOptions = LayoutOptions.End;
					vButton.Expand = false;
				}
			}
			var vSenderButton = (AccordionButton)sender;

			if (vSenderButton.Expand)
			{
				vSenderButton.Expand = false;
			}
			else vSenderButton.Expand = true;
			//vSenderButton.VerticalOptions = LayoutOptions.End;
			vSenderButton.AssosiatedContent.IsVisible = vSenderButton.Expand;
		}
	}

	public class AccordionButton : Button
	{
		#region Private Variables
		bool mExpand = false;
        #endregion
        public AccordionButton()
        {
            HorizontalOptions = LayoutOptions.StartAndExpand;
			BackgroundColor = Color.White;            
            BorderRadius = 5;
            BorderWidth = 0;			
            ImageSource = "forward.png";

        }
		
		public SalesTargetSummary MaincategorySalesTargetSummaries { get; set; }

		#region Properties
		public bool Expand
		{
			get { return mExpand; }
			set { mExpand = value; }
		}
		public ContentView AssosiatedContent
		{ get; set; }
		#endregion
	}

	public class TargetButton : Button
	{
		#region Private Variables
		bool mExpand = false;
		#endregion
		public TargetButton()
		{
			HorizontalOptions = LayoutOptions.StartAndExpand;
			BackgroundColor = Color.White;
					
		}

		public SalesTargetSummary MaincategorySalesTargetSummaries { get; set; }

		#region Properties
		public bool Expand
		{
			get { return mExpand; }
			set { mExpand = value; }
		}
		public ContentView AssosiatedContent
		{ get; set; }
		#endregion
	}

	public class AchievedButton : Button
	{
		#region Private Variables
		bool mExpand = false;
		#endregion
		public AchievedButton()
		{
			HorizontalOptions = LayoutOptions.StartAndExpand;
			BackgroundColor = Color.White;
			
		}

		public SalesTargetSummary MaincategorySalesTargetSummaries { get; set; }

		#region Properties
		public bool Expand
		{
			get { return mExpand; }
			set { mExpand = value; }
		}
		public ContentView AssosiatedContent
		{ get; set; }
		#endregion
	}

	public class BalanceButton : Button
	{
		#region Private Variables
		bool mExpand = false;
		#endregion
		public BalanceButton()
		{
			HorizontalOptions = LayoutOptions.StartAndExpand;
			BackgroundColor = Color.White;
		
		}

		public SalesTargetSummary MaincategorySalesTargetSummaries { get; set; }

		#region Properties
		public bool Expand
		{
			get { return mExpand; }
			set { mExpand = value; }
		}
		public ContentView AssosiatedContent
		{ get; set; }
		#endregion
	}

	public class AccordionSource
	{
		public string Maincategory { get; set; }
		public int Target { get; set; }
		public int Achieved { get; set; }
		public int Balance { get; set; }
		public SalesTargetSummary MaincategorySalesTargetSummaries { get; set; }
		public Color HeaderTextColor { get; set; }
		//public Color HeaderBackGroundColor { get; set; }
		public ListView ContentItems{ get; set; }
		public List<SalesTargetSummary> SubcategorySalesTargetSummaries { get; set; }
		
	}
}
