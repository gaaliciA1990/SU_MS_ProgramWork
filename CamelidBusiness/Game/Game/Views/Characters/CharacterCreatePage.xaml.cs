﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Game.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.GameRules;
using Game.Models;
using Game.ViewModels;


namespace Game.Views
{
    /// <summary>
    /// Create Character
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class CharacterCreatePage : ContentPage
    {
        //Local storage for images
        private Dictionary<CharacterClanEnum, List<string>> imageList = GameImagesHelper.GetCharacterImage();

        //index tracer for local storage
        private int imageIndex = 0;

        // The Character to create
        public GenericViewModel<CharacterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;


        // Empty Constructor for UTs
        public CharacterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            data.Data = new CharacterModel();
            this.ViewModel = data;

            this.ViewModel.Title = "Create";

            NameEntry.Placeholder = "Name";
            DescriptionEntry.Placeholder = "Description";

            // Load the values for the Level into the Picker
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                //LevelPicker.Items.Add(i.ToString());
            }

            this.ViewModel.Data.Level = 1;
            // LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;
            _ = UpdatePageBindingContext();

            ClanPicker.SelectedItem = ViewModel.Data.Clan.ToString();

            SetSliderMaximumBound();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var level = this.ViewModel.Data.Level;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            // This resets the Picker to -1 index, need to reset it back
            ViewModel.Data.Level = level;
            //LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            ClanPicker.SelectedItem = ViewModel.Data.Clan.ToString();

            ManageHealth();

            AddItemsToDisplay();

