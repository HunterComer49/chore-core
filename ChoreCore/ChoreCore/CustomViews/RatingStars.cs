using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChoreCore.CustomViews
{
    public partial class RatingStars : ContentView
    {
        public RatingStars() : base()
        {
            Grid grid = new Grid();

            //Create star image placeholders
            StarImages = new List<Image>();

            for (int i = 0; i < 5; i++)
            {
                StarImages.Add(new Image());

                //Add image
                grid.Children.Add(StarImages[i], i, 0);
            }

            Content = grid;
        }

        private List<Image> StarImages { get; set; }

        public Slider Slider { get; set; }

        public double Rating
        {
            get { return (double)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public PrecisionType Precision
        {
            get { return (PrecisionType)GetValue(PrecisionProperty); }
            set { SetValue(PrecisionProperty, value); }
        }

        public ImageSource ImageFullStar
        {
            get { return (ImageSource)GetValue(ImageFullStarProperty); }
            set { SetValue(ImageFullStarProperty, value); }
        }

        public ImageSource ImageEmptyStar
        {
            get { return (ImageSource)GetValue(ImageEmptyStarProperty); }
            set { SetValue(ImageEmptyStarProperty, value); }
        }

        public ImageSource ImageHalfStar
        {
            get { return (ImageSource)GetValue(ImageHalfStarProperty); }
            set { SetValue(ImageHalfStarProperty, value); }
        }

        public double StarWidth
        {
            get { return (double)GetValue(StarWidthProperty); }
            set { SetValue(StarWidthProperty, value); }
        }

        public double StarHeight
        {
            get { return (double)GetValue(StarHeightProperty); }
            set { SetValue(StarHeightProperty, value); }
        }

        public static readonly BindableProperty RatingProperty =
            BindableProperty.Create(propertyName: nameof(Rating),
              returnType: typeof(double),
              declaringType: typeof(RatingStars),
              defaultValue: 0.0,
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: UpdateStarsDisplay);

        public static readonly BindableProperty PrecisionProperty =
         BindableProperty.Create(propertyName: nameof(Precision),
             returnType: typeof(PrecisionType),
             declaringType: typeof(RatingStars),
             defaultValue: PrecisionType.Half,
             propertyChanged: OnPrecisionPropertyChanged);

        public static readonly BindableProperty ImageFullStarProperty =
          BindableProperty.Create(propertyName: nameof(ImageFullStar),
              returnType: typeof(ImageSource),
              declaringType: typeof(RatingStars),
              defaultValue: ImageSource.FromResource("rating_full.png"),
              propertyChanged: UpdateStarsDisplay);

        public static readonly BindableProperty ImageEmptyStarProperty =
          BindableProperty.Create(propertyName: nameof(ImageEmptyStar),
              returnType: typeof(ImageSource),
              declaringType: typeof(RatingStars),
              defaultValue: ImageSource.FromResource("rating_empty.png"),
              propertyChanged: UpdateStarsDisplay);

        public static readonly BindableProperty ImageHalfStarProperty =
          BindableProperty.Create(propertyName: nameof(ImageHalfStar),
              returnType: typeof(ImageSource),
              declaringType: typeof(RatingStars),
              defaultValue: ImageSource.FromResource("rating_half.png"),
              propertyChanged: UpdateStarsDisplay);

        public static readonly BindableProperty StarWidthProperty =
          BindableProperty.Create(propertyName: nameof(StarWidth),
              returnType: typeof(double),
              declaringType: typeof(RatingStars),
              defaultValue: 0.0,
              propertyChanged: UpdateStarsDisplay);

        public static readonly BindableProperty StarHeightProperty =
          BindableProperty.Create(propertyName: nameof(StarHeight),
              returnType: typeof(double),
              declaringType: typeof(RatingStars),
              defaultValue: 0.0,
              propertyChanged: UpdateStarsDisplay);

        private static void OnPrecisionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RatingStars control = (RatingStars)bindable;
            control.Slider.Maximum = ((PrecisionType)newValue).Equals(PrecisionType.Half) ? 10 : 5;
            control.UpdateStarsDisplay();
        }

        private static void UpdateStarsDisplay(BindableObject bindable, object oldValue, object newValue)
        {
            ((RatingStars)bindable).UpdateStarsDisplay();
        }

        //Fill the stars based on the rating value
        private void UpdateStarsDisplay()
        {
            var value = Rating;

            for (int i = 0; i < StarImages.Count; i++)
            {
                StarImages[i].Source = GetStarSource(i, value);
                StarImages[i].WidthRequest = StarWidth;
                StarImages[i].HeightRequest = StarHeight;

                value--;
            }
        }

        private ImageSource GetStarSource(int position, double value)
        {
            if (value > 1)
            {
                return ImageFullStar;
            }
            else if (value <= 0.9 && value >= 0.1 && Precision == PrecisionType.Half)
            {
                return ImageHalfStar;
            }
            else
            {
                return ImageEmptyStar;
            }

            //int currentStarMaxRating = position + 1;

            //if (Precision.Equals(PrecisionType.Half))
            //{
            //    currentStarMaxRating *= 2;
            //}

            //if (Rating >= currentStarMaxRating)
            //{
            //    return ImageFullStar;
            //}
            //else if (Rating >= currentStarMaxRating - 1 && Precision.Equals(PrecisionType.Half))
            //{
            //    return ImageHalfStar;
            //}
            //else
            //{
            //    return ImageEmptyStar;
            //}
        }
    }
}