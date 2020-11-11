using DocumentFormat.OpenXml.Office.Drawing;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Win2DDrawImageTest
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CanvasBitmap createdBitmap;

        public MainPage()
        {
            this.InitializeComponent();
        }
        private CanvasDrawingSession drawingsession;

        private void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
          
            if (createdBitmap != null)
            {
               
                args.DrawingSession.DrawImage(createdBitmap);
            }
            drawingCanvas.Invalidate();
        }

        private async void btnAddPicture_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            var overlayPictureFile = await picker.PickSingleFileAsync();
            if (overlayPictureFile == null)
            {

                return;
            }
            else
            {

            }
            using (IRandomAccessStream stream = await overlayPictureFile.OpenAsync(FileAccessMode.Read))
            {
                //get canvascontrol's Device property.
                CanvasDevice device = drawingCanvas.Device;
                createdBitmap = await CanvasBitmap.LoadAsync(device, stream);
                //use device property to make renderer
                var renderer = new CanvasRenderTarget(device,
                                      createdBitmap.SizeInPixels.Width,
                                      createdBitmap.SizeInPixels.Height, createdBitmap.Dpi);
                //make ds with above renderer.
                using (var ds = renderer.CreateDrawingSession())
                {
                    ds.DrawImage(createdBitmap, 0, 0);
                }

            }
        }
    }
}
