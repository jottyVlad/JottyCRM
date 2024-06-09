using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace JottyCRM.View
{
    public partial class WelcomeView : UserControl
    {
        private readonly double _logoAnimationDuration = 0.5;
        private readonly double _animationDuration = 0.5;
        
        public WelcomeView()
        {
            InitializeComponent();

            Logo.Opacity = 0;
            WelcomeCaption.Opacity = 0;
            LoginButton.Opacity = 0;
            RegistrationButton.Opacity = 0;
            WelcomeCaption.Foreground = System.Windows.Media.Brushes.White;
                
            DoubleAnimation logoAnimation = new DoubleAnimation()
            {
                From = 0, To = 1, Duration = TimeSpan.FromSeconds(_animationDuration)
            };
            logoAnimation.Completed += (sender, args) =>
            {
                DoubleAnimation captionAnimation = new DoubleAnimation()
                {
                    From = 0, To = 1, Duration = TimeSpan.FromSeconds(_animationDuration)
                };

                captionAnimation.Completed += (sender1, args1) =>
                {
                    DoubleAnimation loginBtnAnimation = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromSeconds(_animationDuration)
                    };
                    loginBtnAnimation.Completed += (sender2, args2) =>
                    {
                        DoubleAnimation registrationBtnAnimation = new DoubleAnimation
                        {
                            From = 0,
                            To = 1,
                            Duration = TimeSpan.FromSeconds(_animationDuration)
                        };
                        RegistrationButton.BeginAnimation(Button.OpacityProperty, registrationBtnAnimation);
                    };

                    LoginButton.BeginAnimation(Button.OpacityProperty, loginBtnAnimation);
                };
                WelcomeCaption.BeginAnimation(TextBlock.OpacityProperty, captionAnimation);
            };

            Logo.BeginAnimation(Image.OpacityProperty, logoAnimation);
        }
    }
}