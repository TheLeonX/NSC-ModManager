using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace NSC_ModManager
{
    public static class AnimatedVisibility
    {
        public static readonly DependencyProperty TargetVisibilityProperty =
            DependencyProperty.RegisterAttached(
                "TargetVisibility",
                typeof(Visibility),
                typeof(AnimatedVisibility),
                new PropertyMetadata(Visibility.Visible, OnTargetVisibilityChanged));

        public static void SetTargetVisibility(DependencyObject d, Visibility value) =>
            d.SetValue(TargetVisibilityProperty, value);

        public static Visibility GetTargetVisibility(DependencyObject d) =>
            (Visibility)d.GetValue(TargetVisibilityProperty);

        private static readonly TimeSpan Duration = TimeSpan.FromMilliseconds(220);

        private static void OnTargetVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement fe) return;

            var newVis = (Visibility)e.NewValue;
            if (newVis == Visibility.Visible)
                FadeIn(fe);
            else
                FadeOut(fe, newVis);
        }

        private static void FadeIn(FrameworkElement fe)
        {
            fe.BeginAnimation(UIElement.OpacityProperty, null);
            fe.Visibility = Visibility.Visible;
            fe.Opacity = 0;

            var da = new DoubleAnimation(0, 1, Duration)
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop
            };

            da.Completed += (s, e) => fe.Opacity = 1;

            fe.BeginAnimation(UIElement.OpacityProperty, da);
        }

        private static void FadeOut(FrameworkElement fe, Visibility finalVisibility)
        {
            fe.BeginAnimation(UIElement.OpacityProperty, null);

            if (fe.Visibility != Visibility.Visible)
            {
                fe.Visibility = finalVisibility;
                return;
            }

            var da = new DoubleAnimation(fe.Opacity, 0, Duration)
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn },
                FillBehavior = FillBehavior.Stop
            };

            da.Completed += (s, e) =>
            {
                fe.Opacity = 1;
                fe.Visibility = finalVisibility;
            };

            fe.BeginAnimation(UIElement.OpacityProperty, da);
        }
    }
}
