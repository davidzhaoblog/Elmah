using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public class PopupVM : _ViewModelBase
    {
        #region 1. Global UI

        public Framework.Xaml.AppShellVM AppShellVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.AppShellVM>();
            }
        }

        private bool m_EnableMasterContent;
        public bool EnableMasterContent
        {
            get { return m_EnableMasterContent; }
            set
            {
                Set(nameof(EnableMasterContent), ref m_EnableMasterContent, value);
            }
        }

        private bool m_GobackPreviousView = false;
        public bool GobackPreviousView
        {
            get { return m_GobackPreviousView; }
            set
            {
                Set(nameof(GobackPreviousView), ref m_GobackPreviousView, value);
            }
        }

        private Framework.Xaml.PopupOptions m_PopupOption;
        public Framework.Xaml.PopupOptions PopupOption
        {
            get { return m_PopupOption; }
            set
            {
                Set(nameof(PopupOption), ref m_PopupOption, value);
                RaisePropertyChanged(nameof(EnableMasterContent));
            }
        }

        /// <summary>
        /// to hide RegularPopup, BottomUp, Inline,
        /// </summary>
        /// <param name="shellNavBarIsVisible"></param>
        public void HidePopup()
        {
            EnableMasterContent = true;
            ActionSheetVM = null;
            PopupOption = PopupOptions.Invisibile;
        }

        #endregion 1. Global UI

        #region 2.RegularPopup

        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { Set(nameof(Message), ref _Message, value); }
        }

        private string m_HighlightedMessage;
        public string HighlightedMessage
        {
            get { return m_HighlightedMessage; }
            set
            {
                Set(nameof(HighlightedMessage), ref m_HighlightedMessage, value);
            }
        }

        private string m_EndMessage;
        public string EndMessage
        {
            get { return m_EndMessage; }
            set
            {
                Set(nameof(EndMessage), ref m_EndMessage, value);
            }
        }

        public ICommand Command_ClosePopup { get; set; }

        void Command_ClosePopup_Click()
        {
            HidePopup();
        }

        public ICommand Command_Close { get; set; }

        void Command_Close_Click()
        {
            if (GobackPreviousView && !IsItemControlPopupVisible) // Current View is a Page
                Shell.Current.SendBackButtonPressed();
            else if (IsItemControlPopupVisible) // Current View is a Popup with Action Buttons
                HideItemControlPopup();

            HidePopup();
        }

        #endregion 2.RegularPopup

        #region 2.1. RegularPopup.PopupActionSheet

        private bool m_ShowActionSheet = false;
        public bool ShowPopupActionSheet
        {
            get { return m_ShowActionSheet; }
            set
            {
                Set(nameof(ShowPopupActionSheet), ref m_ShowActionSheet, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionSheetVM m_PopupActionSheet;
        public Framework.Xaml.ActionForm.ActionSheetVM PopupActionSheet
        {
            get { return m_PopupActionSheet; }
            set
            {
                Set(nameof(PopupActionSheet), ref m_PopupActionSheet, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_OkActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel OkActionItemModel
        {
            get { return m_OkActionItemModel; }
            set
            {
                Set(nameof(OkActionItemModel), ref m_OkActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_CloseActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel CloseActionItemModel
        {
            get { return m_CloseActionItemModel; }
            set
            {
                Set(nameof(CloseActionItemModel), ref m_CloseActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_CancelActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel CancelActionItemModel
        {
            get { return m_CancelActionItemModel; }
            set
            {
                Set(nameof(CancelActionItemModel), ref m_CancelActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_YesActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel YesActionItemModel
        {
            get { return m_YesActionItemModel; }
            set
            {
                Set(nameof(YesActionItemModel), ref m_YesActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_NoActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel NoActionItemModel
        {
            get { return m_NoActionItemModel; }
            set
            {
                Set(nameof(NoActionItemModel), ref m_NoActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_SaveConfirmationActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel SaveConfirmationActionItemModel
        {
            get { return m_SaveConfirmationActionItemModel; }
            set
            {
                Set(nameof(SaveConfirmationActionItemModel), ref m_SaveConfirmationActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_DeleteConfirmationActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel DeleteConfirmationActionItemModel
        {
            get { return m_DeleteConfirmationActionItemModel; }
            set
            {
                Set(nameof(DeleteConfirmationActionItemModel), ref m_DeleteConfirmationActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_EnableConfirmationActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel EnableConfirmationActionItemModel
        {
            get { return m_EnableConfirmationActionItemModel; }
            set
            {
                Set(nameof(EnableConfirmationActionItemModel), ref m_EnableConfirmationActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_DisableConfirmationActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel DisableConfirmationActionItemModel
        {
            get { return m_DisableConfirmationActionItemModel; }
            set
            {
                Set(nameof(DisableConfirmationActionItemModel), ref m_DisableConfirmationActionItemModel, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_ConfirmationActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel ConfirmationActionItemModel
        {
            get { return m_ConfirmationActionItemModel; }
            set
            {
                Set(nameof(ConfirmationActionItemModel), ref m_ConfirmationActionItemModel, value);
            }
        }

        #endregion 2.1. RegularPopup.PopupActionSheet

        private Framework.Xaml.ActionForm.ActionSheetVM m_ActionSheetVM;
        // will be used when BottomUpActionSheet/InlineActionSheet
        public Framework.Xaml.ActionForm.ActionSheetVM ActionSheetVM
        {
            get { return m_ActionSheetVM; }
            set
            {
                Set(nameof(ActionSheetVM), ref m_ActionSheetVM, value);
            }
        }

        #region 3. BottomUpActionSheet

        public void ShowBottomUpActionSheet()
        {
            EnableMasterContent = false;
            PopupOption = PopupOptions.BottomUpActionSheet;
        }

        public void ShowBottomUpActionSheet(Framework.Xaml.ActionForm.ActionSheetVM actionSheetVM)
        {
            EnableMasterContent = false;
            ActionSheetVM = actionSheetVM;
            PopupOption = PopupOptions.BottomUpActionSheet;
        }

        #endregion 3. BottomUpPopup

        #region 4. InlineActionSheet

        public void ShowInlineActionSheet(Framework.Xaml.ActionForm.ActionSheetVM actionSheetVM)
        {
            EnableMasterContent = true;
            ActionSheetVM = actionSheetVM;
            PopupOption = PopupOptions.InlineActionSheet;
        }

        #endregion 4. InlinePopup

        public void ShowVerticalActionSheet()
        {
            EnableMasterContent = false;
            PopupOption = PopupOptions.VerticalActionSheet;
        }

        public void ShowVerticalActionSheet(Framework.Xaml.ActionForm.ActionSheetVM actionSheetVM)
        {
            EnableMasterContent = false;
            ActionSheetVM = actionSheetVM;
            PopupOption = PopupOptions.VerticalActionSheet;
        }

        #region 5. CountDownPopup // at bottom for now

        private Countdown _countdown = new Countdown();

        private bool m_IsCountDownPopupVisible;
        public bool IsCountDownPopupVisible
        {
            get { return m_IsCountDownPopupVisible; }
            set
            {
                Set(nameof(IsCountDownPopupVisible), ref m_IsCountDownPopupVisible, value);
            }
        }

        private bool m_IsCountDownTimerVisible;
        public bool IsCountDownTimerVisible
        {
            get { return m_IsCountDownTimerVisible; }
            set
            {
                Set(nameof(IsCountDownTimerVisible), ref m_IsCountDownTimerVisible, value);
            }
        }

        private int m_RemainingSeconds;
        public int RemainingSeconds
        {
            get { return m_RemainingSeconds; }
            set
            {
                Set(nameof(RemainingSeconds), ref m_RemainingSeconds, value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="duration">in seconds</param>
        public void ShowCountDownPopup(int duration = 10, bool isCountDownTimerVisible = true)
        {
            _countdown.Ticked += OnCountdownTicked;
            _countdown.Completed += OnCountdownCompleted;

            _countdown.Start(duration);
            RemainingSeconds = duration;
            IsCountDownPopupVisible = true;
            IsCountDownTimerVisible = isCountDownTimerVisible;
        }

        public void HideCountDownPopup()
        {
            IsCountDownPopupVisible = false;
            IsCountDownTimerVisible = false;
            _countdown.Ticked -= OnCountdownTicked;
            _countdown.Completed -= OnCountdownCompleted;
        }

        void OnCountdownTicked()
        {
            this.RemainingSeconds = (int)_countdown.RemainTime.TotalSeconds;
        }

        void OnCountdownCompleted()
        {
            HideCountDownPopup();
        }

        #endregion 5. CountDownPopup // at bottom for now

        #region 6. ItemControlPopup

        public ICommand Command_CloseItemControlPopup { get; set; }

        void Command_CloseItemControlPopup_Click()
        {
            HidePopup();

            if (GobackPreviousView && !IsItemControlPopupVisible) // Current View is a Page
                Shell.Current.SendBackButtonPressed();
            else //if (IsItemControlPopupVisible) // Current View is a Popup with Action Buttons
                HideItemControlPopup();
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_CloseItemControlPopupActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel CloseItemControlPopupActionItemModel
        {
            get { return m_CloseItemControlPopupActionItemModel; }
            set
            {
                Set(nameof(CloseItemControlPopupActionItemModel), ref m_CloseItemControlPopupActionItemModel, value);
            }
        }

        private bool m_IsItemControlPopupVisible;
        public bool IsItemControlPopupVisible
        {
            get { return m_IsItemControlPopupVisible; }
            set
            {
                Set(nameof(IsItemControlPopupVisible), ref m_IsItemControlPopupVisible, value);
            }
        }

        private string m_ItemControlKey;
        public string ItemControlKey
        {
            get { return m_ItemControlKey; }
            set
            {
                Set(nameof(ItemControlKey), ref m_ItemControlKey, value);
            }
        }

        public void ShowItemControlPopup(string itemControlKey, bool currenNavigationBarIsVisible, bool enableMasterContent = false, bool gobackPreviousView = true, bool shellNavBarIsVisible = false)
        {
            AppShellVM.SetShellNavBarIsVisible(shellNavBarIsVisible);
            IsItemControlPopupVisible = true;
            ItemControlKey = itemControlKey;
            GobackPreviousView = gobackPreviousView;
            EnableMasterContent = enableMasterContent;
        }

        public void HideItemControlPopup(bool shellNavBarIsVisible = true, bool enableMasterContent = true)
        {
            AppShellVM.SetShellNavBarIsVisible(shellNavBarIsVisible);
            IsItemControlPopupVisible = false;
            ItemControlKey = null;
            GobackPreviousView = false;
            EnableMasterContent = enableMasterContent;
        }

        #endregion 6. ItemControlPopup

        #region 7. RightSidePopup

        public ICommand Command_ShowRightSidePopup { get; set; }

        public void ShowRightSidePopup(bool shellNavBarIsVisible = false, bool enableMasterContent = false)
        {
            EnableMasterContent = shellNavBarIsVisible;
            PopupOption = PopupOptions.RightSidePopup;
            //AppShellVM.SetShellNavBarIsVisible(shellNavBarIsVisible);
        }

        public ICommand Command_HideRightSidePopup { get; set; }
        public void HideRightSidePopup(bool shellNavBarIsVisible = true, bool enableMasterContent = true)
        {
            //AppShellVM.SetShellNavBarIsVisible(shellNavBarIsVisible);
            PopupOption = PopupOptions.Invisibile;
            EnableMasterContent = enableMasterContent;
        }

        #endregion 7. RightSidePopup

        public PopupVM()
        {
            if (Command_Close == null) Command_Close = new Command(Command_Close_Click);
            if (Command_ShowRightSidePopup == null) Command_ShowRightSidePopup = new Command(() => ShowRightSidePopup());
            if (Command_HideRightSidePopup == null) Command_HideRightSidePopup = new Command(() => HideRightSidePopup());

            if (Command_ClosePopup == null) Command_ClosePopup = new Command(Command_ClosePopup_Click);
            if (Command_CloseItemControlPopup == null) Command_CloseItemControlPopup = new Command(Command_CloseItemControlPopup_Click);

            OkActionItemModel = GetCloseActionItemModel(Framework.Resx.UIStringResource.MessageOK, Framework.Xaml.FontAwesomeIcons.ThumbsUp);
            CloseActionItemModel = GetCloseActionItemModel(Framework.Resx.UIStringResource.Close, Framework.Xaml.FontAwesomeIcons.TimesCircle);
            CancelActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.Cancel, Framework.Xaml.FontAwesomeIcons.TimesCircle, Command_ClosePopup);

            YesActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.Yes, Framework.Xaml.FontAwesomeIcons.ThumbsUp, null);
            NoActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.No, Framework.Xaml.FontAwesomeIcons.ThumbsDown, Command_ClosePopup);

            CloseItemControlPopupActionItemModel = GetCloseItemControlPopupActionItemModel(Framework.Resx.UIStringResource.Close, Framework.Xaml.FontAwesomeIcons.TimesCircle);

            SaveConfirmationActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.Save, Framework.Xaml.FontAwesomeIcons.CheckCircle);
            DeleteConfirmationActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.Delete, Framework.Xaml.FontAwesomeIcons.Trash);
            EnableConfirmationActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.Enable, Framework.Xaml.FontAwesomeIcons.PlusCircle);
            DisableConfirmationActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.Disable, Framework.Xaml.FontAwesomeIcons.MinusCircle);
            ConfirmationActionItemModel = GetActionItemModel(Framework.Resx.UIStringResource.Confirm, Framework.Xaml.FontAwesomeIcons.CheckCircle);
        }

        #region ShowPopup(...)

        public void ShowPopup(string message, bool showPopupActionSheet = true, bool enableMasterContent = false, bool gobackPreviousView = false)
        {
            ShowPopupActionSheet = false;
            EnableMasterContent = enableMasterContent;
            PopupOption = Framework.Xaml.PopupOptions.RegularPopup;
            Message = message;
            HighlightedMessage = string.Empty;
            EndMessage = string.Empty;
            ShowPopupActionSheet = showPopupActionSheet;
            GobackPreviousView = gobackPreviousView;
        }

        public void ShowPopup(string message, string highlightedMessage, string endMessage, Framework.Xaml.ActionForm.ActionSheetVM actionSheet, bool enableMasterContent = false, bool gobackPreviousView = false)
        {
            PopupActionSheet = actionSheet;
            ShowPopupActionSheet = true;
            EnableMasterContent = enableMasterContent;
            PopupOption = Framework.Xaml.PopupOptions.RegularPopup;
            Message = message;
            HighlightedMessage = highlightedMessage;
            EndMessage = endMessage;
            GobackPreviousView = gobackPreviousView;
        }

        public void ShowYesNoConfirmationPopup(ICommand yesCommand, object yesCommandParameter, string message, string highlightedMessage, string endMessage)
        {
            var list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            YesActionItemModel.Command = yesCommand;
            YesActionItemModel.CommandParameter = yesCommandParameter;
            list.Add(YesActionItemModel);
            list.Add(NoActionItemModel);
            var actionSheet = new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list) };

            ShowPopup(message, highlightedMessage, endMessage, actionSheet);
        }

        public void ShowBuiltInPopup(BuiltInPopupTypes type, string message, string highlightedMessage, string endMessage, ICommand command, bool enableMasterContent = false, bool gobackPreviousView = false)
        {
            Framework.Xaml.ActionForm.ActionSheetVM actionSheet;
            if (type == Framework.Xaml.BuiltInPopupTypes.SaveConfirmation)
                actionSheet = GetSavePopupActionSheetVM(command);
            else if (type == Framework.Xaml.BuiltInPopupTypes.DeleteConfirmation)
                actionSheet = GetDeletePopupActionSheetVM(command);
            else if (type == Framework.Xaml.BuiltInPopupTypes.EnableConfirmation)
                actionSheet = GetEnableConfirmationPopupActionSheetVM(command);
            else if (type == Framework.Xaml.BuiltInPopupTypes.DisableConfirmation)
                actionSheet = GetDisableConfirmationPopupActionSheetVM(command);
            else if (type == Framework.Xaml.BuiltInPopupTypes.Confirmation)
                actionSheet = GetConfirmationPopupActionSheetVM(command);
            else if (type == Framework.Xaml.BuiltInPopupTypes.ClosePopup)
                actionSheet = GetClosePopupActionSheetVM();
            else if (type == Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup)
                actionSheet = GetCloseItemControlPopupActionSheetVM();
            else
                actionSheet = new Framework.Xaml.ActionForm.ActionSheetVM { };
            ShowPopup(message, highlightedMessage, endMessage, actionSheet, enableMasterContent, gobackPreviousView);
        }

        #endregion ShowPopup(...)

        #region Get...ActionItemModel(...)

        public Framework.Xaml.ActionForm.ActionItemModel GetCloseActionItemModel(string title = null, string fontIcon = null)
        {
            return GetActionItemModel(title ?? Framework.Resx.UIStringResource.Close, fontIcon ?? Framework.Xaml.FontAwesomeIcons.TimesCircle, Command_Close);
        }

        public Framework.Xaml.ActionForm.ActionItemModel GetCloseItemControlPopupActionItemModel(string title = null, string fontIcon = null)
        {
            return GetActionItemModel(title ?? Framework.Resx.UIStringResource.Close, fontIcon ?? Framework.Xaml.FontAwesomeIcons.TimesCircle, Command_CloseItemControlPopup);
        }

        private Framework.Xaml.ActionForm.ActionItemModel GetActionItemModel(string title = null, string fontIcon = null, ICommand command = null)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = title ?? Framework.Resx.UIStringResource.Confirm,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = fontIcon ?? Framework.Xaml.FontAwesomeIcons.CheckCircle, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = command,
                Position = 100,
            };
        }

        private Framework.Xaml.ActionForm.ActionSheetVM GetSavePopupActionSheetVM(ICommand saveCommand)
        {
            SaveConfirmationActionItemModel.Command = saveCommand;
            var itemViewModelList = new List<Framework.Xaml.ActionForm.ActionItemModel> { SaveConfirmationActionItemModel, CancelActionItemModel };
            return new Framework.Xaml.ActionForm.ActionSheetVM { ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(itemViewModelList) };
        }

        private Framework.Xaml.ActionForm.ActionSheetVM GetDeletePopupActionSheetVM(ICommand deleteCommand)
        {
            DeleteConfirmationActionItemModel.Command = deleteCommand;
            var itemViewModelList = new List<Framework.Xaml.ActionForm.ActionItemModel> { DeleteConfirmationActionItemModel, CancelActionItemModel };
            return new Framework.Xaml.ActionForm.ActionSheetVM { ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(itemViewModelList) };
        }

        private Framework.Xaml.ActionForm.ActionSheetVM GetEnableConfirmationPopupActionSheetVM(ICommand confirmCommand)
        {
            EnableConfirmationActionItemModel.Command = confirmCommand;
            var itemViewModelList = new List<Framework.Xaml.ActionForm.ActionItemModel> { EnableConfirmationActionItemModel, CancelActionItemModel };
            return new Framework.Xaml.ActionForm.ActionSheetVM { ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(itemViewModelList) };
        }

        private Framework.Xaml.ActionForm.ActionSheetVM GetDisableConfirmationPopupActionSheetVM(ICommand confirmCommand)
        {
            DisableConfirmationActionItemModel.Command = confirmCommand;
            var itemViewModelList = new List<Framework.Xaml.ActionForm.ActionItemModel> { DisableConfirmationActionItemModel, CancelActionItemModel };
            return new Framework.Xaml.ActionForm.ActionSheetVM { ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(itemViewModelList) };
        }

        private Framework.Xaml.ActionForm.ActionSheetVM GetConfirmationPopupActionSheetVM(ICommand confirmCommand)
        {
            ConfirmationActionItemModel.Command = confirmCommand;
            var itemViewModelList = new List<Framework.Xaml.ActionForm.ActionItemModel> { ConfirmationActionItemModel, CancelActionItemModel };
            return new Framework.Xaml.ActionForm.ActionSheetVM { ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(itemViewModelList) };
        }

        private Framework.Xaml.ActionForm.ActionSheetVM GetClosePopupActionSheetVM()
        {
            var itemViewModelList = new List<Framework.Xaml.ActionForm.ActionItemModel> { CloseActionItemModel };
            return new Framework.Xaml.ActionForm.ActionSheetVM { ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(itemViewModelList) };
        }

        private Framework.Xaml.ActionForm.ActionSheetVM GetCloseItemControlPopupActionSheetVM()
        {
            var itemViewModelList = new List<Framework.Xaml.ActionForm.ActionItemModel> { CloseItemControlPopupActionItemModel };
            return new Framework.Xaml.ActionForm.ActionSheetVM { ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(itemViewModelList) };
        }

        public Framework.Xaml.ActionForm.ActionItemModel GetHideRightSidePopupActionItemModel()
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Cancel,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.TimesCircle, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = Command_HideRightSidePopup,
                Position = 100,
            };
        }

        #endregion Get...ActionItemModel(...)
    }
}

