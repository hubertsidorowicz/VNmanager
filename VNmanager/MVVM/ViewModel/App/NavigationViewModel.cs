using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VNmanager
{
    public class NavigationViewModel : BaseViewModel
    {
        #region Private Memebers
        
        /// <summary>
        /// List of view Models that app can change between
        /// </summary>
        private List<IPageViewModel> _pageViewModels;

        /// <summary>
        /// List of view Models that app can change between
        /// </summary>
        private List<string> _pageHistory;
        
        /// <summary>
        /// Index of Page that we are on now
        /// </summary>
        public int PageHistoryIndex = -1;

        /// <summary>
        /// Is true when we are going by arrows
        /// </summary>
        private bool isArrow { get; set; } = false;

        #endregion

        #region Public Properties

        ///<summary>
        /// Instance of main view model
        /// </summary>
        private MainViewModel Mvvm { get { return App.Mvvm; } }
        
        /// <summary>
        /// List of view Models that app can change between
        /// </summary>
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        /// <summary>
        /// History of last visited pages
        /// </summary>
        public List<string> PageHistory 
        {
            get 
            {
                if (_pageHistory == null)
                    _pageHistory = new List<string>();

                return _pageHistory;
            }
        }

        #endregion

        #region Public Commands
        /// <summary>
        /// A command to Change the view in MainWindow
        /// </summary>
        
        public RelayCommand<string> ChangeView { get { return new RelayCommand<string>(Change); } }
        public RelayCommand MoveBack { get { return new RelayCommand(Back); } }
        public RelayCommand MoveForward { get { return new RelayCommand(Forward); } }
        #endregion

        #region Constructor

        public NavigationViewModel()
        {
            //ChangeView = new RelayCommand<string>(Change);
            // Add available pages and set page

            Mvvm.gameOnDisplay = "Collection";
            AddPageHistory(Mvvm.gameOnDisplay);

            PageViewModels.Add(new GameCollectionViewModel());
            PageViewModels.Add(new GameProfileViewModel());
            PageViewModels.Add(new GameProfile2ViewModel());
            PageViewModels.Add(new TuneProfileViewModel());

            var b = ValidateHistory("back");
            var f = ValidateHistory("forward");
        }
        #endregion

        #region Command Methods

        public void Change(string target)
        {
            Console.WriteLine(target);

            if(target == "Collection")
            {
                Mvvm.CurrentPageViewModel = PageViewModels[0];
                AddPageHistory(target);
            }
            else if(target == "_back_")
            {
                Console.WriteLine("Going Back");
                if (ValidateHistory("back") == true)
                {
                    Mvvm.gameOnDisplay = PageHistory[PageHistoryIndex - 1];
                    PageHistoryIndex--;
                    Mvvm.ChangeColor.Execute(Mvvm.gameOnDisplay);
                    if (Mvvm.gameOnDisplay != "Collection")
                        Mvvm.SetProfileData();
                    
                    if (Mvvm.gameOnDisplay == "Collection")
                        Mvvm.CurrentPageViewModel = PageViewModels[0];
                    else
                    {
                        if(Mvvm.CurrentPageViewModel == PageViewModels[1])
                            Mvvm.CurrentPageViewModel = PageViewModels[2];
                        else
                            Mvvm.CurrentPageViewModel = PageViewModels[1];
                    }
                }
            }
            else if (target == "_forward_")
            {
                if (ValidateHistory("forward") == true)
                {
                    Mvvm.gameOnDisplay = PageHistory[PageHistoryIndex + 1];
                    PageHistoryIndex++;
                    Mvvm.ChangeColor.Execute(Mvvm.gameOnDisplay);

                    if (Mvvm.gameOnDisplay != "Collection")
                        Mvvm.SetProfileData();

                    if (Mvvm.gameOnDisplay == "Collection")
                        Mvvm.CurrentPageViewModel = PageViewModels[0];
                    else
                    {
                        if (Mvvm.CurrentPageViewModel == PageViewModels[1])
                            Mvvm.CurrentPageViewModel = PageViewModels[2];
                        else
                            Mvvm.CurrentPageViewModel = PageViewModels[1];
                    }
                }
            }
            else if (target == "TuneProfile")
            {
                Console.WriteLine("Tune Profile !!!!!!!!!!!!!!!!!! ");
                AddPageHistory("TuneProfile");

                Mvvm.CurrentPageViewModel = PageViewModels[3];
            }
            else
            {
                Mvvm.gameOnDisplay = target;
                Mvvm.SetProfileData();
                AddPageHistory(target);

                if (Mvvm.CurrentPageViewModel == PageViewModels[0] || Mvvm.CurrentPageViewModel == PageViewModels[1])
                    Mvvm.CurrentPageViewModel = PageViewModels[2];
                else
                    Mvvm.CurrentPageViewModel = PageViewModels[1];
            }

            var b = ValidateHistory("back");
            var f = ValidateHistory("forward");
        }

        public void AddPageHistory(string title)
        {
            PageHistoryIndex++;
            PageHistory.Add(title);
            ShowHistory();
        }
        public void BackPageHistory()
        {
            PageHistoryIndex--;
        }
        private bool ValidateHistory(string p)
        {
            if (p == "back")
            {
                Mvvm.backImage = "/Images/Icons/ArrowBackGrey.png";
                if (PageHistoryIndex == 0)
                    return false;

                string game = PageHistory[PageHistoryIndex - 1];

                if (!isGameInCollection(game))
                    return false;

                Mvvm.backImage = "/Images/Icons/ArrowBackLight.png";
                return true;
            }
            else if(p == "forward")
            {
                Mvvm.forwardImage = "/Images/Icons/ArrowForwardGrey.png";
                if (PageHistoryIndex == PageHistory.Count-1)
                    return false;

                string game = PageHistory[PageHistoryIndex + 1];

                if (!isGameInCollection(game))
                    return false;

                Mvvm.forwardImage = "/Images/Icons/ArrowForwardLight.png";
                return true;
            }
            return false;

        }

        public void Back(object o)
        {
            Change("_back_");
        }

        public void Forward(object o)
        {
            Change("_forward_");

        }

        private void ShowHistory()
        {
            foreach (var ph in PageHistory)
            {
                Console.WriteLine(ph);
            }
        }

        #endregion

        #region Helpers

        private bool isGameInCollection(string gameTitle)
        {
            var check = SqliteDataAccess.LoadOneGame(gameTitle);

            if (check.Count == 0 && gameTitle != "Collection")
                return false;

            return true;
        }

        #endregion
    }
}
