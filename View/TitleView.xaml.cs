using NSC_ModManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NSC_ModManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private DispatcherTimer _screenshotTimer;
        private const double SCREENSHOT_INTERVAL = 3.0; // seconds
        private bool _useImageA = true;

        public MainWindow() {
            InitializeComponent();
            InitializeScreenshotTimer();
            DataContext = new TitleViewModel();
            
        }
        private void InitializeScreenshotTimer()
        {
            _screenshotTimer = new DispatcherTimer();
            _screenshotTimer.Interval = TimeSpan.FromSeconds(SCREENSHOT_INTERVAL);
            _screenshotTimer.Tick += ScreenshotTimer_Tick;
            _screenshotTimer.Start();
        }

        private void ScreenshotTimer_Tick(object sender, EventArgs e)
        {
            // Advance to next screenshot with animation
            var viewModel = DataContext as TitleViewModel;
            if (viewModel != null && viewModel.HasScreenshots)
            {
                AnimateScreenshotTransition(() => viewModel.GetNextScreenshot());
            }
        }

        private void ScreenshotImage_Click(object sender, MouseButtonEventArgs e)
        {
            // Reset timer when user manually clicks
            _screenshotTimer.Stop();
            _screenshotTimer.Start();

            var viewModel = DataContext as TitleViewModel;
            if (viewModel != null && viewModel.HasScreenshots)
            {
                AnimateScreenshotTransition(() => viewModel.GetNextScreenshot());
            }
        }

        private void AnimateScreenshotTransition(Func<BitmapImage> getNextImage)
        {
            Image fadeOutImage = _useImageA ? ScreenshotImageA : ScreenshotImageB;
            Image fadeInImage = _useImageA ? ScreenshotImageB : ScreenshotImageA;

            fadeInImage.Source = getNextImage();

            var fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.4)
            };

            var fadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.4)
            };

            fadeOutImage.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            fadeInImage.BeginAnimation(UIElement.OpacityProperty, fadeIn);

            _useImageA = !_useImageA;
        }

        protected override void OnClosed(EventArgs e)
        {
            _screenshotTimer?.Stop();
            base.OnClosed(e);
        }
        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ModernWpf.Controls.ToggleSwitch;
            if (toggleSwitch == null) return;

            // Get the data context (the mod item)
            var modItem = toggleSwitch.DataContext;
            if (modItem == null) return;

            // Get the command from the window's DataContext
            var window = Window.GetWindow(toggleSwitch);
            if (window?.DataContext == null) return;

            var dataContext = window.DataContext;
            var commandProperty = dataContext.GetType().GetProperty("EnableModIsCheckedCommand");
            if (commandProperty == null) return;

            var command = commandProperty.GetValue(dataContext) as ICommand;
            if (command != null && command.CanExecute(modItem))
            {
                command.Execute(modItem);
            }
        }
        private void AnimatedText_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock == null) return;

            // Measure the text width
            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            double textWidth = textBlock.DesiredSize.Width;
            double containerWidth = 300; // Width of the container

            // Check if text is longer than "Sasuke And Naruto S1 ( Awake"
            string referenceText = "SasSasuke And Naruto S1Sas";
            FormattedText formattedReference = new FormattedText(
                referenceText,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black,
                VisualTreeHelper.GetDpi(textBlock).PixelsPerDip);

            double referenceWidth = formattedReference.Width;

            // Only animate if text is longer than reference
            if (textWidth <= referenceWidth)
            {
                return;
            }

            TranslateTransform transform = textBlock.RenderTransform as TranslateTransform;
            if (transform == null) return;

            // Calculate how far to move (negative value to move left, showing the end of text)
            double moveDistance = -(textWidth - containerWidth);

            // Create the animation
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.RepeatBehavior = RepeatBehavior.Forever;

            // Total duration for one complete cycle: 1s wait + 3s move + 1s wait + instant return
            TimeSpan totalDuration = TimeSpan.FromSeconds(5);

            // Keyframe 0: Start at position 0, wait 1 second
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));

            // Keyframe 1-2: Move to the end over 3 seconds
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(moveDistance, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4))));

            // Keyframe 3: Wait at the end for 1 second
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(moveDistance, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5))));

            // The animation loops, so it will jump back to 0 automatically

            // Start the animation
            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
        private void InstallMod_Drop(object sender, DragEventArgs e) {

            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.

                foreach (string mod_path in files) {
                    try {
                        //string modmanager_folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "modmanager");

                        string modmanager_folder = Properties.Settings.Default.ModManagerFolder;

                        if (!Directory.Exists(modmanager_folder))
                        {
                            //Directory.CreateDirectory(modmanager_folder);
                            ModernWpf.MessageBox.Show("Select Mod folder!");
                            return;
                        }
                        string InstallMod_folder = System.IO.Path.Combine(modmanager_folder,System.IO.Path.GetFileNameWithoutExtension(mod_path));
                        if (Directory.Exists(InstallMod_folder))
                        {
                            Directory.Delete(InstallMod_folder, true);
                        }
                        Directory.CreateDirectory(InstallMod_folder);
                        System.IO.Compression.ZipFile.ExtractToDirectory(mod_path, @InstallMod_folder);
                        TitleViewModel VM = ((TitleViewModel)this.DataContext);
                        VM.RefreshModList();
                    } catch (Exception ex) {
                        SystemSounds.Exclamation.Play();
                        ModernWpf.MessageBox.Show("Something went wrong.. Report issue on GitHub \n\n" + ex.StackTrace + " \n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }

            }
        }
        private void RosterEditor_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("This tool will help you to edit character roster with drag and drop.",
                20);
        }
        private void CompileMods_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("Make sure you closed game before starting compiling process. This process will also clear whole game before adding any mod. If you have important files in ModdingAPI folder, it's better to save it somewhere else.",
                20);
        }
        private void ClearGame_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("This process will clear whole game. If you have important files in ModdingAPI folder, it's better to save it somewhere else.",
                20);
        }
        private void InstallMod_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("You can install any mod with .nsc, .ensc, .uns, .unse format!",
                20);
        }
        private void DeleteMod_MouseEnter(object sender, MouseEventArgs e) {
            TitleViewModel VM = ((TitleViewModel)this.DataContext);
            VM.KyurutoDialogTextLoader("Didn't like mod?",
                20);
        }

        private void ModManagerToggle_Checked(object sender, RoutedEventArgs e)
        {
            StartStoryboardBlocking((ToggleButton)sender, "ToS4");
        }

        private void ModManagerToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            StartStoryboardBlocking((ToggleButton)sender, "ToDefault");
        }

        private void StartStoryboardBlocking(ToggleButton button, string storyboardKey)
        {
            if (button == null) return;

            // блокируем кнопку
            button.IsEnabled = false;

            // берём ресурс и клонируем, чтобы не мешать повторным вызовам
            if (!(TryFindResource(storyboardKey) is Storyboard original))
            {
                button.IsEnabled = true;
                return;
            }

            Storyboard sb = original.Clone();

            EventHandler onCompleted = null;
            onCompleted = (s, ev) =>
            {
                sb.Completed -= onCompleted;
                button.IsEnabled = true;
            };

            sb.Completed += onCompleted;

            // Запускаем сториборд в контексте ToggleButton, делаем controllable, чтобы Completed сработал.
            sb.Begin(button, true);
        }
    }
}
