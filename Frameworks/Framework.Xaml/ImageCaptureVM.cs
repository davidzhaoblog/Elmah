using Plugin.Media.Abstractions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public class ImageCaptureVM : ViewModelBase
    {
        public event EventHandler OnCloseWindow;

        private string m_UriImageSource;
        public string UriImageSource
        {
            get { return m_UriImageSource; }
            set
            {
                Set(nameof(UriImageSource), ref m_UriImageSource, value);
            }
        }

        #region Properties

        public Collection<MediaFile> ImageCollection { get; set; } = new Collection<MediaFile>();

        private string _Photo = "profile.png";
        public string Photo
        {
            get { return _Photo; }
            set { Set(nameof(Photo), ref _Photo, value); }
        }

        private Stream _ImageStream = new MemoryStream();
        public Stream ImageStream
        {
            get { return _ImageStream; }
            set { Set(nameof(ImageStream), ref _ImageStream, value); }
        }

        private Xamarin.Forms.ImageSource _ImageSource;
        public Xamarin.Forms.ImageSource ImageSource
        {
            get { return _ImageSource; }
            set { Set(nameof(ImageSource), ref _ImageSource, value); }
        }

        private bool _IsAcceptVisible = false;
        public bool IsAcceptVisible
        {
            get { return _IsAcceptVisible; }
            set { Set(nameof(IsAcceptVisible), ref _IsAcceptVisible, value); }
        }

        #endregion Properties

        #region commands
        public ICommand Command_TakeAPhoto { get; set; }
        public ICommand Command_ChooseFromLibrary { get; set; }
        public ICommand Command_HideChoosePictureMenu { get; set; }
        public ICommand Command_Accept { get; set; }
        //public ICommand Command_SetPhotoType { get; set; }

        #endregion

        public ImageCaptureVM()
        {
            InitCommands();
            ImageSource = Xamarin.Forms.ImageSource.FromFile("userprofile.png");
            UriImageSource = "http://10.0.2.2:5000/Home/GetTestImage?id=2";
        }

        public void InitCommands()
        {
            if (Command_HideChoosePictureMenu == null) Command_HideChoosePictureMenu = new Command(Command_HideChoosePictureMenu_Click);
            if (Command_ChooseFromLibrary == null) Command_ChooseFromLibrary = new Command(Command_ChooseFromLibrary_Click);
            if (Command_Accept == null) Command_Accept = new Command<string>(Command_Accept_Click);
            if (Command_TakeAPhoto == null) Command_TakeAPhoto = new Command(Command_TakeAPhoto_Click);
            //if (Command_SetPhotoType == null) Command_SetPhotoType = new RelayCommand<PhotoCommandParameter>(Command_SetPhotoType_Click);
        }

        #region command methods
        //void Command_SetPhotoType_Click(PhotoCommandParameter param)
        //{
        //    //CurrentPhotoType = (int)System.Enum.Parse(typeof(PhotoTypes), photoType);
        //    PhotoCommandParameter = param;
        //}

        async void Command_TakeAPhoto_Click()
        {
            await Plugin.Media.CrossMedia.Current.Initialize();

            MediaFile image = await Plugin.Media.CrossMedia.Current.TakePhotoAsync((new StoreCameraMediaOptions()
            {
                AllowCropping = true,
                DefaultCamera = CameraDevice.Front,
                SaveToAlbum = false,
                //RotateImage = false,
                CustomPhotoSize = 25,
                CompressionQuality = 92,
            }));

            await ProcessImage(image);
        }

        async void Command_ChooseFromLibrary_Click()
        {
            MediaFile image = await Plugin.Media.CrossMedia.Current.PickPhotoAsync((new PickMediaOptions()
            {
                //CustomPhotoSize = 50,
                CompressionQuality = 70,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 800
            }));

            await ProcessImage(image);
        }

        void Command_HideChoosePictureMenu_Click()
        {
            OnCloseWindow?.Invoke(this, null);
        }

        /// <param name="source"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        async void Command_Accept_Click(string source)
        {
            try
            {
                // TODO: implement upload image
                //await UploadPhotoToAzureStorage(ImageStream);

            }
            catch //(Exception ex)
            {
                //Logger.I.LogError(ex, "An error occured in ImageCrop_OnImageAccepted");
            }
            OnCloseWindow?.Invoke(this, null);
            this.IsAcceptVisible = false;
            ImageSource = Xamarin.Forms.ImageSource.FromFile("userprofile.png");
        }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        public async Task<MemoryStream> UploadPhotoToAzureStorage(Stream result)
        {
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                result.Position = 0;
                result.CopyTo(ms);
                bytes = ms.ToArray();
            }

            MemoryStream msCopied = new MemoryStream(bytes);

            // Step #1. save to AzureStorage or any other storage, return url
            //string photoId = await AzureStorage.UploadBlobOperationFromStream(msCopied, AzureStorage.BlobContainerName_customerphoto);

            //Step #2. save to database if photoId
            //TODO: save to database if photoId
            //if (!string.IsNullOrEmpty(photoId))
            //{
            //    //Step #3. Delete original in AzureStorage or any other storage
            //    if (!string.IsNullOrEmpty(Photo))
            //    {
            //      await AzureStorage.DeleteBlob(Photo);
            //      Photo = photoId;
            //    }
            //}

            return msCopied;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        async Task ProcessImage(MediaFile image)
        {
            if (image != null)
            {
                //ImageCollection.Add(image);

                //if (this.PhotoCommandParameter.PhotoType == PhotoTypes.CUSTOMER_PHOTO)
                //{
                    var bitmap = SKBitmap.Decode(image.GetStreamWithImageRotatedForExternalStorage());
                    var altImage = SKImage.FromBitmap(bitmap);
                    var length = Math.Min(bitmap.Width, bitmap.Height);
                    var subset = altImage.Subset(SKRectI.Create((bitmap.Width - length) / 2, (bitmap.Height - length) / 2, length, length));

                    ImageStream = subset.Encode(SKEncodedImageFormat.Jpeg, 100).AsStream();
                //}
                //else
                //{
                //    ImageStream = image.GetStreamWithImageRotatedForExternalStorage();
                //}

                IsAcceptVisible = true;

                try
                {
                    // TODO: load image from Azure
                    //ImageSource = AzureStorage.GetImageSource(ImageStream);
                }
                catch //(Exception ex)
                {
                    //Logger.I.LogError(ex, "An error occured while processing the image");
                }
            }
        }
    }
}