            return true;
        }


        ///// <summary>
        ///
        /// COMMENTED OUT AS THIS ISN'T DOING ANYTHING THAT WE CAN TEST AND 
        /// DOESN'T APPEAR TO HAVE A USE IN SOLVING THE EXPANDING SLIDER MAX
        /// PROBLEM. KEEPING IN IN THE EVENT THIS IS NEEDED
        /// 
        ///// adjust attribute values displayed next to the slider
        ///// to include item bonuses 
        ///// </summary>
        //public void AdjustValuesWithBonuses(ItemModel item)
        //{
        //    switch (item.Attribute)
        //    {
        //        //case AttributeEnum.Speed:
        //        //    ViewModel.Data.Speed += Math.Min(0, 50 - ViewModel.Data.GetSpeedTotal);
        //        //    SpeedValue.Text = ViewModel.Data.GetSpeedTotal.ToString();
        //        //    SpeedSlider.Maximum = 50 - ViewModel.Data.GetSpeedTotal + ViewModel.Data.Speed;
        //        //    break;
                
        //        case AttributeEnum.Defense:
        //            ViewModel.Data.Defense += Math.Min(0, 50 - ViewModel.Data.GetDefenseTotal);
        //            DefenseValue.Text = ViewModel.Data.GetDefenseTotal.ToString();
        //            break;
                
        //        case AttributeEnum.Attack:
        //            ViewModel.Data.Attack += Math.Min(0, 50 - ViewModel.Data.GetAttackTotal);
        //            AttackValue.Text = ViewModel.Data.GetAttackTotal.ToString();
        //            break;
                
        //        case AttributeEnum.MaxHealth:
        //            MaxHealthValue.Text = ViewModel.Data.GetMaxHealthTotal.ToString();
        //            break;
               
        //        case AttributeEnum.CurrentHealth:
        //            break;
                
        //        case AttributeEnum.Unknown:
        //            AdjustSliderValues();
        //            break;
        //    }
        //}

        /// <summary>
        /// Setting the max bound on the slider to ensure it doesn't auto-adjust
        /// and ensure the labels are updating correctly when changes are made
        /// 
        /// Nothing can go over 20 on the display, player is OP at that point, no need to 
        /// show the values since it's reached our "perceived" max values
        /// </summary>
        /// <param name="s"></param>
        public void SetSliderMaximumBound()
        {
            // reload the label text when Level is changed
            SpeedValue.Text = ViewModel.Data.GetSpeedTotal.ToString();
            // Prevent the max slide value from increasing beyond 20
            SpeedSlider.Maximum = 20 - ViewModel.Data.GetSpeedTotal + ViewModel.Data.Speed;

            DefenseValue.Text = ViewModel.Data.GetDefenseTotal.ToString();
            DefenseSlider.Maximum = 20 - ViewModel.Data.GetDefenseTotal + ViewModel.Data.Defense;

            AttackValue.Text = ViewModel.Data.GetAttackTotal.ToString();
            AttackSlider.Maximum = 20 - ViewModel.Data.GetAttackTotal + ViewModel.Data.Attack;
        }

        /// <summary>
        /// The Level selected from the list
        /// Need to recalculate Max Health
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Level_Changed(object sender, EventArgs args)
        {
            // Change the Level
            //ViewModel.Data.Level = LevelPicker.SelectedIndex + 1;

            ManageHealth();
        }

        /// <summary>
        /// Change the Level Picker
        /// </summary>
        public void ManageHealth()
        {
            // Roll for new HP
            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            // Show the Result
            MaxHealthValue.Text = ViewModel.Data.MaxHealth.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            //Check if the required input fields are filled
            bool isValid = Entry_Validator();
            if (isValid == false)
            {
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new CharacterModel().ImageURI;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Slider for Level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Level_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = string.Format("{0}", Math.Round(e.NewValue));
            SetSliderMaximumBound();
            ManageHealth();
        }

        /// <summary>
        /// Catch the change to the Slider for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = string.Format("{0}", this.ViewModel.Data.GetAttackTotal);
        }

        /// <summary>
        /// Catch the change to the Slider for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = string.Format("{0}", this.ViewModel.Data.GetDefenseTotal);
        }

        /// <summary>
        /// Catch the change to the Slider for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = string.Format("{0}", this.ViewModel.Data.GetSpeedTotal);
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            _ = ViewModel.Data.AddItem(PopupLocationEnum, data.Id);

            UpdatePageBindingContext();

            SetSliderMaximumBound();

            AddItemsToDisplay();

            ClosePopup();
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemLocationEnum location)
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Items for :";
            PopupLocationValue.Text = location.ToMessage();

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null, // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel>
            {
                NoneItem
            };

            // Add the rest of the items to the list
            itemList.AddRange(ItemIndexViewModel.Instance.GetLocationItems(location).Where(item => item.IsUnique == false));

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            // Remember the location for this popup
            PopupLocationEnum = location;

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup()
        {
            PopupItemSelector.IsVisible = false;
        }

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemBox.Children.Remove(data);
            }

            //Add a StackLayout for each of the children 
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the Plus
            var ImageSource = "icon_add.png";

            var data = ViewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                data = new ItemModel { Location = location, ImageURI = ImageSource };
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI,
                HeightRequest = 40,
                WidthRequest = 40
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(location);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageLabelBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RollDice_Clicked(object sender, EventArgs e)
        {
            _ = DiceAnimationHandeler();

            _ = RandomizeCharacter();

            return;
        }

        /// <summary>
        /// 
        /// Randomize the Character
        /// Keep the Level the Same
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RandomizeCharacter()
        {
            // Randomize Name
            ViewModel.Data.Name = RandomPlayerHelper.GetCharacterName();
            ViewModel.Data.Description = RandomPlayerHelper.GetCharacterDescription();

            // Randomize the Attributes
            ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Level = RandomPlayerHelper.GetAbilityValue();

            // Randomize an Item for Location
            ViewModel.Data.Head = RandomPlayerHelper.GetItem(ItemLocationEnum.Head);
            ViewModel.Data.Necklass = RandomPlayerHelper.GetItem(ItemLocationEnum.Necklass);
            ViewModel.Data.PrimaryHand = RandomPlayerHelper.GetItem(ItemLocationEnum.PrimaryHand);
            ViewModel.Data.OffHand = RandomPlayerHelper.GetItem(ItemLocationEnum.OffHand);
            ViewModel.Data.RightFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.LeftFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.Feet = RandomPlayerHelper.GetItem(ItemLocationEnum.Feet);

            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            (ViewModel.Data.ImageURI, ViewModel.Data.Clan) = RandomPlayerHelper.GetCharacterImage();

            _ = UpdatePageBindingContext();

            SetSliderMaximumBound();

            return true;
        }

        /// <summary>
        /// Setup the Dice Animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool DiceAnimationHandeler()
        {
            // Animate the Rolling of the Dice
            var image = RollDice;
            uint duration = 1000;

            var parentAnimation = new Animation();

            // Grow the image Size
            var scaleUpAnimation = new Animation(v => image.Scale = v, 1, 2, Easing.SpringIn);

            // Spin the Image
            var rotateAnimation = new Animation(v => image.Rotation = v, 0, 360);

            // Shrink the Image
            var scaleDownAnimation = new Animation(v => image.Scale = v, 2, 1, Easing.SpringOut);

            parentAnimation.Add(0, 0.5, scaleUpAnimation);
            parentAnimation.Add(0, 1, rotateAnimation);
            parentAnimation.Add(0.5, 1, scaleDownAnimation);

            parentAnimation.Commit(this, "ChildAnimations", 16, duration, null, null);

            return true;
        }

        /// <summary>
        /// When the right button is clicked, the image will change to the next index or the beginning of the
        /// index if at the last index. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList[ViewModel.Data.Clan].Count;

            //increment, if greater than imagecount loop back
            imageIndex++;
            if (imageIndex >= imageCount)
            {
                imageIndex = 0;
            }

            // Update the image
            ViewModel.Data.ImageURI = imageList[ViewModel.Data.Clan][imageIndex];
            CharacterImage.Source = ViewModel.Data.ImageURI;
        }


        /// <summary>
        /// Change to default image of changed clan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picker_ClanChanged(object sender, EventArgs e)
        {
            imageIndex = 0;
            ViewModel.Data.ImageURI = imageList[ViewModel.Data.Clan][imageIndex];
            CharacterImage.Source = ViewModel.Data.ImageURI;
        }

        /// <summary>
        /// When the left button is clicked, the image will change to the previous index or the end of the
        /// index if at 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList[ViewModel.Data.Clan].Count;

            //decrement, if less than imagecount loop back
            imageIndex--;
            if (imageIndex < 0)
            {
                imageIndex = imageCount - 1;
            }

            // Update the image
            ViewModel.Data.ImageURI = imageList[ViewModel.Data.Clan][imageIndex];
            CharacterImage.Source = ViewModel.Data.ImageURI;
        }

        /// <summary>
        /// Helper function to help validate required input fields
        /// </summary>
        /// <returns></returns>
        private bool Entry_Validator()
        {
            bool isValid = true;

            // validate the Name has something entered
            if (string.IsNullOrWhiteSpace(this.ViewModel.Data.Name))
            {
                NameEntry.PlaceholderColor = Xamarin.Forms.Color.Red;
                isValid = false;
            }

            // validate the Description has something entered
            if (string.IsNullOrWhiteSpace(this.ViewModel.Data.Description))
            {
                DescriptionEntry.PlaceholderColor = Xamarin.Forms.Color.Red;
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Validate the Entry fields for Name and Descriptions
        /// are filled with valid text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry_Validator();
        }
    }
}